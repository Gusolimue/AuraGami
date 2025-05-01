using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APManager : MonoBehaviour
{
    [Header("For Adjustments")]
    public float[] multLevels;
    public int multIncrementStreak;
    [Range(0,1)]
    public float[] stagePassPercent;

    public float sigilSliderSpeed = 5f;
    private float targetSigilValue = 0f;
    [Space]
    [Header("Cheats")]
    [SerializeField]bool forceSuccess = false;
    [Space]


    [Header("In Game Info")]
    public int curStreak;
    public float curAP;
    [SerializeField] int[] stageTargetTotals;
    [SerializeField] float[] stageTargetValues;

    [Space]
    [Header("To Set/Call")]
    public AuraFXBehavior[] auraFXBehaviors;
    public Slider sigil;
    public static APManager Instance;


    private void Awake()
    {
        Instance = this;
        curAP = 0;
        stageTargetTotals = new int[3];
        stageTargetValues = new float[3];
        UpdateSigils();
        UpdateAuraFX();
    }

    private void Update()
    {
        if (forceSuccess) curAP = 12;
        sigil.value = Mathf.Lerp(sigil.value, targetSigilValue, Time.deltaTime * sigilSliderSpeed);
    }

    public void SetTargetValues()
    {
        for (int c = 0; c < 3; c++)
        {
            List<Board> tmpStage = LevelManager.Instance.level.GetStage(c);
            for (int i = 0; i < tmpStage.Count; i++)
            {
                foreach (var item in tmpStage[i].interactables)
                {
                    if (item.interactableType != eTargetType.regularObstacle)
                    {
                        stageTargetTotals[c]++;
                    }
                }
            }
            stageTargetValues[c] = (2 - stagePassPercent[c]) / stageTargetTotals[c];
        }
    }

    public void IncreaseAP()
    {
        sigilSliderSpeed = 5f;
        curAP += stageTargetValues[Mathf.Clamp(LevelManager.currentStageIndex, 0, stageTargetValues.Length-1)] * multLevels[Mathf.Clamp((curStreak / multIncrementStreak), 0, multLevels.Length - 1 )];
        curAP = Mathf.Clamp(curAP, 0, Mathf.Infinity);
        curStreak += 1;
        UpdateSigils();
        UpdateAuraFX();

        if (sigil.value >= .999f)
        {
            SigilShieldBehavior.Instance.shieldPoints++;

            if (SigilShieldBehavior.Instance.shieldPoints >= 5)
            {
                SigilShieldBehavior.Instance.shieldPoints = 0;
                SigilShieldBehavior.Instance.IncreaseShield();
            }
        }
    }

    public void DecreaseAP(float _percent)
    {
        if (FrontEndSceneTransitionManager.Instance.isTransitioning == false)
        {
            if (SigilShieldBehavior.Instance.shieldNum <= 0)
            {
                sigilSliderSpeed = 5f;
                curAP -= stageTargetValues[Mathf.Clamp(LevelManager.currentStageIndex, 0, stageTargetValues.Length - 1)] * _percent;
                curAP = Mathf.Clamp(curAP, 0, Mathf.Infinity);
                curStreak = 0;
                UpdateSigils();
                UpdateAuraFX();
            }
            else
            {
                SigilShieldBehavior.Instance.DecreaseShield();
            }

        }
     
    }
    //int GetMultIndex()
    //{
    //    return Mathf.Clamp((curStreak / multIncrementStreak), 0, multLevels.Length);
    //}

    public bool StagePassCheck()
    {
        bool tmp = false;
        if (curAP >= /*LevelManager.currentStageIndex*/ 1) tmp = true;
        return tmp;
    }
    public void ResetAP()
    {
        sigilSliderSpeed = 1f;
        curAP = 0;
        UpdateSigils();
        UpdateAuraFX();
        SigilShieldBehavior.Instance.ShieldReset();
    }
    public void UpdateSigils()
    {
        targetSigilValue = Mathf.Clamp01(curAP) * sigil.maxValue;
        //sigil.value = Mathf.Clamp01(curAP) * sigil.maxValue;
        //sigils[0].value = Mathf.Clamp(curAP, 0, 1f) * sigils[0].maxValue;
        //sigils[1].value = Mathf.Clamp(curAP - 1, 0, 1f) * sigils[1].maxValue;
        //sigils[2].value = Mathf.Clamp(curAP - 2, 0, 1f) * sigils[2].maxValue;
    }
    public void UpdateAuraFX()
    {
        foreach (var aura in auraFXBehaviors)
        {

        }
    }




































    //[Header("Sigils")]
    //[SerializeField] Slider sigilOne;
    //[SerializeField] Slider sigilTwo;
    //[SerializeField] public Slider sigilThree;
    //[Space]
    //[Header("Sigil Values")]
    //public float ap = .1f;
    //public float curAP = 0f;
    //[Space]
    //[Header("Multiplier Values")]
    //public float multBuildUp;
    //public float maxMult = 0f;
    //public float apIncrease = .1f;

    //public int curSigil;
    //public bool isMaxMult = false;

    //public int totalTargets;
    //private void Awake()
    //{
    //    Instance = this;
    //    curSigil = 1;  
    //}

    //public void SetTargetValues()
    //{
    //    for (int c = 0; c < 3; c++)
    //    {
    //        List<Board> tmpStage = LevelManager.Instance.level.GetStage(c);
    //        for (int i = 0; i < tmpStage.Count; i++)
    //        {
    //            foreach (var item in tmpStage[i].interactables)
    //            {
    //                if (item.interactableType != eTargetType.regularObstacle)
    //                {
    //                    totalTargets++;
    //                }
    //            }
    //        }
    //    }
    //}
    //public void IncreaseAP()
    //{
    //    curAP += ap;
    //    multBuildUp += .1f;

    //    if (curSigil == 1) sigilOne.value += ap; 
    //    if (curSigil == 2) sigilTwo.value += ap; 
    //    if (curSigil == 3) sigilThree.value += ap; 

    //    Debug.Log("AP Earned: " + ap);
    //    Debug.Log("Current AP Cumulation: " + curAP);
    //}

    //public void DecreaseAP()
    //{
    //    curAP -= ap;
    //    ap = .1f;
    //    maxMult = 0f;
    //    if (curSigil == 1) sigilOne.value -= ap;

    //    if (curSigil == 2) 
    //    {
    //        sigilTwo.value -= ap;
    //        if (sigilTwo.value <= -1) curSigil = 1; 
    //    }

    //    if (curSigil == 3)
    //    {
    //        sigilThree.value -= ap;
    //        if (sigilThree.value <= -1) curSigil = 2;
    //    }

    //    AuraFXBehavior.Instance.ResetAuraVFX();
    //}

    //public void APBehavior()
    //{
    //    if (multBuildUp >= 1f) 
    //    {
    //        maxMult += .1f;
    //        if (maxMult >= .5f) 
    //        {
    //            isMaxMult = true;
    //            maxMult = .5f;
    //        }

    //        if (isMaxMult == false) 
    //        {
    //            ap += apIncrease;
    //        }

    //        AuraFXBehavior.Instance.IncreaseAuraVFX();
    //        multBuildUp = 0f;
    //    }

    //    if (sigilOne.value == 2) curSigil = 2;
    //    if (sigilTwo.value == 4) curSigil = 3;
    //}
}
