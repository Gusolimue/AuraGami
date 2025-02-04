using UnityEngine;
using System.Collections;
//Base Script to source for all in game interactables (targets and hazards)
public enum eSide { left, right, any }
public class BaseInteractableBehavior : MonoBehaviour
{
    public Color leftColor;
    public Color rightColor;
    public Renderer interactableRenderer;
    public int moveSpeed;
    eSide side;

    float originSpawnDistance;
    float currentSpawnDistance;
    Vector3 lastPos;
    Vector3 targetPos;
    public virtual void InitInteractable(eSide _eSide)
    {
        side = _eSide;
        switch (side)
        {
            case eSide.left:
                //interactableRenderer.material.color = leftColor;
                break;
            case eSide.right:
                //interactableRenderer.material.color = rightColor;
                break;
            case eSide.any:
                break;
            default:
                break;
        }
    }

    void UpdateMovementTarget()
    {

    }
    void Update()
    {
        // Moves instantiated targets and obstacles foward

        transform.position = Vector3.Lerp(lastPos, targetPos, 1f);
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

    internal virtual void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<AvatarBehavior>())
        {
            if(other.GetComponent<AvatarBehavior>().side == side || other.GetComponent<AvatarBehavior>().side == eSide.any)
            {
                AvatarCollision();
            }
        }
    }
}
