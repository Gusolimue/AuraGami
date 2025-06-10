using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticsManager : MonoBehaviour
{
    public static HapticsManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public void TriggerVibration(XRBaseController controller, float amplitude, float duration)
    {
        controller.SendHapticImpulse(amplitude, duration);
    }
}
