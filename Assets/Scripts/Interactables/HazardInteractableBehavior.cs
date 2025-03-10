using UnityEngine;
//Interactable Behavior for hazards that damage player
public class HazardInteractableBehavior : BaseInteractableBehavior
{
    [Header("Variables to Adjust")]
    public int damageAmount = 1; // at 1, obstacles will damage ap the amount of 1 target. 2 = the amount of 2 targets, etc
    //[Header("Variables to Set")]
    //[Header("Variables to Call")]

    public override void AvatarCollision()
    {
        APManager.Instance.DecreaseAP(1);
        Destroy(gameObject);
    }
}
