using UnityEngine;
using System.Collections;

public class AvatarTargetVFX : MonoBehaviour
{
    public bool triggerVFX = false;
    [SerializeField] GameObject vfxContainer;
    private Vector3 triggerPosition;

    public bool ShouldTriggerVFX(out Vector3 pos)
    {
        if (triggerVFX)
        {
            triggerVFX = false;
            triggerPosition = vfxContainer.transform.position;
            pos = triggerPosition;
            return true;
        }
        pos = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            triggerVFX = true;
        }
    }
}
