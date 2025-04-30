using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    [SerializeField] Color noneTransparent;

    public bool isShieldUp;
    public float transitionSpeed = 5f;
    public int shieldPoints;

    private void Awake()
    {
        Instance = this;
        shieldNum = 0;
        shieldBar.value = 0f;
        isShieldUp = false;

        for(int i = 0; i < shieldBarMeter.Length; i++)
        {
            shieldBarMeter[i].color = noneTransparent;
        }
    }

    private void Update()
    {
        /*Color targetColor;
        Vector3 targetScale;

        for (int i = 0; i < shieldBarMeter.Length; i++)
        {
            if (shieldNum >= 0)
            {
                shieldBarMeter[i].color = noneTransparent;
            }
            if (i == shieldNum)
            {
                targetColor = noneTransparent;
                targetScale = shieldSizeChange.transform.localScale;
            }
            else if (i < shieldNum)
            {
                targetColor = transparent;
                targetScale = shieldBarMeter[i].transform.localScale;
            }
            else
            {
                targetColor = transparent;
                targetScale = shieldBarMeter[i].transform.localScale;
            }

            shieldBarMeter[i].color = Color.Lerp(shieldBarMeter[i].color, targetColor, Time.deltaTime * transitionSpeed);
            shieldBarMeter[i].transform.localScale = Vector3.Lerp(
                shieldBarMeter[i].transform.localScale, targetScale, Time.deltaTime * transitionSpeed);
        }*/
    }

    public void IncreaseShield()
    {
        if (shieldNum <= 3) shieldNum++; shieldBar.value += .33f;
    }

    public void DecreaseShield()
    {
        if (shieldNum >= 0) shieldNum--; shieldBar.value -= .33f;
    }

    public void ShieldReset()
    {
        shieldNum = 0;
        shieldBar.value = 0f;
    }

    /*if (shieldNum == 0)
       {
           for (int i = 0; i < shieldBarMeter.Length; i++)
           {
               shieldBarMeter[i].color = Color.Lerp(shieldBarMeter[i].color, noneTransparent,
                Time.deltaTime * transitionSpeed);
               shieldBarMeter[i].transform.localScale = Vector3.Lerp(shieldBarMeter[i].transform.
                   localScale, shieldSizeChange.transform.localScale, Time.deltaTime * transitionSpeed);
           }          
       }

       if (shieldNum == 1)
       {
           shieldBarMeter[0].color = Color.Lerp(shieldBarMeter[0].color, transparent,
               Time.deltaTime * transitionSpeed);


           shieldBarMeter[1].color = Color.Lerp(shieldBarMeter[1].color, noneTransparent,
               Time.deltaTime * transitionSpeed);
           shieldBarMeter[1].transform.localScale = Vector3.Lerp(shieldBarMeter[1].transform.
               localScale, shieldSizeChange.transform.localScale, Time.deltaTime * transitionSpeed);
       }

       if (shieldNum == 2)
       {
           shieldBarMeter[1].color = Color.Lerp(shieldBarMeter[1].color, transparent,
               Time.deltaTime * transitionSpeed);

           shieldBarMeter[2].color = Color.Lerp(shieldBarMeter[2].color, noneTransparent, 
               Time.deltaTime * transitionSpeed);
           shieldBarMeter[2].transform.localScale = Vector3.Lerp(shieldBarMeter[2].transform.
               localScale, shieldSizeChange.transform.localScale, Time.deltaTime * transitionSpeed);
       }

       if (shieldNum == 3)
       {
           shieldBarMeter[2].color = Color.Lerp(shieldBarMeter[2].color, transparent,
               Time.deltaTime * transitionSpeed);
       }*/
}
