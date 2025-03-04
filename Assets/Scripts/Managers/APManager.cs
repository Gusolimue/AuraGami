using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APManager : MonoBehaviour
{
    public static APManager Instance;
    [Header("Sigils")]
    [SerializeField] Slider sigilOne;
    [SerializeField] Slider sigilTwo;
    [SerializeField] public Slider sigilThree;
    [Space]
    [Header("Sigil Values")]
    public float ap = .1f;
    public float curAP = 0f;
    [Space]
    [Header("Multiplier Values")]
    public float multBuildUp;
    public float maxMult = 0f;
    public float apIncrease = .1f;

    public int curSigil;
    public bool isMaxMult = false;

    public int totalTargets;
    private void Awake()
    {
        Instance = this;
        curSigil = 1;  
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
                        totalTargets++;
                    }
                }
            }
        }
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
        maxMult = 0f;
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
            maxMult += .1f;
            if (maxMult >= .5f) 
            {
                isMaxMult = true;
                maxMult = .5f;
            }

            if (isMaxMult == false) 
            {
                ap += apIncrease;
            }

            AuraFXBehavior.Instance.IncreaseAuraVFX();
            multBuildUp = 0f;
        }

        if (sigilOne.value == 2) curSigil = 2;
        if (sigilTwo.value == 4) curSigil = 3;
    }
}
