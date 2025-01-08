using UnityEngine;
using UnityEngine.UI;

public class FrontEnd : MonoBehaviour
{
    [SerializeField] GameObject playButton_BG_OnEnter;
    [SerializeField] GameObject playButton_BG_OnPressed;
    public Animator playButton_BG_OnEnter_Anim;
    public Animator playButton_BG_OnExit_Anim;
    public Animator playButton_BG_OnPressed_Anim;

    private void Awake()
    {
        playButton_BG_OnEnter_Anim = playButton_BG_OnEnter.GetComponent<Animator>();
        playButton_BG_OnExit_Anim = playButton_BG_OnEnter.GetComponent<Animator>();
        playButton_BG_OnPressed_Anim = playButton_BG_OnPressed.GetComponent<Animator>();

        playButton_BG_OnEnter_Anim.enabled = false;
        playButton_BG_OnExit_Anim.enabled = false;
        playButton_BG_OnPressed_Anim.enabled = false;
    }

    public void OnPlayButtonEnter()
    {
        playButton_BG_OnEnter_Anim.enabled = true;
        playButton_BG_OnEnter_Anim.SetTrigger("OnPlayButtonEnter");
    }
    public void OnPlayButtonExit()
    {
        playButton_BG_OnExit_Anim.enabled = true;
        playButton_BG_OnExit_Anim.SetTrigger("OnPlayButtonExit");
    }

    public void OnPlayButtonPressed()
    {
        playButton_BG_OnPressed_Anim.enabled = true;
        playButton_BG_OnPressed_Anim.SetTrigger("OnPlayButtonPressed");

        NewAudioManager.Instance.frontEndButtonSFX.Play();
        SceneTransitionManager.Instance.SceneTransitionSplash();
    }

    public void OnLevelsButtonPressed()
    {
        NewAudioManager.Instance.frontEndButtonSFX.Play();
        CanvasManager.Instance.ShowCanvasLevelSelect();
        Destroy(this.gameObject);
    }

    public void OnSettingsButtonPressed()
    {
        NewAudioManager.Instance.frontEndButtonSFX.Play();
        CanvasManager.Instance.ShowCanvasSettings();
        Destroy(this.gameObject);
    }

    public void OnQuitButtonPressed() // Will exit game (works for builds only).
    {
        Application.Quit();
    }
}
