using UnityEngine;

public class SplineTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collisiondetected");
    }
}
