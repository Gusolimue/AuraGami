using UnityEngine;
using UnityEngine.InputSystem;


public class TargetTrigger : MonoBehaviour
{
    public string playerTag;

    private void OnTriggerEnter(Collider other) // Destroys targets depending on playerTag. 
    {
        if (other.CompareTag(playerTag))
        {
            GetComponentInParent<BaseInteractableBehavior>().AvatarCollision();
            //ScoreManager.Instance.scoreNum++;
            //ScoreManager.Instance.ChangeScore();
            //Destroy(gameObject);
        }
    }
}
