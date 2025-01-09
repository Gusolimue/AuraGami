using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Variables to Adjust")]
    [SerializeField] int startingHealth = 4;
    //[Header("Variables to Set")]
    [Header("Variables to Call")]
    public static PlayerHealth Instance;
    public int currentHealth;

    private void Awake()
    {
        Instance = this;
        SetHealth(startingHealth); 
    }
    public void SetHealth(int _amount)
    {
        currentHealth = _amount;
        Hud.Instance.SetHealthText();
    }
    public void TakeDamage(int _amount)
    {
        currentHealth -= _amount;
        Hud.Instance.SetHealthText();
        ScoreManager.Instance.ResetStreak();
    }
}
