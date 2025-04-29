using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<HazardInteractableBehavior>() != null)
        {
            collision.collider.GetComponent<HazardInteractableBehavior>().AvatarCollision();
        }
    }
}
