using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Holds and changes canvas elements in game
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
    //sets the streak text canvas element
    public void SetStreakText()
    {
        streakText.text = "Streak : " + StreakManager.Instance.currentStreak;
    }
    //sets the health text canvas element
    public void SetHealthText()
    {
        healthText.text = "Health: " + PlayerHealth.Instance.currentHealth;
    }
}
