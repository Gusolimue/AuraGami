using UnityEngine;
using UnityEngine.XR;

public class HapticsManager : MonoBehaviour
{
    public static HapticsManager Instance;

    uint channel = 0;
    bool success;

    void Awake()
    {
        Instance = this;
    }

    public void TriggerVibration(InputDevice controller, float amplitude, float duration)
    {
        UnityEngine.XR.HapticCapabilities capabilities;
        if (controller.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                success = controller.SendHapticImpulse(channel, amplitude, duration);
                if (success)
                {
                    Debug.Log("Vibrating " + controller.name);
                }
                else
                {
                    Debug.Log("Failed to vibrate " + controller.name);
                }
            }
        }
        else
        {
            Debug.Log("Device " + controller.name + " has no haptic capabilities");
        }
    }
}
