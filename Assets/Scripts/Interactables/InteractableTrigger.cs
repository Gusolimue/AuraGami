using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    bool leftTriggered = false;
    bool rightTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        AvatarBehavior ab = null;
        if (other.GetComponent<AvatarBehavior>() != null)
        {
            ab = other.GetComponent<AvatarBehavior>();
        }
        else if (other.GetComponentInParent<AvatarBehavior>() != null)
        {
            ab = other.GetComponentInParent<AvatarBehavior>();
        }

        if (ab != null && GetComponentInParent<BaseInteractableBehavior>() != null)
        {
            BaseInteractableBehavior ib = GetComponentInParent<BaseInteractableBehavior>();
            if (ab.side == ib.side || ib.side == eSide.any)
            {
                ib.AvatarCollision(ab);
            }
            else if (ib.side == eSide.both)
            {
                if (ab.side == eSide.left) leftTriggered = true;
                if (ab.side == eSide.right) rightTriggered = true;
                if (leftTriggered == rightTriggered == true) ib.AvatarCollision(ab);
            }
            else if (GetComponentInParent<HazardInteractableBehavior>() != null)
            {
                GetComponentInParent<BaseInteractableBehavior>().AvatarCollision(ab);
            }
        }
        else if (other.GetComponent<PlayerTrigger>() != null)
        {
            if (GetComponentInParent<HazardInteractableBehavior>() != null)
            {
                GetComponentInParent<BaseInteractableBehavior>().AvatarCollision(ab);
            }
        }
    }
}
