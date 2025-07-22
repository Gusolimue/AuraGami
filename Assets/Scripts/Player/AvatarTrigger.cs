using UnityEngine;
using UnityEngine.Splines;
public class AvatarTrigger : MonoBehaviour
{
    public AvatarBehavior avatarBehavior;
    bool leftTriggered;
    bool rightTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SplineExtrude>(out SplineExtrude se))
        {
            if(other.transform.parent.TryGetComponent<ThreadedTargetInteractableBehavior>(out ThreadedTargetInteractableBehavior ti))
            {
                if (avatarBehavior.side == ti.side || ti.side == eSide.any)
                {
                    ti.onSpline = true;
                    Debug.Log("threadedEnter");
                    HapticsManager.Instance.ToggleVibration(avatarBehavior.side, true, 0.3f);

                    ti.count = 0;
                }
                else if (ti.side == eSide.both)
                {
                    if (avatarBehavior.side == eSide.left) leftTriggered = true;
                    if (avatarBehavior.side == eSide.right) rightTriggered = true;
                    if (leftTriggered == rightTriggered == true)
                    {
                        ti.onSpline = true;
                        Debug.Log("threadedEnter");
                        HapticsManager.Instance.ToggleVibration(avatarBehavior.side, true, 0.3f);
                        ti.count = 0;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SplineExtrude>() != null)
        {
            if (other.transform.parent.TryGetComponent<ThreadedTargetInteractableBehavior>(out ThreadedTargetInteractableBehavior ti))
            {
                if (avatarBehavior.side == ti.side || ti.side == eSide.any)
                {
                    ti.onSpline = false;
                    Debug.Log("threadedExit");
                    HapticsManager.Instance.ToggleVibration(avatarBehavior.side, false);
                }
                else if (ti.side == eSide.both)
                {
                    if (avatarBehavior.side == eSide.left) leftTriggered = false;
                    if (avatarBehavior.side == eSide.right) rightTriggered = false;
                    if (leftTriggered || rightTriggered == false)
                    {
                        ti.onSpline = false;
                        Debug.Log("threadedExit");
                        HapticsManager.Instance.ToggleVibration(avatarBehavior.side, false);
                    }
                }
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<SplineExtrude>() != null)
        {
            if (other.transform.parent.TryGetComponent<ThreadedTargetInteractableBehavior>(out ThreadedTargetInteractableBehavior ti))
            {
                if (avatarBehavior.side == ti.side || ti.side == eSide.any)
                {
                    ti.count = 0;
                }
                else if (ti.side == eSide.both)
                {
                    if (avatarBehavior.side == eSide.left) leftTriggered = true;
                    if (avatarBehavior.side == eSide.right) rightTriggered = true;
                    if (leftTriggered || rightTriggered == true)
                    {
                        ti.count = 0;
                    }
                }
            }
        }
    }
}
