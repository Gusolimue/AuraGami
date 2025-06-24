using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class HapticsManager : MonoBehaviour
{
    public static HapticsManager Instance;
    public HapticImpulsePlayer leftImpulsePlayer;
    public HapticImpulsePlayer rightImpulsePlayer;

    void Awake()
    {
        Instance = this;
    }

    public void TriggerVibration(bool leftController, float amplitude, float duration)
    {
        if (leftController)
        {
            leftImpulsePlayer.SendHapticImpulse(amplitude, duration);
        }
        else
        {
            rightImpulsePlayer.SendHapticImpulse(amplitude, duration);
        }
    }
}
