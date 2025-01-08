using UnityEngine;
using UnityEngine.UI;

public class FrontEnd : MonoBehaviour
{
    [SerializeField] GameObject playButton_BG_OnEnter;
    public Animator playButton_BG_OnEnter_Anim;

    private void Awake()
    {
        playButton_BG_OnEnter_Anim = playButton_BG_OnEnter.GetComponent<Animator>();
        playButton_BG_OnEnter_Anim.enabled = false;
    }

    public void OnPlayButtonEnter()
    {
        playButton_BG_OnEnter_Anim.enabled = true;
        playButton_BG_OnEnter_Anim.SetTrigger("OnPlayButtonEnter");
    }
    public void OnPlayButtonExit()
    {
        playButton_BG_OnEnter_Anim.SetTrigger("OnPlayButtonExit");
    }

    public void OnPlayButtonPressed()
    {
        SceneTransitionManager.Instance.SceneTransitionSplash();
    }

    public void OnLevelsButtonPressed()
    {
        AudioManager.Instance.playSong(AudioManager.Instance.songs[1]);
        CanvasManager.Instance.ShowCanvasLevelSelect();
        Destroy(this.gameObject);
    }

    public void OnSettingsButtonPressed()
    {
        CanvasManager.Instance.ShowCanvasSettings();
        Destroy(this.gameObject);
    }

    public void OnQuitButtonPressed() // Will exit game (works for builds only).
    {
        Application.Quit();
    }
}
