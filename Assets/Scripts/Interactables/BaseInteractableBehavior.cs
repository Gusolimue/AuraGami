using UnityEngine;
using System.Collections;
//Base Script to source for all in game interactables (targets and hazards)
public enum eSide { left, right }
public class BaseInteractableBehavior : MonoBehaviour
{
    public int moveSpeed;
    eSide side;

    public virtual void InitInteractable(eSide _eSide)
    {
        side = _eSide;
        
    }

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

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<PulseBehavior>())
        {
            GetComponentInParent<BaseInteractableBehavior>().AvatarCollision();
            //ScoreManager.Instance.scoreNum++;
            //ScoreManager.Instance.ChangeScore();
            //Destroy(gameObject);
        }
        if (other.CompareTag("Circle"))
        {
            GetComponentInParent<BaseInteractableBehavior>().CircleCollision();
        }
    }
}
