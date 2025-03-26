using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using Unity.Mathematics;

[ExecuteAlways]
public class PlanarReflections : MonoBehaviour
{
    [Serializable]
    public enum Resolution
    {
        Full,
        Half,
        Third,
        Quarter
    }

    [Serializable]
    public class PlanarReflectionSettings
    {
        public Resolution resolution = Resolution.Full;
        public float m_ClipPlaneOffset = 0.07f;
        public LayerMask reflectionLayers = -1;
        public bool shadows;
    }

    [SerializeField]
    public PlanarReflectionSettings reflectionSettings = new PlanarReflectionSettings();

    public GameObject target;
    public float planeOffset;
    private static Camera reflectionCamera;
    private RenderTexture reflectionTexture;

    private int2 _oldReflectionTextureSize;

    public static event Action<ScriptableRenderContext, Camera> BeginPlanarReflections;

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += ExecutePlanarReflections;
    }

    private void OnDisable()
    {
        Cleanup();
    }

    private void OnDestroy()
    {
        Cleanup();
    }

    private void Cleanup()
    {
        RenderPipelineManager.beginCameraRendering -= ExecutePlanarReflections;

        if (reflectionCamera)
        {
            reflectionCamera.targetTexture = null;
            SafeDestroy(reflectionCamera.gameObject);
        }
        if (reflectionTexture) RenderTexture.ReleaseTemporary(reflectionTexture);
    }

    private static void SafeDestroy(GameObject obj)
    {
        if (Application.isEditor) DestroyImmediate(obj);
        else Destroy(obj);
    }

    private void UpdateCamera(Camera src, Camera dest)
    {
        if (dest == null) return;

        dest.CopyFrom(src);
        dest.useOcclusionCulling = false;
        if (dest.gameObject.TryGetComponent(out UniversalAdditionalCameraData camData))
        {
            camData.renderShadows = reflectionSettings.shadows;
        }
    }

    private void UpdateReflectionCamera(Camera realCamera)
    {
        if (reflectionCamera == null)
            reflectionCamera = CreateMirrorObjects();

        /* Reflection plane position and normal */
        Vector3 pos = Vector3.zero;
        Vector3 normal = Vector3.up;
        if (target != null)
        {
            pos = target.transform.position + Vector3.up * planeOffset;
            normal = target.transform.up;
        }

        UpdateCamera(realCamera, reflectionCamera);

        /* Render reflection */
        var d = -Vector3.Dot(normal, pos) - reflectionSettings.m_ClipPlaneOffset;
        var reflectionPlane = new Vector4(normal.x, normal.y, normal.z, d);

        var reflection = Matrix4x4.identity;
        reflection *= Matrix4x4.Scale(new Vector3(1, -1, 1));

        CalculateReflectionMatrix(ref reflection, reflectionPlane);
        var oldPosition = realCamera.transform.position - new Vector3(0, pos.y * 2, 0);
        var newPosition = ReflectPosition(oldPosition);
        reflectionCamera.transform.forward = Vector3.Scale(realCamera.transform.forward, new Vector3(1, -1, 1));
        reflectionCamera.worldToCameraMatrix = realCamera.worldToCameraMatrix * reflection;

        // Setup oblique projection matrix so that near plane is our reflection
        // plane. This way we clip everything below/above it for free.
        var clipPlane = CameraSpacePlane(reflectionCamera, pos - Vector3.up * 0.1f, normal, 1.0f);
        var projection = realCamera.CalculateObliqueMatrix(clipPlane);
        reflectionCamera.projectionMatrix = projection;
        reflectionCamera.cullingMask = reflectionSettings.reflectionLayers; // never render water layer
        reflectionCamera.transform.position = newPosition;
    }

    /* Calculate reflection matrix around given plane */
    private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMatrix, Vector4 plane)
    {
        reflectionMatrix.m00 = (1F - 2F * plane[0] * plane[0]);
        reflectionMatrix.m01 = (-2F * plane[0] * plane[1]);
        reflectionMatrix.m02 = (-2F * plane[0] * plane[2]);
        reflectionMatrix.m03 = (-2F * plane[3] * plane[0]);

        reflectionMatrix.m10 = (-2F * plane[1] * plane[0]);
        reflectionMatrix.m11 = (1F - 2F * plane[1] * plane[1]);
        reflectionMatrix.m12 = (-2F * plane[1] * plane[2]);
        reflectionMatrix.m13 = (-2F * plane[3] * plane[1]);

        reflectionMatrix.m20 = (-2F * plane[2] * plane[0]);
        reflectionMatrix.m21 = (-2F * plane[2] * plane[1]);
        reflectionMatrix.m22 = (1F - 2F * plane[2] * plane[2]);
        reflectionMatrix.m23 = (-2F * plane[3] * plane[2]);

        reflectionMatrix.m30 = 0F;
        reflectionMatrix.m31 = 0F;
        reflectionMatrix.m32 = 0F;
        reflectionMatrix.m33 = 1F;
    }

    private static Vector3 ReflectPosition(Vector3 pos)
    {
        var newPos = new Vector3(pos.x, -pos.y, pos.z);
        return newPos;
    }

    private float GetScaleValue()
    {
        switch (reflectionSettings.resolution)
        {
            case Resolution.Full:
                return 1f;
            case Resolution.Half:
                return 0.5f;
            case Resolution.Third:
                return 0.33f;
            case Resolution.Quarter:
                return 0.25f;
            default:
                return 0.5f; // default to half res
        }
    }

    private static bool Int2Compare(int2 a, int2 b)
    {
        return a.x == b.x && a.y == b.y;
    }

    // Given position/normal of the plane, calculates plane in camera space.
    private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
    {
        var offsetPos = pos + normal * reflectionSettings.m_ClipPlaneOffset;
        var m = cam.worldToCameraMatrix;
        var cameraPosition = m.MultiplyPoint(offsetPos);
        var cameraNormal = m.MultiplyVector(normal).normalized * sideSign;
        return new Vector4(cameraNormal.x, cameraNormal.y, cameraNormal.z, -Vector3.Dot(cameraPosition, cameraNormal));
    }

    private Camera CreateMirrorObjects()
    {
        var go = new GameObject("Planar Reflections", typeof(Camera));
        var cameraData = go.AddComponent(typeof(UniversalAdditionalCameraData)) as UniversalAdditionalCameraData;

        cameraData.requiresColorOption = CameraOverrideOption.Off;

        cameraData.requiresDepthOption = CameraOverrideOption.Off;
        cameraData.SetRenderer(0);

        var t = transform;
        var reflectionCamera = go.GetComponent<Camera>();
        reflectionCamera.transform.SetPositionAndRotation(t.position, t.rotation);
        reflectionCamera.depth = -10;
        reflectionCamera.enabled = false;
        go.hideFlags = HideFlags.HideAndDontSave;

        return reflectionCamera;
    }

    private void PlanarReflectionTexture(Camera cam)
    {
        if (reflectionTexture == null)
        {
            var res = ReflectionResolution(cam, UniversalRenderPipeline.asset.renderScale);
            const bool useHdr10 = true;
            const RenderTextureFormat hdrFormat = useHdr10 ? RenderTextureFormat.RGB111110Float : RenderTextureFormat.DefaultHDR;

            reflectionTexture = RenderTexture.GetTemporary(res.x, res.y, 16,
                GraphicsFormatUtility.GetGraphicsFormat(hdrFormat, true));
        }

        reflectionCamera.targetTexture = reflectionTexture;
    }

    private int2 ReflectionResolution(Camera cam, float scale)
    {
        var x = (int)(cam.pixelWidth * scale * GetScaleValue());
        var y = (int)(cam.pixelHeight * scale * GetScaleValue());
        return new int2(x, y);
    }

    private void ExecutePlanarReflections(ScriptableRenderContext context, Camera camera)
    {
        // we dont want to render planar reflections in reflections or previews
        if (camera.cameraType == CameraType.Reflection || camera.cameraType == CameraType.Preview)
            return;

        UpdateReflectionCamera(camera); // create reflected camera
        PlanarReflectionTexture(camera); // create and assign RenderTexture

        var data = new PlanarReflectionSettingData(); // save quality settings and lower them for the planar reflections
        data.Set(); // set quality settings

        BeginPlanarReflections?.Invoke(context, reflectionCamera); // callback Action for PlanarReflection
        UniversalRenderPipeline.RenderSingleCamera(context, reflectionCamera); // render planar reflections

        data.Restore(); // restore the quality settings
        Shader.SetGlobalTexture("_PlanarReflectionTexture", reflectionTexture); // Assign texture to water shader
    }

    class PlanarReflectionSettingData
    {
        private readonly bool _fog;
        private readonly int _maxLod;
        private readonly float _lodBias;

        public PlanarReflectionSettingData()
        {
            _fog = RenderSettings.fog;
            _maxLod = QualitySettings.maximumLODLevel;
            _lodBias = QualitySettings.lodBias;
        }

        public void Set()
        {
            GL.invertCulling = true;
            RenderSettings.fog = false; // disable fog for now as it's incorrect with projection
            QualitySettings.maximumLODLevel = 1;
            QualitySettings.lodBias = _lodBias * 0.5f;
        }

        public void Restore()
        {
            GL.invertCulling = false;
            RenderSettings.fog = _fog;
            QualitySettings.maximumLODLevel = _maxLod;
            QualitySettings.lodBias = _lodBias;
        }
    }
}
