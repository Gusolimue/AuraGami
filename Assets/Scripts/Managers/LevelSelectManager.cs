using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance;
    public int whichLevel;

    private void Awake()
    {
        Instance = this;
        whichLevel = (int)eScene.frontEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Level_Exploration"))
        {
            whichLevel = (int)eScene.levelExploration;

            other.gameObject.SetActive(false);
            FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
            Debug.Log("LEVEL_EXPLORATION");
        }

        if (other.CompareTag("Level_Freedom"))
        {
            whichLevel = (int)eScene.levelFreedom;

            other.gameObject.SetActive(false);
            FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
            Debug.Log("LEVEL_FREEDOM");

        }
    }
}
