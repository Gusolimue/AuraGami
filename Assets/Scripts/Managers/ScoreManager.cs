using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int scoreNum;
    [SerializeField] TextMeshProUGUI scoreTXT;

    private void Awake()
    {
        Instance = this;
        scoreNum = 0;
    }

    public void ChangeScore()
    {
        scoreTXT.text = "Score: " + scoreNum;
    }
}
