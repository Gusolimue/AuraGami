using UnityEngine;
using UnityEngine.UI;

public class APManager : MonoBehaviour
{
    [Header("Sigils")]
    [SerializeField] Slider sigilOne;
    [Space]
    public float ap = .1f;
    public float curAP = 0f;
    public int apMultiplier = 1;

    public void IncreaseAP()
    {
        ap *= apMultiplier;
        curAP += ap;
        sigilOne.value += ap;
    }

    public void DecreaseAP()
    {
        apMultiplier = 1;
        curAP -= ap;
        sigilOne.value -= ap;
    }

    public void APMult()
    {
        if (curAP <= 1)
        {
            apMultiplier++;
        }
    }

}
