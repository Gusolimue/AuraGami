using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FrontEndSceneTransitionManager : MonoBehaviour
{
    public static FrontEndSceneTransitionManager Instance;
    [SerializeField] Image transitionSplash;

    public float fadeInDuration;
    public float fadeOutDuration;

    public bool isTransitioning;

    private void Awake()
    {
        Instance = this;
        SceneFadeOutTransitionSplash();
        isTransitioning = false;
    }

    public void SceneFadeInTransitionSplash()
    {
        StartCoroutine(TransitionFadeIn(0));
    }

    public void SceneFadeInTransitionRestartSplash()
    {
        StartCoroutine(TransitionFadeInRestart(0));
    }

    public void SceneFadeOutTransitionSplash()
    {
        StartCoroutine(TransitionFadeOut(1));
    }

    public IEnumerator TransitionFadeIn(float alpha)
    {
        alpha = 0f;
        fadeInDuration = 1f;
        isTransitioning = true;
        //AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_orbSelectionTransition);
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeInDuration;
            transitionSplash.color = new Color(transitionSplash.color.r, transitionSplash.color.g,
                transitionSplash.color.b, alpha); // Updates alpha 
            yield return null; // Wait for the next frame
        }
        AudioManager.Instance.StopMusic();
        LoadManager.Instance.LoadScene((eScene)LevelSelectManager.Instance.whichLevel);
        isTransitioning = false;
    }
    public IEnumerator TransitionFadeOut(float alpha)
    {
        alpha = 1f;
        fadeOutDuration = 1f;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / fadeOutDuration;
            transitionSplash.color = new Color(transitionSplash.color.r, transitionSplash.color.g,
                transitionSplash.color.b, alpha); // Updates alpha 
            yield return null; // Wait for the next frame
        }
        if(SceneManager.GetActiveScene().buildIndex != (int)eScene.frontEnd)
        {
            BeatManager.Instance.StartSong();
        }
    }

    public IEnumerator TransitionFadeInRestart(float alpha)
    {
        alpha = 0f;
        fadeInDuration = 1f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeInDuration;
            transitionSplash.color = new Color(transitionSplash.color.r, transitionSplash.color.g,
                transitionSplash.color.b, alpha); // Updates alpha 
            yield return new WaitForSecondsRealtime(.01f);
        }
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
