using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedbackBehavior : MonoBehaviour
{   
    [Range(0,1)]
    public float duration;
    public float intensity;

    public InputActionReference leftHandHaptic;
    public InputActionReference rightHandHaptic;

    public void TriggerLeftHaptic(XRBaseController controller)
    {
        if (intensity > 0) controller.SendHapticImpulse(intensity, duration);
    }   
}
