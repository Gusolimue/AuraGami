using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    bool leftTriggered = false;
    bool rightTriggered = false;
    AvatarBehavior ab;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AvatarBehavior>() != null)
        {
            ab = other.GetComponent<AvatarBehavior>();
        }
        else if (other.GetComponentInParent<AvatarBehavior>() != null)
        {
            ab = other.GetComponentInParent<AvatarBehavior>();
        }

        if (ab != null && GetComponentInParent<BaseInteractableBehavior>() != null)
        {
            if(ab.side == GetComponentInParent<BaseInteractableBehavior>().side || GetComponentInParent<BaseInteractableBehavior>().side == eSide.any)
            {
                GetComponentInParent<BaseInteractableBehavior>().AvatarCollision();
            }
            else if (GetComponentInParent<BaseInteractableBehavior>().side == eSide.both)
            {
                if (ab.side == eSide.left) leftTriggered = true;
                if (ab.side == eSide.right) rightTriggered = true;
                if (leftTriggered == rightTriggered == true) GetComponentInParent<BaseInteractableBehavior>().AvatarCollision();
            }
        }
    }
}
