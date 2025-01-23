using UnityEngine;
using UnityEngine.InputSystem;


public class NoteTrigger : MonoBehaviour
{
    public string playerTag;

    private void OnTriggerEnter(Collider other) // Destroys targets depending on playerTag. 
    {
        if (other.CompareTag(playerTag))
        {
            GetComponentInParent<TargetBehavior>().TargetTriggered();
            //ScoreManager.Instance.scoreNum++;
            //ScoreManager.Instance.ChangeScore();
            //Destroy(gameObject);
        }
        if (other.CompareTag("Circle"))
        {
            GetComponentInParent<TargetBehavior>().CircleCollision();
        }
    }
}
