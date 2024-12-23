using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    public string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Blue Hit!");
            Destroy(gameObject);
        }
    }
}
