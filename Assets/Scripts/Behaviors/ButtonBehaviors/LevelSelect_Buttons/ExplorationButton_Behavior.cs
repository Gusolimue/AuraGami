using UnityEngine;

public class ExplorationButton_Behavior : MonoBehaviour
{
    [SerializeField] GameObject explorationPlanet;

    private Animator planetAnim;
    private Animator planetShrinkAnim;

    public void Awake()
    {
       planetAnim = explorationPlanet.GetComponent<Animator>();
       planetShrinkAnim = explorationPlanet.GetComponent <Animator>();

       planetAnim.enabled = false;
       planetShrinkAnim.enabled = false;
    }

    public void OnExplorationButtonEntered()
    {
        planetAnim.enabled = true;
        planetAnim.SetTrigger("LevelHover");
    }

    public void OnExplorationButtonnExit()
    {
        planetShrinkAnim.enabled = true;
        planetShrinkAnim.SetTrigger("LevelNotHover");
    }

    public void OnExplorationButtonPressed()
    {
        LevelSelectManager.Instance.whichLevel = 1;
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionPauseSplash();
    }
}
