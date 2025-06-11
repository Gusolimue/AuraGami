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
                Debug.Log("threadedEnter");
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
                Debug.Log("threadedExit");
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<SplineExtrude>() != null)
        {
            if (other.GetComponentInParent<ThreadedTargetInteractableBehavior>() != null)
            {
                if (count > .1f)
                {
                    other.GetComponentInParent<ThreadedTargetInteractableBehavior>().onSpline = false;
                    Debug.Log("threadTimeout");
                }
                
                count = 0;
            }
        }
    }
}
