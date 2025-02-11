using UnityEngine;
//Interactable Behavior for hazards that damage player
public class HazardInteractableBehavior : BaseInteractableBehavior
{
    [Header("Variables to Adjust")]
    public int damageAmount = 1;
    //[Header("Variables to Set")]
    //[Header("Variables to Call")]

    public override void AvatarCollision()
    {
       // PlayerHealth.Instance.TakeDamage(damageAmount);
        Destroy(gameObject);
    }
}
