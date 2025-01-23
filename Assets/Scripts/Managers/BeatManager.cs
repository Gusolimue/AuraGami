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
    [SerializeField] UnityEvent trigger;
    int lastInterval;
    public float GetIntervalLength(float _bpm)
    {
        return 60f / (_bpm * steps);
    }
    public void CheckForNewInterval (float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)         
        {
            lastInterval = Mathf.FloorToInt(interval);
            trigger.Invoke();
        }
    }
}
