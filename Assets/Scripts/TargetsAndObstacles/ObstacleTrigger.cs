using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // Destroys obstacles when player characters collide with them.
    {
        if (other.CompareTag("Snake") || other.CompareTag("Crane"))
        {
            GetComponentInParent<TargetBehavior>().ObstacleTriggered();
            //PlayerHealth.Instance.healthPoints--;
            //PlayerHealth.Instance.ChangeHealth();

            //ScoreManager.Instance.scoreNum--;
            //ScoreManager.Instance.ChangeScore();
            //Destroy(gameObject);
        }
        if (other.CompareTag("Circle"))
        {
            GetComponentInParent<TargetBehavior>().CircleCollision();
        }
    }
}
