using UnityEngine;
using System.Collections.Generic;

// Controls the instantiation of environment prefabs
public class EnvironmentManager : MonoBehaviour
{
    [Header("Variables to Set")]
    [SerializeField] List<GameObject> rightPrefabs;
    [SerializeField] List<GameObject> leftPrefabs;
    [SerializeField] int prefabLength = 300;
    [SerializeField] int centerOffset = 100; // How far left/right the environment prefabs spawn

    [Header("Variables to Adjust")]
    [SerializeField] int spawnDistance = -50;
    [SerializeField] int destroyBound = -350;
    [SerializeField] public static int environmentSpeed;

    [Header("Current Environment Rows")]
    [SerializeField] List<GameObject> environmentRows;

    private int rowCount;

    void Awake()
    {
        rowCount = 0;
        SpawnEnvironment(spawnDistance);
        SpawnEnvironment(spawnDistance);
        SpawnEnvironment(spawnDistance);
        SpawnEnvironment(spawnDistance);

        environmentSpeed = PlayerPrefs.GetInt("terrainSpeed", 5);
    }

    private void Start()
    {
        
    }
    // Controls environment movement and removal
    void Update()
    {
        if (PauseManager.Instance.isPaused == false)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - Time.deltaTime * environmentSpeed);

            for (int rowIndex = 0; rowIndex < environmentRows.Count; rowIndex++)
            {
                GameObject eRow = environmentRows[rowIndex];
                if (eRow.transform.position.z <= destroyBound) // If an environment is out of bounds
                {
                    // Destroy the environment and spawn a new one
                    Destroy(eRow);
                    environmentRows.RemoveAt(0);
                    SpawnEnvironment(spawnDistance);
                    break;
                }
            }
        }
    }

    // Spawns the next row of environment prefabs at the set distance
    private void SpawnEnvironment(int _spawnDistance)
    {
        rowCount++;
        GameObject environmentRow = new GameObject("EnvironmentRow: " + rowCount);
        environmentRow.transform.parent = gameObject.transform;
        environmentRow.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _spawnDistance);

        Instantiate(rightPrefabs[Random.Range(0, rightPrefabs.Count)],
            new Vector3(environmentRow.transform.position.x + centerOffset, environmentRow.transform.position.y, environmentRow.transform.position.z),
            Quaternion.identity, environmentRow.transform); // Spawn right prefab

        Instantiate(leftPrefabs[Random.Range(0, leftPrefabs.Count)],
            new Vector3(environmentRow.transform.position.x - centerOffset, environmentRow.transform.position.y, environmentRow.transform.position.z),
            Quaternion.identity, environmentRow.transform); // Spawn left prefab

        environmentRows.Add(environmentRow);

        spawnDistance += prefabLength;
    }
}
