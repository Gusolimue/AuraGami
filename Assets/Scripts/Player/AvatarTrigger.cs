using UnityEngine;

public class AvatarTrigger : MonoBehaviour
{
    AvatarBehavior ab;
    private void Awake()
    {
        if(GetComponentInParent<AvatarBehavior>() != null)
        {
            ab = GetComponentInParent<AvatarBehavior>();
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (ab != null)
    //    {
    //        if (other.GetComponentInParent())
    //        {

    //        }
    //    }
    //}
}
