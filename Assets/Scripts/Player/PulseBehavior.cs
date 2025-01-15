using UnityEngine;

public class PulseBehavior : MonoBehaviour
{
    [Header("Variables to Adjust")]
    public float scaleAmount = .2f;
    public float pulseSpeed = 5f;
    public float maxStreakScaleMinAmount = 1.1f;
    [Header("Variables to Set")]
    public GameObject pulseObject;
    //[Header("Variables to Call")]

    float count = 0;
    float pulseScale = 1f;
    Vector3 originScale;
    void Awake()
    {
        originScale = pulseObject.transform.localScale;
    }
    void Update()
    {
        pulseScale = 1f + (ScoreManager.Instance.currentStreak * scaleAmount);
        count += Time.deltaTime;
        pulseObject.transform.localScale = Vector3.Lerp(pulseObject.transform.localScale, originScale * pulseScale, count * pulseSpeed);
        //if (pulseObject.activeSelf == true)
        //{
        //    pulseObject.transform.localScale = Vector3.Lerp(transform.localScale, originScale, Time.deltaTime);
        //    if (pulseObject.transform.localScale == originScale) pulseObject.SetActive(false);
        //}
    }

    public void StartPulse()
    {
        count = 0;
       pulseScale = 1f + (ScoreManager.Instance.currentStreak * scaleAmount);
        if (ScoreManager.Instance.currentStreak > 0)
        {
            //pulseObject.SetActive(true);
        }
    }
}
