using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] targetsAndObstacles;
    public Transform[] spawnPoints;
    public float beatTime = (60/130)*2;
    private float timer;

    void Update()
    {
        if (timer>beatTime)
        {
            // Instantiate objects in random positions.
            GameObject objects = Instantiate(targetsAndObstacles[Random.Range(0, 2)], spawnPoints[Random.Range(0, 3)]);

            // Zeroing out objects in game.
            objects.transform.localPosition = Vector3.zero;

            // Will rotate spawnPoints (which will rotate objects) at different angles.
            //objects.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            timer -= beatTime;
        }
        timer += Time.deltaTime;
    }
}
