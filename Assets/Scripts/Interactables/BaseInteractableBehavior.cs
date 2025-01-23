using UnityEngine;
using System.Collections;
//Base Script to source for all in game interactables (targets and hazards)

public class BaseInteractableBehavior : MonoBehaviour
{
    [Header("Variables to Adjust")]
    public int moveSpeed;
    //[Header("Variables to Set")]
    //[Header("Variables to Call")]

    void Update()
    {
        // Moves instantiated targets and obstacles foward
        transform.position += Time.deltaTime * transform.forward * moveSpeed;
    }
    //Method called when object's trigger collides with avatar
    public virtual void AvatarCollision()
    {
        Destroy(this.gameObject);
    }

    //Method called when object's trigger collides with the avatar circle
    public void CircleCollision()
    {
        Destroy(this.gameObject, 2f);
    }
}
