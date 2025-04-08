using UnityEngine;

// Controls the movement of the environment spawned from the environmentManager
public class EnvironmentMoveAndDestroyBehavior : MonoBehaviour
{
    public EnvironmentManager environmentManager;
    public int speed;
    public int destroyBound;

    void Update()
    {
        // Current bug where a gap appears between environments, try controlling movement with a lerp and spawn/destroy when it is finished
        transform.Translate(Vector3.back * Time.deltaTime * speed);

        if (transform.position.z <= destroyBound)
        {
            environmentManager.SpawnEnvironment(environmentManager.spawnDistance);
            Destroy(this.gameObject);
        }
    }
}
