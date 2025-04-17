using UnityEngine;
using UnityEngine.Splines;
public class AvatarTrigger : MonoBehaviour
{
    float count;

    private void Update()
    {
        count += Time.deltaTime;
    }
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
                if (count > .2f) other.GetComponentInParent<ThreadedTargetInteractableBehavior>().onSpline = false;
                count = 0;
            }
        }
    }
}
