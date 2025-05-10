using UnityEngine;
using System.Collections;

public class AvatarTargetVFXNagini : MonoBehaviour
{
    public bool triggerVFX = false;
    private int lastCollide = -1;

    [SerializeField] GameObject vfxContainer;
    private Vector3 triggerPosition;

    public bool ShouldTriggerVFX(out Vector3 pos)
    {
        if (triggerVFX)
        {
            triggerVFX = false;
            //triggerPosition = this.gameObject.transform.position + Vector3.down * 1.5f;
            pos = triggerPosition + Vector3.down * 1.5f;
            return true;
        }
        pos = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            int targetID = other.gameObject.GetInstanceID();

            if (targetID != lastCollide)
            {
                lastCollide = targetID;
                triggerPosition = other.transform.position;
                triggerVFX = true;
            }
        }
    }

    IEnumerator ManageBool()
    {
        yield return new WaitForSeconds(.5f);
        //isCollidingWithTarget = false;
    }
}
