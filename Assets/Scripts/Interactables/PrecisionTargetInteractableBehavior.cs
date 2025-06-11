using UnityEngine;

// Interactable behavior for precision targets that act as like a hazard when hit inaccurately, and as a normal target when hit accurately
public class PrecisionTargetInteractableBehavior : BaseInteractableBehavior
{
    [Header("Variables to Adjust")]
    public int damageAmount = 1;

    // Check if the avatar is colliding with the inner circle (target) or just the outer ring (hazard)
    public override void AvatarCollision(AvatarBehavior avatarBehavior)
    {
        // Create a temp box collider to check if the inner circle is being collided with
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(transform.localScale.x / 8, transform.localScale.y / 8, transform.localScale.z / 8), Quaternion.identity);
        Debug.Log("Precision target has hit: " + hitColliders.Length + " colliders");
        if (hitColliders.Length > 1)
        {
            // Act like a normal target
            Debug.Log("Hit the target");
            APManager.Instance.IncreaseAP();
            base.AvatarCollision(avatarBehavior);
        }
        else
        {
            // Act like a hazard
            Debug.Log("Missed the mark");
            APManager.Instance.DecreaseAP(1);
            if (avatarBehavior != null) avatarBehavior.ObstacleCollision();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_obstacle_hit);
            StopTarget();
        }
    }    
}
