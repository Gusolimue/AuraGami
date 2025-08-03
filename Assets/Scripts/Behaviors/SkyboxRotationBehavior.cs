using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SkyboxRotationBehavior : MonoBehaviour
{
    public float speed;
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
    }
}
