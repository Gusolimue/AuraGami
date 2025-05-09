using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsButtonBehavior : MonoBehaviour
{
    [SerializeField] Slider[] connectors;
    public float connectorValueTarget;
    public float fillSpeed = 1f;

    private bool isHovering;


    private void Update()
    {
        foreach (Slider slider in connectors)
        {
            if (slider == null) continue;

            float targetValue = isHovering ? 1f : 0f;
            slider.value = Mathf.MoveTowards(slider.value, targetValue, fillSpeed * Time.deltaTime);
        }
    }

    public void IncreaseFill()
    {
        isHovering = true;
    }

    public void DecreaseFill()
    {
        isHovering = false;
    }

}
