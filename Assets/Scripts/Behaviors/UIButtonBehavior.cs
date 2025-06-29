using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonBehavior : MonoBehaviour
{
    [Header("Highlight Nebula")]
    [SerializeField] Image highLightNebula;
    [SerializeField] Color[] fadeColors;

    public int fadeSpeed;
    private bool isHighlight;
    private float count;
 
    private void Awake()
    {
        Color curColor = highLightNebula.color;
        highLightNebula.color = new Color(curColor.r, curColor.g, curColor.b, 0);
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isHighlight) highLightNebula.color = Color.Lerp(highLightNebula.color, fadeColors[0], count / 20);
        else highLightNebula.color = Color.Lerp(highLightNebula.color, fadeColors[1], count / fadeSpeed);
    }

    public void OnButtonEnter()
    {
        isHighlight = true;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .1f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
        count = 0;
    }

    public void OnButtonExit()
    {
        isHighlight = false;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverExitSmall);
        count = 0;
    }
}
