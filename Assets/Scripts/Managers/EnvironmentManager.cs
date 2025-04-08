using UnityEngine;
using System.Collections.Generic;

// Controls the instantiation of environment prefabs
public class EnvironmentManager : MonoBehaviour
{
    [Header("Variables to Set")]
    [SerializeField] List<GameObject> rightPrefabs;
    [SerializeField] List<GameObject> leftPrefabs;
    [SerializeField] int prefabLength; //300

    [Header("Variables to Adjust")]
    [SerializeField] int spawnDistance; //550
    [SerializeField] int centerOffset; //100 // How far left/right the environment prefabs spawn
    [SerializeField] int destroyBound; //-350
    [SerializeField] int environmentSpeed;

    [SerializeField] List<GameObject> environmentRows;

    //private float startTime;
    //public float movementTime;

    void Start()
    {
        // Spawn the initial three environment prefabs
        SpawnEnvironment(spawnDistance - (prefabLength * 2));
        SpawnEnvironment(spawnDistance - prefabLength);
        SpawnEnvironment(spawnDistance);
    }

    // Controls environment movement and removal
    void Update()
    {
        for (int rowIndex = 0; rowIndex < environmentRows.Count; rowIndex++)
        {
            GameObject eRow = environmentRows[rowIndex];
            if (eRow.transform.position.z <= destroyBound)
            {
                Destroy(eRow);
                environmentRows.RemoveAt(0);
                SpawnEnvironment(spawnDistance);
                rowIndex--;
            }
            eRow.transform.position = new Vector3(eRow.transform.position.x, eRow.transform.position.y, eRow.transform.position.z - Time.deltaTime * environmentSpeed);

            //float tmpElapsedTime = Time.time - startTime;
            //float t = 0f;
            //t = Mathf.Clamp(tmpElapsedTime / movementTime, 0f, 1f);
            //eRow.transform.position = new Vector3(eRow.transform.position.x, eRow.transform.position.y, Mathf.Lerp(spawnDistance, destroyBound, t));
        }
    }

    // Spawns the next row of environment prefabs at the set distance
    private void SpawnEnvironment(int _spawnDistance)
    {
        GameObject environmentRow = new GameObject("EnvironmentRow");
        environmentRow.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _spawnDistance);

        Instantiate(rightPrefabs[Random.Range(0, rightPrefabs.Count)],
            new Vector3(environmentRow.transform.position.x + centerOffset, environmentRow.transform.position.y, environmentRow.transform.position.z),
            Quaternion.identity, environmentRow.transform); // Spawn right prefab

        Instantiate(leftPrefabs[Random.Range(0, leftPrefabs.Count)],
            new Vector3(environmentRow.transform.position.x - centerOffset, environmentRow.transform.position.y, environmentRow.transform.position.z),
            Quaternion.identity, environmentRow.transform); // Spawn left prefab

        environmentRows.Add(environmentRow);

        //startTime = Time.time;
    }
}
