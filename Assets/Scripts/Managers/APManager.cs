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
    [Header("Sigil Values")]
    public float ap = .1f;
    public float curAP = 0f;
    [Space]
    [Header("Multiplier Values")]
    public float multBuildUp;
    public float maxMult = .5f;
    public float apIncrease = .1f;

    public int curSigil;
    public bool isMaxMult = false;

    private void Awake()
    {
        Instance = this;
        curSigil = 1;  
    }

    public void IncreaseAP()
    {
        curAP += ap;
        multBuildUp += .1f;

        if (curSigil == 1) sigilOne.value += ap; 
        if (curSigil == 2) sigilTwo.value += ap; 
        if (curSigil == 3) sigilThree.value += ap; 

        Debug.Log("AP Earned: " + ap);
        Debug.Log("Current AP Cumulation: " + curAP);
    }

    public void DecreaseAP()
    {
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

        AuraFXBehavior.Instance.ResetAuraVFX();
    }

    public void APBehavior()
    {
        if (multBuildUp >= 1f) 
        {
            if (isMaxMult == false) ap += apIncrease;
            else ap = maxMult;

            AuraFXBehavior.Instance.IncreaseAuraVFX();
            multBuildUp = 0f;
        }

        if (sigilOne.value == 2) curSigil = 2;
        if (sigilTwo.value == 4) curSigil = 3;
    }
}
