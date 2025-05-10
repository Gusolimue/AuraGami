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

    public void APVFXSpawn(Vector3 position, float particleCount)
    {
        targetVFX.SetVector3("SpawnPosition", position);
        targetVFX.SetFloat("ParticleCount", particleCount);
        targetVFX.SendEvent("TargetHitEvent");
    }
}
