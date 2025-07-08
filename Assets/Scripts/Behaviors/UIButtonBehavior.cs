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

    public void OnButtonEnter() // Using an Event Trigger, I use this script to fade in and out the highlight nebula for every nebula-based button. It make producing each button a thirty-second process.
                                // With this method, beyond calling for other methods that play sound and activate haptics, I used a bool to determine whether the highlight nebula fades in or out. In this case, it fades in.
    {
        isHighlight = true;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .1f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
        count = 0;
    }

    public void OnButtonExit() // When the cursor exits the button, the highlight nebula fades out.
    {
        isHighlight = false;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverExitSmall);
        count = 0;
    }
}
