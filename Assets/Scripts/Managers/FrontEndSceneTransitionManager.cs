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

    private void Awake()
    {
        Instance = this;
        SceneFadeOutTransitionSplash();
    }

    public void SceneFadeInTransitionSplash()
    {
        StartCoroutine(TransitionFadeIn(0));
    }

    public void SceneFadeOutTransitionSplash()
    {
        StartCoroutine(TransitionFadeOut(1));
    }

    //COROUTINES WILL NO FUNCTION UPON RELOADING SCENE,
    public IEnumerator TransitionFadeIn(float alpha)
    {
        alpha = 0f;
        fadeInDuration = 1f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeInDuration;
            transitionSplash.color = new Color(transitionSplash.color.r, transitionSplash.color.g,
                transitionSplash.color.b, alpha); // Updates alpha 
            yield return null; // Wait for the next frame
        }
        if (LoadManager.Instance.whichScene == 0) LoadManager.Instance.LoadScene(eScene.levelFreedom);
        if (LoadManager.Instance.whichScene == 1) LoadManager.Instance.LoadScene(eScene.frontEnd); 
        if (PauseMenu.Instance.isRestarting == true) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    }

}
