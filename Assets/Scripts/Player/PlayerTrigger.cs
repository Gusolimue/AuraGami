using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HazardInteractableBehavior>() != null)
        {
            other.GetComponent<HazardInteractableBehavior>().AvatarCollision();
        }
    }
}
