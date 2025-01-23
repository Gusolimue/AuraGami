using UnityEngine;
using TMPro;
//sets and tracks the player's streak
public class StreakManager : MonoBehaviour
{
    [Header("Variables to Adjust")]
    [SerializeField]
    int maxStreak = 6;
    //[Header("Variables to Set")]
    [Header("Variables to Call")]
    public static StreakManager Instance;
    public int currentStreak;

    private void Awake()
    {
        Instance = this;
        ResetStreak();
    }
    //adds to the player streak
    public void AddStreak()
    {
        if (currentStreak < maxStreak)
        {
            currentStreak++;
        }
        Hud.Instance.SetStreakText();
    }
    //resets the player streak
    public void ResetStreak()
    {
        currentStreak = 0;
        Hud.Instance.SetStreakText();
    }
}
