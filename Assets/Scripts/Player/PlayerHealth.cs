using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    [SerializeField] TextMeshProUGUI healthPointsTXT;
    public int healthPoints;

    private void Awake()
    {
        Instance = this;
        healthPoints = 4;   
    }

    public void ChangeHealth()
    {
        healthPointsTXT.text = "Health: " + healthPoints;
    }
}
