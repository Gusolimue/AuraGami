using UnityEngine;
using UnityEngine.VFX;

public class APVFXManager : MonoBehaviour
{
    public static APVFXManager Instance;
    [SerializeField] VisualEffect[] targetVFX;

    private void Awake()
    {
        Instance = this;   
    }
    public void APVfxSpawnNagini(Vector3 position, float particleCount)
    {
        targetVFX[0].SetVector3("SpawnPosition", position);
        targetVFX[0].SetFloat("ParticleCount", particleCount);
        targetVFX[0].SendEvent("TargetHitEvent");
    }
    public void APVfxSpawnYata(Vector3 position, float particleCount)
    {
        targetVFX[1].SetVector3("SpawnPosition", position);
        targetVFX[1].SetFloat("ParticleCount", particleCount);
        targetVFX[1].SendEvent("TargetHitEvent");
    }
}
