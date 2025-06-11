using UnityEngine;
using UnityEngine.VFX;

public class APVFXManager : MonoBehaviour
{
    public static APVFXManager Instance;
    [SerializeField] VisualEffect[] targetVFX;
    [SerializeField] GameObject sigilTarget;

    private void Awake()
    {
        Instance = this;   
    }
    public void APVfxSpawnNagini(Vector3 position, float particleCount)
    {
        targetVFX[0].SetVector3("SpawnPosition", position);
        targetVFX[0].SetFloat("ParticleCount", particleCount);
        targetVFX[1].SetVector3("AttractTarget", sigilTarget.transform.position);
        targetVFX[0].SendEvent("TargetHitEvent");
    }
    public void APVfxSpawnYata(Vector3 position, float particleCount)
    {
        targetVFX[1].SetVector3("SpawnPosition", position);
        targetVFX[1].SetFloat("ParticleCount", particleCount);
        targetVFX[1].SetVector3("AttractTarget", sigilTarget.transform.position);
        targetVFX[1].SendEvent("TargetHitEvent");
    }
    public void APVfxSpawnSigil(Vector3 targetPos)
    {
        targetVFX[1].SetVector3("SpawnPosition", sigilTarget.transform.position);
        targetVFX[1].SetVector3("AttractTarget", targetPos);
        targetVFX[1].SetFloat("ParticleCount", 1);
        targetVFX[1].SendEvent("TargetHitEvent");
    }
}
