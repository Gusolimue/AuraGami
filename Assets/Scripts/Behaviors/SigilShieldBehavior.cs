using UnityEngine;
using UnityEngine.UI;

public class SigilShieldBehavior : MonoBehaviour
{
    public static SigilShieldBehavior Instance;

    [Header("Shield Assets")]
    [SerializeField] Slider shieldBar;
    [SerializeField] Image[] shieldBarMeter;
    public int shieldNum;
    [Space]

    [Header("Shield Effects")]
    [SerializeField] Image shieldSizeChange;
    [SerializeField] Color transparent;

    public bool isShieldDown = true;
    public float transitionSpeed = 5f;

    private void Awake()
    {
        Instance = this;    
    }

    private void Update()
    {
        if (shieldNum == 2)
        {
            shieldBarMeter[0].color = Color.Lerp(shieldBarMeter[0].color, transparent, 
                Time.deltaTime * transitionSpeed);
            shieldBarMeter[0].transform.localScale = Vector3.Lerp(shieldBarMeter[0].transform.
                localScale, shieldSizeChange.transform.localScale, Time.deltaTime * transitionSpeed);
        }
    }

    public void IncreaseShield()
    {

    }

    public void DecreaseShield()
    {

    }
}
