using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // Destroys obstacles when player characters collide with them.
    {
        if (other.CompareTag("Snake") || other.CompareTag("Crane"))
        {
            PlayerHealth.Instance.healthPoints--;
            PlayerHealth.Instance.ChangeHealth();

            ScoreManager.Instance.scoreNum--;
            ScoreManager.Instance.ChangeScore();
            Destroy(gameObject);
        }
    }
}
