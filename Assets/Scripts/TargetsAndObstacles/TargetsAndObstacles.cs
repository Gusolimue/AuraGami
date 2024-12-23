using UnityEngine;
using System.Collections;

public class TargetsAndObstacles : MonoBehaviour
{
    void Update()
    {
        // Moves instantiated targets and obstacles foward
        transform.position += Time.deltaTime * transform.forward * 2;
        StartCoroutine(DestroyInstantObjects());
    }

    IEnumerator DestroyInstantObjects() // Destroys obstacles and missed targets.
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
}
