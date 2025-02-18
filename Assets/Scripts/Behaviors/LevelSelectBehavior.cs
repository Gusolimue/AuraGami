using UnityEngine;

public class LevelSelectBehavior : MonoBehaviour
{
    public static LevelSelectBehavior Instance;
    public int whichLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Level_Exploration"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("LEVEL_EXPLORATION");
            whichLevel = 1;
        }
    }
}
