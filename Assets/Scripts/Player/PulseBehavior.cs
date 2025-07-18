using UnityEngine;
//Sets and scales the pulse model depending on the player's streak size
public class PulseBehavior : MonoBehaviour
{
    [Header("Variables to Adjust")]
    [SerializeField]
    float scaleAmount = .2f;
    [SerializeField]
    float pulseSpeed = 5f;

    float count = 0;
    float pulseScale = 1f;
    Vector3 originScale;
    void Awake()
    {
        originScale = this.gameObject.transform.localScale;
    }
    //scales the pulse model to the new target smoothly
    void Update()
    {
        pulseScale = 1f + (StreakManager.Instance.currentStreak * scaleAmount);
        count += Time.deltaTime;
        this.gameObject.transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale, originScale * pulseScale, count * pulseSpeed);
    }
    //sets a new target for the player model to scale to
    public void StartPulse()
    {
       count = 0;
       pulseScale = 1f + (StreakManager.Instance.currentStreak * scaleAmount);
    }
}
