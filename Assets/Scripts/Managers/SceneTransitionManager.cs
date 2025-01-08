using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    [SerializeField] Image transitionSplash;

    private void Awake()
    {
        Instance = this;
        transitionSplash.gameObject.SetActive(false);
    }

    public void SceneTransitionSplash()
    {
        transitionSplash.gameObject.SetActive(true);
    }
}
