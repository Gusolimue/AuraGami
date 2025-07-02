using UnityEngine;
using UnityEngine.UI;

public class ConstellationBehavior : MonoBehaviour
{
    [SerializeField] Slider[] connectors;
    public float fillSpeed = 5f;
    private float fillTarget = 1f;
    private float count = 0;

    private void Awake()
    {
        foreach (Slider sliders in connectors)
        {
            sliders.value = 0f;
        }
    }

    private void Update()
    {
        count += Time.deltaTime;
        foreach (Slider sliders in connectors)
        {
            sliders.value = Mathf.Lerp(sliders.value, fillTarget, count / fillSpeed);
        }
    }
}
