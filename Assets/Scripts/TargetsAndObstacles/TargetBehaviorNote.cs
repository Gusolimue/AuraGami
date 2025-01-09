using UnityEngine;

public class TargetBehaviorNote : TargetBehavior
{
    public override void TargetTriggered()
    {
        ScoreManager.Instance.AddStreak();
        Destroy(gameObject);
    }
}
