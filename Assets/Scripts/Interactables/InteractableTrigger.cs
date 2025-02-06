using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AvatarBehavior>() != null && GetComponentInParent<BaseInteractableBehavior>() != null)
        {
            if(other.GetComponent<AvatarBehavior>().side == GetComponentInParent<BaseInteractableBehavior>().side
                || GetComponentInParent<BaseInteractableBehavior>().side == eSide.any)
            {
                GetComponentInParent<BaseInteractableBehavior>().AvatarCollision();
            }
            
        }
    }
}
