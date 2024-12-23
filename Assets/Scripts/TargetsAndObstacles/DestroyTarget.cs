using UnityEngine;

public class DestroyTarget : MonoBehaviour
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
