using UnityEngine;
using UnityEngine.XR;

public class HapticsManager : MonoBehaviour
{
    public static HapticsManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public void TriggerVibration(InputDevice controller, float amplitude, float duration)
    {
        controller.SendHapticImpulse(1, amplitude, duration);
    }
}
