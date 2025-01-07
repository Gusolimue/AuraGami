using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{
    public int moveSpeed;

    void Update()
    {
        // Moves instantiated targets and obstacles foward
        transform.position += Time.deltaTime * transform.forward * moveSpeed;
        //StartCoroutine(DestroyInstantObjects());
    }
    public void TargetTriggered()
    {
        ScoreManager.Instance.scoreNum++;
        ScoreManager.Instance.ChangeScore();
        Destroy(gameObject);
    }
    public void ObstacleTriggered()
    {
        PlayerHealth.Instance.healthPoints--;
        PlayerHealth.Instance.ChangeHealth();

        ScoreManager.Instance.scoreNum--;
        ScoreManager.Instance.ChangeScore();
        Destroy(gameObject);
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
