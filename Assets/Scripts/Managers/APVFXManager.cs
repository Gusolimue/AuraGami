using UnityEngine;
using UnityEngine.VFX;

public class APVFXManager : MonoBehaviour
{
    public static APVFXManager Instance;
    [SerializeField] VisualEffect targetVFX;

    private void Awake()
    {
        Instance = this;   
    }

    public void APVFXSpawn()
    {
         targetVFX.SendEvent("TargetHitEvent");
    }
}
