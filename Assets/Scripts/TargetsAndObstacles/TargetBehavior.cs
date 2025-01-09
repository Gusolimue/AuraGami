using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{
    [Header("Variables to Adjust")]
    public int moveSpeed;
    //[Header("Variables to Set")]
    //[Header("Variables to Call")]

    void Update()
    {
        // Moves instantiated targets and obstacles foward
        transform.position += Time.deltaTime * transform.forward * moveSpeed;
        //StartCoroutine(DestroyInstantObjects());
    }
    public virtual void TargetTriggered()
    {
        Destroy(this.gameObject);
    }

    public void CircleCollision()
    {
        Destroy(this.gameObject, 2f);
    }
    IEnumerator DestroyInstantObjects() // Destroys obstacles and missed targets.
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
}
