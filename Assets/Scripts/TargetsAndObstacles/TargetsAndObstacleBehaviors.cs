using UnityEngine;
using UnityEngine.InputSystem;


public class TargetsAndObstacleBehaviors : MonoBehaviour
{
    public string playerTag;

    private void OnTriggerEnter(Collider other) // Destroys targets depending on playerTag. 
    {
        if (other.CompareTag(playerTag))
        {
            if (CompareTag("RedObstacle"))
            {
                PlayerHealth.Instance.healthPoints--;
                PlayerHealth.Instance.ChangeHealth();
            }
            ScoreManager.Instance.scoreNum++;
            ScoreManager.Instance.ChangeScore();
            Destroy(gameObject);
        }
    }
}
