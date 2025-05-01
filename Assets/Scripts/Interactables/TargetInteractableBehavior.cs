using UnityEngine;
//interactable behavior for basic targets that imporve the player's streak when hit
public class TargetInteractableBehavior : BaseInteractableBehavior
{
    public override void AvatarCollision(AvatarBehavior avatarBehavior = null)
    {
        APManager.Instance.IncreaseAP();
        base.AvatarCollision();
    }
}
