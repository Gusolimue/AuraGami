using UnityEngine;
using System.Collections;

public class AvatarTargetVFXYata : MonoBehaviour
{
    public bool isCollidingWithTarget = false;

    public bool ShouldTriggerVFX()
    {
        if (isCollidingWithTarget)
        {
            isCollidingWithTarget = false; // Consume the trigger
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            isCollidingWithTarget = true;
            //StartCoroutine(ManageBool());
        }
    }

    IEnumerator ManageBool()
    {
        yield return new WaitForSeconds(.5f);
        isCollidingWithTarget = false;
    }
}
