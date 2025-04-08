using UnityEngine;
using System.Collections.Generic;

// Controls the instantiation of environment prefabs
public class EnvironmentManager : MonoBehaviour
{
    [Header("Variables to Set")]
    [SerializeField] List<GameObject> rightPrefabs;
    [SerializeField] List<GameObject> leftPrefabs;

    [Header("Variables to Adjust")]
    public int spawnDistance;
    [SerializeField] int centerOffset; // How far left/right the environment prefabs spawn
    [SerializeField] int environmentSpeed;
    [SerializeField] int zBound;

    void Start()
    {
        // Spawn the initial environment prefabs
        SpawnEnvironment(-50);
        SpawnEnvironment(spawnDistance-300);
        SpawnEnvironment(spawnDistance);
    }

    // Spawns the next row of environment prefabs at the set distance
    public void SpawnEnvironment(int _spawnDistance)
    {
        GameObject environmentRow = new GameObject("EnvironmentRow");
        environmentRow.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _spawnDistance);
        environmentRow.AddComponent<EnvironmentMoveAndDestroyBehavior>();
        environmentRow.GetComponent<EnvironmentMoveAndDestroyBehavior>().environmentManager = this;
        environmentRow.GetComponent<EnvironmentMoveAndDestroyBehavior>().speed = environmentSpeed;
        environmentRow.GetComponent<EnvironmentMoveAndDestroyBehavior>().destroyBound = zBound;

        Instantiate(rightPrefabs[Random.Range(0, rightPrefabs.Count)],
            new Vector3(environmentRow.transform.position.x + centerOffset, environmentRow.transform.position.y, environmentRow.transform.position.z),
            Quaternion.identity, environmentRow.transform); // Spawn right prefab

        Instantiate(leftPrefabs[Random.Range(0, leftPrefabs.Count)],
            new Vector3(environmentRow.transform.position.x - centerOffset, environmentRow.transform.position.y, environmentRow.transform.position.z),
            Quaternion.identity, environmentRow.transform); // Spawn left prefab
    }
}
