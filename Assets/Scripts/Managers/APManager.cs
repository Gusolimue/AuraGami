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
    public int apMultiplier = 1;

    public int curSigil;

    private void Awake()
    {
        Instance = this;
        curSigil = 1;    
    }

    public void IncreaseAP()
    {
        ap *= apMultiplier;
        curAP += ap;
        if (curSigil == 1) sigilOne.value += ap;
        if (curSigil == 2) sigilTwo.value += ap;
        if (curSigil == 3) sigilThree.value += ap;
    }

    public void DecreaseAP()
    {
        apMultiplier = 1;
        curAP -= ap;
        sigilOne.value -= ap;
    }

    public void APBehavior()
    {
        if (curAP <= 1) apMultiplier++;

        if (sigilOne.value == 2) curSigil = 2;
        if (sigilTwo.value == 2) curSigil = 3;
    }
}
