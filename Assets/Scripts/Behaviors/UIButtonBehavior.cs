using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonBehavior : MonoBehaviour
{
    //[SerializeField] public ParticleSystem highlightNebula;

    [Header("Highlight Nebula")]
    [SerializeField] Image highLightNebula;
    [SerializeField] Color[] fadeColors;
    public int fadeSpeed;


    private bool isHighlight;
    private float count;
 
    private void Awake()
    {
        //highlightNebula.gameObject.SetActive(false);
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isHighlight) highLightNebula.color = Color.Lerp(highLightNebula.color, fadeColors[0], count / 20);
        else highLightNebula.color = Color.Lerp(highLightNebula.color, fadeColors[1], count / fadeSpeed);
    }

    /*public void OnButtonEnter()
    {
        highlightNebula.gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
    }

    public void OnButtonExit()
    {
        highlightNebula.gameObject.SetActive(false);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverExitSmall);
    }*/

    public void OnButtonEnter()
    {
        isHighlight = true;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .1f, .1f);
        count = 0;
    }

    public void OnButtonExit()
    {
        isHighlight = false;
        count = 0;
    }
}
