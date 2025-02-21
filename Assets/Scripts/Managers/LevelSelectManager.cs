using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance;
    public GameObject[] levelOrbs;
    public int whichLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Level_Exploration"))
        {
            whichLevel = 1;

            other.gameObject.SetActive(false);
            ExplorationTransition();
            Debug.Log("LEVEL_EXPLORATION");
        }

        if (other.CompareTag("Level_Freedom"))
        {
            whichLevel = 2;

            other.gameObject.SetActive(false);
            FreedomTransition();
            Debug.Log("LEVEL_FREEDOM");
        }
    }

    public void ExplorationTransition()
    {
        FrontEnd.Instance.playButton_BG.color = FrontEnd.Instance.explorationColor;

        if (whichLevel == 1)
        {
            levelOrbs[1].SetActive(true);
        }
    }

    public void FreedomTransition()
    {
        FrontEnd.Instance.playButton_BG.color = FrontEnd.Instance.freedomColor;

        if (whichLevel == 2)
        {
            levelOrbs[0].SetActive(true);
        }
    }
}
