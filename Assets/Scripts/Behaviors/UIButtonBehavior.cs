using UnityEngine;
using System.Collections;

public class UIButtonBehavior : MonoBehaviour
{
    [SerializeField] public ParticleSystem highlightNebula;

    private void Awake()
    {
        highlightNebula.gameObject.SetActive(false);
    }
    public void OnButtonEnter()
    {
        highlightNebula.gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
    }

    public void OnButtonExit()
    {
        highlightNebula.gameObject.SetActive(false);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverExitSmall);
    }
}
