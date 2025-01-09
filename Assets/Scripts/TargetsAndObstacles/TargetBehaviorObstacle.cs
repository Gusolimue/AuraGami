using UnityEngine;

public class TargetBehaviorObstacle : TargetBehavior
{
    public int damageAmount = 1;
    public override void TargetTriggered()
    {
        PlayerHealth.Instance.TakeDamage(damageAmount);
        Destroy(gameObject);
    }
}
