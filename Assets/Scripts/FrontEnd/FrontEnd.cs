using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FrontEnd : MonoBehaviour
{
    [Header("Play Button BG Assets")]
    [SerializeField] Image playButton_BG;
    [SerializeField] GameObject playButton_BG_OnEnter;
    [SerializeField] GameObject playButton_BG_OnPressed;
    [Space]
    [Header("Play Button BG Colors")]
    public Color whiteColor;
    public Color blueColor;
    private Color currentColor;

    private Animator playButton_BG_OnEnter_Anim;
    private Animator playButton_BG_OnExit_Anim;
    private Animator playButton_BG_OnPressed_Anim;

    public float colorChangeDuration = 3f;

    private void Awake()
    {
        CanvasManager.Instance.ShowCanvasFE();
        // Grabbing the animator from each of these game objects.
        playButton_BG_OnEnter_Anim = playButton_BG_OnEnter.GetComponent<Animator>();
        playButton_BG_OnExit_Anim = playButton_BG_OnEnter.GetComponent<Animator>();
        playButton_BG_OnPressed_Anim = playButton_BG_OnPressed.GetComponent<Animator>();

        //Ensuring the animations don't play on awake.
        playButton_BG_OnEnter_Anim.enabled = false;
        playButton_BG_OnExit_Anim.enabled = false;
        playButton_BG_OnPressed_Anim.enabled = false;

        currentColor = blueColor;
        playButton_BG.color = currentColor;
    }

    public void OnPlayButtonEnter()
    {
        playButton_BG_OnEnter_Anim.enabled = true;
        playButton_BG_OnEnter_Anim.SetTrigger("OnPlayButtonEnter");
        StartCoroutine(PlayButtonBGColorChangeOnEnter());
    }
    public void OnPlayButtonExit()
    {
        playButton_BG_OnExit_Anim.enabled = true;
        playButton_BG_OnExit_Anim.SetTrigger("OnPlayButtonExit");
        StartCoroutine(PlayButtonBGColorChangeOnExit());
    }

    public void OnPlayButtonPressed()
    {
        playButton_BG_OnPressed_Anim.enabled = true;
        playButton_BG_OnPressed_Anim.SetTrigger("OnPlayButtonPressed");
        Debug.Log("Play Level!");

        NewAudioManager.Instance.frontEndButtonSFX.Play();
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
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
        NewAudioManager.Instance.frontEndButtonSFX.Play();
        Application.Quit();
    }


    public IEnumerator PlayButtonBGColorChangeOnEnter()
    {
        currentColor = whiteColor;
        float numGoal = 1f;
        float numStart = 0f;

        while (numStart < numGoal)
        {
            numStart += .01f;

            playButton_BG.color = Color.Lerp(playButton_BG.color, currentColor, 0.1f); // Updates colors 
            yield return null; // Wait for the next frame
        }
    }
    public IEnumerator PlayButtonBGColorChangeOnExit()
    {
        currentColor = blueColor;
        float numGoal = 1f;
        float numStart = 0f;

        while (numStart < numGoal)
        {
            numStart += .01f;

            playButton_BG.color = Color.Lerp(playButton_BG.color, currentColor, 0.1f); // Updates colors 
            yield return null; // Wait for the next frame
        }
    }

}
