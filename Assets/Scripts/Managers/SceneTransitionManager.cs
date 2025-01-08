using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    [SerializeField] Image transitionSplash;

    public float fadeDuration = 10f;

    private void Awake()
    {
        Instance = this;
        transitionSplash.color = new Color(transitionSplash.color.r, transitionSplash.color.g, transitionSplash.color.b,
            0);
    }

    public void SceneTransitionSplash()
    {
        StartCoroutine(TransitionFade(0));
    }

    public IEnumerator TransitionFade(float alpha)
    {
        alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeDuration;
            transitionSplash.color = new Color(transitionSplash.color.r, transitionSplash.color.g,
                transitionSplash.color.b, alpha); // Updates alpha 
            yield return null; // Wait for the next frame
        }
        SceneMgr.Instance.SceneTransition();
    }
}
