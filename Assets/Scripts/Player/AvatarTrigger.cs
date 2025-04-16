using UnityEngine;
using UnityEngine.Splines;
public class AvatarTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SplineExtrude>() != null)
        {
            if(other.GetComponentInParent<ThreadedTargetInteractableBehavior>() != null)
            {
                other.GetComponentInParent<ThreadedTargetInteractableBehavior>().onSpline = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SplineExtrude>() != null)
        {
            if (other.GetComponentInParent<ThreadedTargetInteractableBehavior>() != null)
            {
                other.GetComponentInParent<ThreadedTargetInteractableBehavior>().onSpline = false;
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<SplineExtrude>() != null)
        {
            if (other.GetComponentInParent<ThreadedTargetInteractableBehavior>() != null)
            {
                other.GetComponentInParent<ThreadedTargetInteractableBehavior>().onSpline = true;
            }
        }
    }
}
