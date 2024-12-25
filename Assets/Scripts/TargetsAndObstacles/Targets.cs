using UnityEngine;
using UnityEngine.InputSystem;


public class Targets : MonoBehaviour
{
    public string playerTag;

    private void OnTriggerEnter(Collider other) // Destroys targets depending on playerTag. 
    {
        if (other.CompareTag(playerTag))
        {
            ScoreManager.Instance.scoreNum++;
            ScoreManager.Instance.ChangeScore();
            Destroy(gameObject);
        }
    }
}
