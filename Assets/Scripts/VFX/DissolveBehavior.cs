using System.Collections;
using UnityEngine;

public class DissolveBehavior : MonoBehaviour
{

    Renderer r;
    public float fillTarget ;
    public float count;
    public float fillTime = .2f;
    float time;

    private void Awake()
    {
        fillTarget = 0f;
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
        //r.sharedMaterials[0].SetFloat("Dissolve", Mathf.Lerp(.84f, .21f, 0f));

        //SetDissolve(Mathf.Lerp(GetDissolvePercent(), fillTarget, count / time));
        //count += Time.deltaTime;
        //Debug.Log(GetDissolvePercent());
    }

    //shader float is really .21f to .84f for full to empty
    void SetDissolve(float _percentage)
    {
        r.sharedMaterials[0].SetFloat("Dissolve",  Mathf.Lerp(.84f, .21f, _percentage));
    }

    float GetDissolvePercent()
    {
        float tmpFloat = r.sharedMaterials[0].GetFloat("Dissolve");
        return Mathf.InverseLerp(.84f, .21f, tmpFloat);
    }
    public void ResetSphereFill(float _time = 0)
    {
        StartCoroutine(COSphereDrain(_time));
    }
    public void FillSphere(float _percentage, float _time)
    {
        //ResetSphereFill();
        //count = 0;
        //time = _time;
        //fillTarget = _percentage;
        StartCoroutine(COSphereFill(_percentage, _time));
    }

    IEnumerator COSphereFill(float _percentage, float _time)
    {
        SetDissolve(0f);
        float dissolveAmount = _percentage;
        float count = 0;
        while (count < _time)
        {
            SetDissolve(Mathf.Lerp(0f, _percentage, count / _time));
            count += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator COSphereDrain(float _time)
    {
        float count = 0;
        float start = GetDissolvePercent();
        while (count < _time)
        {
            SetDissolve(Mathf.Lerp(start, 0f, count / _time));
            count += Time.deltaTime;
            yield return null;
        }
    }
}
