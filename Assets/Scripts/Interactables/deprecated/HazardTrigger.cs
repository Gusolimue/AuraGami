using UnityEngine;
//triggers hazard methods on collision
public class HazardTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // Destroys hazards when player characters collide with them.
    {
        if (other.GetComponent<AvatarBehavior>() || other.GetComponentInParent<AvatarBehavior>())
        {
            GetComponentInParent<BaseInteractableBehavior>().AvatarCollision();
        }
    }
}
