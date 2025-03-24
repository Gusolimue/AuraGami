using UnityEngine;

public class ExplorationButton_Behavior : MonoBehaviour
{
    [SerializeField] GameObject explorationPlanet;

    private void OnExplorationButtonPressed()
    {
        LevelSelectManager.Instance.whichLevel = 1;
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionPauseSplash();
    }
}
