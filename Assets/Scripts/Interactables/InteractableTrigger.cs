using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    bool leftTriggered = false;
    bool rightTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AvatarBehavior>() != null && GetComponentInParent<BaseInteractableBehavior>() != null)
        {
            if(other.GetComponent<AvatarBehavior>().side == GetComponentInParent<BaseInteractableBehavior>().side
                || GetComponentInParent<BaseInteractableBehavior>().side == eSide.any)
            {
                GetComponentInParent<BaseInteractableBehavior>().AvatarCollision();
            }
            else if (GetComponentInParent<BaseInteractableBehavior>().side == eSide.both)
            {
                if (other.GetComponent<AvatarBehavior>().side == eSide.left) leftTriggered = true;
                if (other.GetComponent<AvatarBehavior>().side == eSide.right) rightTriggered = true;
                if (leftTriggered == rightTriggered == true) GetComponentInParent<BaseInteractableBehavior>().AvatarCollision();
            }
            
        }
    }
}
