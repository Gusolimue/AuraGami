using UnityEngine;
using System.Collections;

public class SpawnTargetsAndObstacles : MonoBehaviour
{
    public int moveSpeed;

    void Update()
    {
        // Moves instantiated targets and obstacles foward
        transform.position += Time.deltaTime * transform.forward * moveSpeed;
        StartCoroutine(DestroyInstantObjects());
    }

    IEnumerator DestroyInstantObjects() // Destroys obstacles and missed targets.
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
}
