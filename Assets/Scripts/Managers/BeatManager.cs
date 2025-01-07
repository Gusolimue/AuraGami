using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Intervals[] intervals;
    private void Update()
    {
        foreach (Intervals interval in intervals)
        {
            float sampledTime = audioSource.timeSamples / (audioSource.clip.frequency * interval.GetIntervalLength(bpm));
            interval.CheckForNewInterval(sampledTime);
        }
    }

}

[System.Serializable]
public class Intervals
{
    [SerializeField] private float steps;
    [SerializeField] float offsetStep;
    [SerializeField] UnityEvent trigger;
    int lastInterval;
    bool offset = false;
    public float GetIntervalLength(float _bpm)
    {
        if (offsetStep > 0)
        {
            offset = true;
            return 60f / (_bpm * offsetStep);
        }
        else
        {
            return 60f / (_bpm * steps);
        }
    }
    public void CheckForNewInterval (float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)         
        {
            lastInterval = Mathf.FloorToInt(interval);
            if (offset)
            {
                offsetStep = 0;
                offset = false;
            }
            else
            {
                trigger.Invoke();
            }
        }
    }
}
