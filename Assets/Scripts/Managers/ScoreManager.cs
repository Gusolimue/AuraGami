using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Variables to Adjust")]
    public int maxStreak = 6;
    //[Header("Variables to Set")]
    [Header("Variables to Call")]
    public static ScoreManager Instance;
    public int currentStreak;

    private void Awake()
    {
        Instance = this;
        ResetStreak();
    }

    public void AddStreak()
    {
        currentStreak++;
        Hud.Instance.SetStreakText();
    }

    public void ResetStreak()
    {
        currentStreak = 0;
        Hud.Instance.SetStreakText();
    }
}
