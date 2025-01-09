using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    [SerializeField] Image transitionSplash;

    public float fadeInDuration;
    public float fadeOutDuration;

    private void Awake()
    {
        Instance = this;
        //transitionSplash.color = new Color(transitionSplash.color.r, transitionSplash.color.g, transitionSplash.color.b,0);
        StartCoroutine(TransitionFadeOut(1));
    }

    public void SceneTransitionSplash()
    {
        StartCoroutine(TransitionFadeIn(0));
    }

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
        SceneMgr.Instance.SceneTransition();
    }
    public IEnumerator TransitionFadeOut(float alpha)
    {
        alpha = 1f;
        fadeOutDuration = 3f;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / fadeOutDuration;
            transitionSplash.color = new Color(transitionSplash.color.r, transitionSplash.color.g,
                transitionSplash.color.b, alpha); // Updates alpha 
            yield return null; // Wait for the next frame
        }
    }

}
