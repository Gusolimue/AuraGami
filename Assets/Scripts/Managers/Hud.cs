using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    [Header("Variables to Adjust")]
    [Header("Variables to Set")]
    public TextMeshProUGUI streakText;
    public TextMeshProUGUI healthText;
    [Header("Variables to Call")]
    public static Hud Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetStreakText()
    {
        streakText.text = "Streak : " + ScoreManager.Instance.currentStreak;
    }
    public void SetHealthText()
    {
        healthText.text = "Health: " + PlayerHealth.Instance.currentHealth;
    }
}
