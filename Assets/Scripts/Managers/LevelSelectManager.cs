using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance;
    public GameObject[] levelOrbs;
    public GameObject[] levelOrbContainers;
    public int whichLevel;

    private void Awake()
    {
        Instance = this;
        SetLevelOrbs(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Level_Exploration"))
        {
            whichLevel = 1;

            other.gameObject.SetActive(false);
            SetLevelOrbs(true);
            ExplorationTransition();
            Debug.Log("LEVEL_EXPLORATION");
        }

        if (other.CompareTag("Level_Freedom"))
        {
            whichLevel = 2;

            other.gameObject.SetActive(false);
            SetLevelOrbs(true);
            FreedomTransition();
            Debug.Log("LEVEL_FREEDOM");
        }
    }

    public void ExplorationTransition()
    {
        FrontEnd.Instance.levelColor = FrontEnd.Instance.explorationColor;
        FrontEnd.Instance.playButton_BG.color = FrontEnd.Instance.levelColor;

        if (whichLevel == 1)
        {
            levelOrbs[1].SetActive(true);
        }
    }

    public void FreedomTransition()
    {
        FrontEnd.Instance.levelColor = FrontEnd.Instance.freedomColor;
        FrontEnd.Instance.playButton_BG.color = FrontEnd.Instance.levelColor;

        if (whichLevel == 2)
        {
            levelOrbs[0].SetActive(true);
        }
    }

    public void SetLevelOrbs(bool set)
    {
        levelOrbs[0].transform.position = levelOrbContainers[0].transform.position;
        levelOrbs[1].transform.position = levelOrbContainers[1].transform.position;
    }

    public void OnLevelOrbClicked()
    {
        Debug.Log("HEY!");
    }
}
