using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FrontEndSceneTransitionManager : MonoBehaviour // This is an earlier script I made that has had some adjustments over time. It handles the transition fade between scenes.
{
    public static FrontEndSceneTransitionManager Instance;
    [SerializeField] Image transitionSplash;
    [SerializeField] Color[] transitionColors;

    public float fadeInDuration;
    public float fadeOutDuration;

    public bool isTransitioning;

    private void Awake()
    {
        Instance = this;
        SceneFadeOutTransitionSplash(); // To keep things simple, I have the fade out coroutine (which repeats until the while statement is no longer true) play on awake since there is no situation where the fadeout would not happen.
        isTransitioning = false;
    }

    public void SceneFadeInTransitionSplash(int scene, int color) // This method is called by multiple scripts when a scene transition is needed. It starts the TransitionFadeIn coroutine which fades in a plain image to mask loading. 
                                                                  // With this method, using the scene and color integer, I can easily choose which scene to transition to and what color I want the plain image to be. 
    {
        StartCoroutine(TransitionFadeIn(scene));
        transitionSplash.color = transitionColors[color];
    }

    public void SceneFadeInTransitionRestartSplash() // This method starts a coroutine that is identical to the TransitionFadeIn coroutine, but it's specifically for restarting a scene. This, however, is unnecessary.
                                                     // To streamline this, I would create a bool that, when true, would transition to the desired scene. If false, it would restart the scene. These bools would be set via this method and the one above it. 
    {
        StartCoroutine(TransitionFadeInRestart(0));
    }

    public void SceneFadeOutTransitionSplash() // Despite not being called by another script, I have this method just in case that would need to be the case in the future. 
    {
        StartCoroutine(TransitionFadeOut(1));
    }

    public IEnumerator TransitionFadeIn(int scene)
    {
        float alpha = 0f;
        fadeInDuration = 1f;
        isTransitioning = true;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeInDuration;
            transitionSplash.color = new Color(transitionSplash.color.r, transitionSplash.color.g,
                transitionSplash.color.b, alpha); 
            yield return null; 
        }
        AudioManager.Instance.StopMusic();
        LoadManager.Instance.LoadScene((eScene)scene);
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
                transitionSplash.color.b, alpha); 
            yield return null; 
        }
        if(SceneManager.GetActiveScene().buildIndex != (int)eScene.frontEnd)
        {
            if((eScene)SceneManager.GetActiveScene().buildIndex != eScene.splashScene)
            {
                Debug.Log((eScene)SceneManager.GetActiveScene().buildIndex);
                BeatManager.Instance.StartSong();
            }
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
                transitionSplash.color.b, alpha); 
            yield return new WaitForSecondsRealtime(.01f);
        }
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
