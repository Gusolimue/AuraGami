using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowButtonBehavior : MonoBehaviour
{
    [Header("Arrow Fill")]
    [SerializeField] Slider fillSlider;
    [SerializeField] Image fill;
    [SerializeField] Color[] fillColors;

    private bool isHover;
    private float count;
    private int curColor;
    private float curSpeed;
    public bool isDefault;

    private void Awake()
    {
        fillSlider.value = 0f;
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isHover)
        {
            fill.color = Color.Lerp(fill.color, fillColors[curColor], count /curSpeed);
            fillSlider.value = Mathf.Lerp(fillSlider.value, 1f, count / 2.5f);
        }
        else
        {
            fill.color = Color.Lerp(fill.color, fillColors[1], count / curSpeed);
            fillSlider.value = Mathf.Lerp(fillSlider.value, 0f, count / 2.5f);
        }
    }

    public void OnHover()
    {
        count = 0;
        isHover = true;
        if (isDefault) curSpeed = .5f;
        else curSpeed = 5;
        curColor = 0;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .1f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
    }

    public void StopHover()
    {
        count = 0;
        isHover = false;
        if (isDefault) curSpeed = 5f;
        else curSpeed = 5;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .1f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
    }

    public void OnArrowPressed()
    {
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        curSpeed = 1;
        count = 0;
        Debug.Log("Arrow Pressed");
        StartCoroutine(ArrowPress());
    }

    private IEnumerator ArrowPress()
    {
        curColor = 2;
        yield return new WaitForSeconds(.2f);
        count = 0;
        curColor = 0;
    }
}
