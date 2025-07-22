using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class HapticsManager : MonoBehaviour
{
    [Header("Haptic Timelines")]
    public HapticTimeline[] testTimeline;
    [Header("Variables To Set")]
    public static HapticsManager Instance;
    public HapticImpulsePlayer leftImpulsePlayer;
    public HapticImpulsePlayer rightImpulsePlayer;

    public bool hapticsOn = true;

    void Awake()
    {
        Instance = this;
    }
    public void TriggerVibrationTimeline(eSide side, HapticTimeline[] hapticTimeline)
    {
        StartCoroutine(COStartHapticTimeline(side, hapticTimeline));
    }
    IEnumerator COStartHapticTimeline(eSide side, HapticTimeline[] hapticTimeline)
    {
        foreach (var tl in hapticTimeline)
        {
            TriggerSimpleVibration(side, tl.amplitude, tl.duration, tl.frequency);
            yield return new WaitForSeconds(tl.duration);
        }
    }
    public void TriggerSimpleVibration(eSide _side, float amplitude, float duration, float frequency = 0f)
    {
        if (hapticsOn)
        {
            switch (_side)
            {
                case eSide.left:
                    leftImpulsePlayer.SendHapticImpulse(amplitude, duration, frequency);
                    break;
                case eSide.right:
                    rightImpulsePlayer.SendHapticImpulse(amplitude, duration, frequency);
                    break;
                case eSide.both:
                    leftImpulsePlayer.SendHapticImpulse(amplitude, duration, frequency);
                    rightImpulsePlayer.SendHapticImpulse(amplitude, duration, frequency);
                    break;
                default:
                    break;
            }
        }
    }

    public void ToggleVibration(eSide _side, bool toggle, float amplitude = 0f, float frequency = 0f)
    {
        if (toggle)
        {
            Debug.Log("Vibration toggled on " + amplitude);
            TriggerSimpleVibration(_side, amplitude, float.MaxValue, frequency);
        }
        else
        {
            Debug.Log("Vibration toggled off");
            TriggerSimpleVibration(_side, amplitude, 0f, frequency);
        }
    }
}

[System.Serializable]
public class HapticTimeline
{
    public float amplitude;
    public float duration;
    public float frequency;
}