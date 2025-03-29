using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance;
    public GameObject[] levelOrbs;
    public GameObject[] orbs;
    public GameObject[] levelOrbContainers;
    public int whichLevel;

    private void Awake()
    {
        Instance = this;
        whichLevel = 0;
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

            levelOrbs[0].transform.parent = levelOrbContainers[0].transform;
            levelOrbs[0].transform.position = levelOrbContainers[0].transform.position;

            levelOrbs[0].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            levelOrbs[0].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            levelOrbs[1].SetActive(true);
            orbs[1].SetActive(true);
        }

        if (other.CompareTag("Level_Freedom"))
        {
            whichLevel = 2;

            other.gameObject.SetActive(false);
            SetLevelOrbs(true);
            FreedomTransition();
            Debug.Log("LEVEL_FREEDOM");

            levelOrbs[1].transform.parent = levelOrbContainers[1].transform;
            levelOrbs[1].transform.position = levelOrbContainers[1].transform.position;

            levelOrbs[1].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            levelOrbs[1].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            levelOrbs[0].SetActive(true);
            orbs[0].SetActive(true);
        }
    }

    public void ExplorationTransition()
    {
        FrontEnd.Instance.levelColor = FrontEnd.Instance.explorationColor;
        FrontEnd.Instance.playButton_BG.color = FrontEnd.Instance.levelColor;

    }

    public void FreedomTransition()
    {
        FrontEnd.Instance.levelColor = FrontEnd.Instance.freedomColor;
        FrontEnd.Instance.playButton_BG.color = FrontEnd.Instance.levelColor;
    }

    public void SetLevelOrbs(bool set)
    {
        if (whichLevel == 0)
        {
            levelOrbs[0].transform.parent = levelOrbContainers[0].transform;
            levelOrbs[1].transform.position = levelOrbContainers[1].transform.position;

            levelOrbs[1].transform.parent = levelOrbContainers[1].transform;
            levelOrbs[1].transform.position = levelOrbContainers[1].transform.position;
        }

       /* if (whichLevel == 1) 
        {
            levelOrbs[1].transform.parent = levelOrbContainers[1].transform;
            levelOrbs[1].transform.position = levelOrbContainers[1].transform.position;

            levelOrbs[1].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            levelOrbs[1].SetActive(true);
        }
        if (whichLevel == 2) 
        {
            levelOrbs[0].transform.parent = levelOrbContainers[0].transform;
            levelOrbs[0].transform.position = levelOrbContainers[0].transform.position;

            levelOrbs[0].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            levelOrbs[0].SetActive(true);
        }*/
    }

    public void OnExplorationOrbGrabbed()
    {
        Debug.Log("HEY!");
    }
}
