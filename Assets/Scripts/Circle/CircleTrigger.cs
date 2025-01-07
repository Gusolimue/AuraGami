using UnityEngine;

public class CircleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponentInParent<TargetBehavior>())
        {
            other.GetComponentInParent<TargetBehavior>().ObstacleTriggered();
            //PlayerHealth.Instance.healthPoints--;
            //PlayerHealth.Instance.ChangeHealth();

            //ScoreManager.Instance.scoreNum--;
            //ScoreManager.Instance.ChangeScore();
            //Destroy(gameObject);
        }
    }
}
