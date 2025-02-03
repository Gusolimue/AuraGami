using UnityEngine;
using UnityEngine.UI;

public class APManager : MonoBehaviour
{
    public static APManager Instance;
    [Header("Sigils")]
    [SerializeField] Slider sigilOne;
    [SerializeField] Slider sigilTwo;
    [SerializeField] Slider sigilThree;
    [Space]
    [Header("Sigil Variables")]
    public float ap = .1f;
    public float curAP = 0f;
    //public int apMultiplier = 1;
    public float multBuildUp;

    public int curSigil;

    private void Awake()
    {
        Instance = this;
        curSigil = 1;    
    }

    public void IncreaseAP()
    {
        //ap *= apMultiplier;
        curAP += ap;
        multBuildUp += .1f;

        if (curSigil == 1) sigilOne.value += ap; 
        if (curSigil == 2) sigilTwo.value += ap; 
        if (curSigil == 3) sigilThree.value += ap; 

        Debug.Log("AP Earned: " + ap);
        Debug.Log("Current AP Cumulation: " + curAP);
        //Debug.Log("Mult: " + apMultiplier);
    }

    public void DecreaseAP()
    {
        //apMultiplier = 1;
        curAP -= ap;
        ap = .1f;
        if (curSigil == 1) sigilOne.value -= ap;

        if (curSigil == 2) 
        {
            sigilTwo.value -= ap;
            if (sigilTwo.value <= -1) curSigil = 1; 
        }

        if (curSigil == 3)
        {
            sigilThree.value -= ap;
            if (sigilThree.value <= -1) curSigil = 2;
        }
    }

    public void APBehavior()
    {
        if (multBuildUp >= 1f) 
        {
            //apMultiplier += 1;
            ap += .1f;
            multBuildUp = 0f;
        }

        if (sigilOne.value == 2) curSigil = 2;
        if (sigilTwo.value == 2) curSigil = 3;
    }
}
