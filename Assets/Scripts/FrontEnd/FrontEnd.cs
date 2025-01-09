using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FrontEnd : MonoBehaviour
{
    [Header("Play Button BG Assets")]
    [SerializeField] Image playButton_BG;
    [SerializeField] GameObject playButton_BG_OnEnter;
    [SerializeField] GameObject playButton_BG_OnPressed;

    private Animator playButton_BG_OnEnter_Anim;
    private Animator playButton_BG_OnExit_Anim;
    private Animator playButton_BG_OnPressed_Anim;

    public float colorChangeDuration = 3f;

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
        playButton_BG.color = new Color(1f, 1f, 1f, 1f);
        //StartCoroutine(PlayButtonBGColorChangeOnEnter(.3f, .5f, .9f, .7f));
    }
    public void OnPlayButtonExit()
    {
        playButton_BG_OnExit_Anim.enabled = true;
        playButton_BG_OnExit_Anim.SetTrigger("OnPlayButtonExit");
        playButton_BG.color = new Color(.3f, .5f, .9f, .7f);
    }

    public void OnPlayButtonPressed()
    {
        playButton_BG_OnPressed_Anim.enabled = true;
        playButton_BG_OnPressed_Anim.SetTrigger("OnPlayButtonPressed");
        Debug.Log("Play Level!");

        NewAudioManager.Instance.frontEndButtonSFX.Play();
        FrontEndSceneTransitionManager.Instance.SceneTransitionSplash();
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

    public IEnumerator PlayButtonBGColorChangeOnEnter(float red, float green, float blue, float alpha)
    {
        red = .3f;
        green = .5f;
        blue = .9f;
        alpha = .7f;

        while (red < 1f && green < 1f && blue < 1f && alpha < 1f)
        {
            red += Time.deltaTime / colorChangeDuration;
            green += Time.deltaTime / colorChangeDuration;
            blue += Time.deltaTime / colorChangeDuration;
            alpha += Time.deltaTime / colorChangeDuration;

            playButton_BG.color = new Color(red, green, blue, alpha); // Updates colors 
            yield return null; // Wait for the next frame
        }
    }
}
