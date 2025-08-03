using UnityEngine;

// Interactable behavior for precision targets that act as like a hazard when hit inaccurately, and as a normal target when hit accurately
public class PrecisionTargetInteractableBehavior : BaseInteractableBehavior
{
    [Header("Variables to Adjust")]
    public int damageAmount = 1;
    public int precisionSize = 8;

    // Check if the avatar is colliding with the inner circle (target) or just the outer ring (hazard)
    public override void AvatarCollision(AvatarBehavior avatarBehavior)
    {
        // Create a temp box collider to check if the inner circle is being collided with
        Collider[] hitColliders = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f),
            new Vector3(transform.localScale.x / precisionSize, transform.localScale.y / precisionSize, transform.localScale.z / 6),
            Quaternion.identity);
        // Check if the avatar is hitting the center
        bool hasHitAvatar = false;
        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.GetComponent<AvatarTrigger>() != null) // If colliding with an avatar
            {
                hasHitAvatar = true;
                break; // Exit the loop
            }
        }
        if (hasHitAvatar) // If an avatar is colliding with the center
        {
            // Act like a normal target
            Debug.Log("Hit the target");
            APManager.Instance.IncreaseAP();
            HapticsManager.Instance.TriggerSimpleVibration(side, .7f, .1f);
            Debug.Log("Precision Target Vibrating");
            base.AvatarCollision(avatarBehavior);
        }
        else
        {
            // Act like a hazard
            Debug.Log("Missed the mark");
            APManager.Instance.DecreaseAP(1);
            HapticsManager.Instance.TriggerSimpleVibration(side, .05f, .1f);
            if (avatarBehavior != null) avatarBehavior.ObstacleCollision();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_obstacle_hit);
            StopTarget();
        }
    }

    // Create a visible wireframe of the precision area in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Check that it is being run in the Editor, so it doesn't try to draw this in Play mode
        if (Application.isEditor)
            // Draw a cube where the precision OverlapBox collider is
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f),
                new Vector3(transform.localScale.x / (precisionSize-2), transform.localScale.y / (precisionSize-2), transform.localScale.z/4));
    }
}
