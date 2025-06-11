using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsButtonBehavior : MonoBehaviour
{
    [Header("Connectors")]
    public static SettingsButtonBehavior Instance;
    [SerializeField] public Slider[] connectors;
    private float fillSpeed = 4f;
    public bool isSelected;

    [Header("Connector Shadows")]
    [SerializeField] Image[] connectorShadows;
    [SerializeField] Color[] targetColor;
    private float hoverSpeed = 5f;
    private bool isHovering;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        foreach (Slider slider in connectors)
        {
            if (slider == null) continue;

            float targetValue = isSelected ? 1f : 0f;
            slider.value = Mathf.MoveTowards(slider.value, targetValue, fillSpeed * Time.deltaTime);
        }

        foreach (Image image in connectorShadows)
        {
            if (isHovering) image.color = Color.Lerp(image.color, targetColor[0], hoverSpeed * Time.deltaTime);
            if (!isHovering) image.color = Color.Lerp(image.color, targetColor[1], hoverSpeed * Time.deltaTime);
        }
    }

    public void IncreaseFill()
    {
        isHovering = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
    }

    public void DecreaseFill()
    {
        isHovering = false;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverExitSmall);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }

}
