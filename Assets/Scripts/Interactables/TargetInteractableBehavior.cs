using UnityEngine;
//interactable behavior for basic targets that imporve the player's streak when hit
public class TargetInteractableBehavior : BaseInteractableBehavior
{
    public override void AvatarCollision()
    {
        StreakManager.Instance.AddStreak();
        //APManager.Instance.IncreaseAP();
        //APManager.Instance.APBehavior();
        Destroy(gameObject);
    }
}
