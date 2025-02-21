using UnityEngine;
using UnityEngine.VFX;

public class AuraFXBehavior : MonoBehaviour
{
    public static AuraFXBehavior Instance;
    [SerializeField]
    float maxSize = .5f;
    [SerializeField]
    float maxAlpha;
    [SerializeField]
    int maxParticlesSpawnRate;
    [SerializeField]
    int maxIncrementCount;

    float initialSize;
    float initialAlpha;
    float initialParticlesSpawnRate;
    float incrementCount;
    float sizeIncrement;
    float alphaIncrement;
    float particlesSpawnRateIncrement;
    VisualEffect VFXInstance;

    void Awake()
    {
        Instance = this;

        incrementCount = 0;
        sizeIncrement = maxSize / maxIncrementCount;
        alphaIncrement = maxAlpha / maxIncrementCount;
        particlesSpawnRateIncrement = maxParticlesSpawnRate / maxIncrementCount;
        VFXInstance = GetComponent<VisualEffect>();
        initialSize = VFXInstance.GetFloat("Size");
        initialAlpha = VFXInstance.GetFloat("Alpha");
        initialParticlesSpawnRate = VFXInstance.GetFloat("ParticleSpawnRate");
    }

    public void IncreaseAuraVFX()
    {
        // Increases count, plays the VFX effect, and increases the size by the current count multiplied by an increment value
        if (incrementCount < maxIncrementCount)
        {
            incrementCount += .4f;
            VFXInstance.Play();
            VFXInstance.SetFloat("Size", initialSize + (incrementCount * sizeIncrement));
            VFXInstance.SetFloat("Alpha", initialAlpha + (incrementCount * alphaIncrement));
            VFXInstance.SetFloat("ParticleSpawnRate", initialParticlesSpawnRate + (incrementCount * particlesSpawnRateIncrement));
            
        }
    }

    public void ResetAuraVFX()
    {
        // Resets the count back to 0 and stops the VFX effect from playing
        incrementCount = 0;
        VFXInstance.Stop();
    }
}
