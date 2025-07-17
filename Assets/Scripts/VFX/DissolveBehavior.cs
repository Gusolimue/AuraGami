using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DissolveBehavior : MonoBehaviour
{

    Renderer r;
    public float fillTarget;
    public float count;
    public float fillTime = .2f;
    float time;

    Transform uprightSphere;
    private void Awake()
    {
        r = GetComponent<Renderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetSphereFill();
    }

    // Update is called once per frame
    void Update()
    {
        SetDissolve(Mathf.Lerp(GetDissolvePercent(), fillTarget, count / time));
        count += Time.deltaTime;
        //Debug.Log(GetDissolvePercent());
    }

    //shader float is really .21f to .84f for full to empty
    void SetDissolve(float _percentage)
    {
        r.sharedMaterials[0].SetFloat("Vector1_9e41d94fb3a847cea58826d26a43525d",  Mathf.Lerp(.84f, .21f, _percentage));
    }

    float GetDissolvePercent()
    {
        float tmpFloat = r.sharedMaterials[0].GetFloat("Vector1_9e41d94fb3a847cea58826d26a43525d");
        return Mathf.InverseLerp(.84f, .21f, tmpFloat);
    }
    public void ResetSphereFill(float _time = 0)
    {
        count = 0;
        time = _time;
        fillTarget = 0;
        SetDissolve(0);
    }
    public void FillSphere(float _percentage)
    {
        count = 0;
        time = fillTime;
        fillTarget += _percentage;
    }
}
