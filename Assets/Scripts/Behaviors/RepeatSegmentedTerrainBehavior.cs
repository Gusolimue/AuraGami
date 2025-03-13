using System.Collections.Generic;
using UnityEngine;

public class RepeatSegmentedTerrainBehavior : MonoBehaviour
{
    [SerializeField]
    public float tempoSpeed = 300;

    [SerializeField]
    private int terrainOffset;

    [SerializeField]
    private GameObject[] terrainPrefabs;

    [SerializeField]
    private GameObject[] terrainCenterPrefabs;

    private Vector3 startPos;
    private float repeatLength;

    private Vector3 terrainPrefabLeftPosition = new Vector3(-60, 0, 0);
    private Vector3 terrainPrefabRightPosition = new Vector3(60, 0, 0);
    private Vector3 terrainPrefabCenterPosition = new Vector3(0, 0, 0);

    List<GameObject> segments = new List<GameObject>();

    private int segmentsMaxCount = 12;
    private int segmentFrontCount = 6;
    private int segmentTotalCount = 0;

    void Start()
    {
        startPos = transform.position;
        repeatLength = transform.localScale.z;

        // Initialize segments
        for (int i = 0; i < segmentsMaxCount; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, (segmentTotalCount + segmentFrontCount) + terrainOffset);
            GameObject segment = SpawnSegment(spawnPosition);
        }
    }

    void Update()
    {
        if (PauseManager.Instance.isPaused == false)
        {
            TerrainMovement();
        }
    }

    void TerrainMovement()
    {
        transform.Translate(Vector3.back * Time.deltaTime * tempoSpeed);
        if (transform.position.z < -(segmentTotalCount * repeatLength) -(terrainOffset * repeatLength))
        {
            //Debug.Log("Spawn new segment");
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, (segmentTotalCount + segmentFrontCount) + terrainOffset);
            GameObject segment = SpawnSegment(spawnPosition);

            // If there are more segments than the maximum number allowed, remove the one in the very back
            if (segments.Count > segmentsMaxCount)
            {
                Destroy(segments[0]);
                segments.RemoveAt(0);
            }
        }
    }

    GameObject SpawnSegment(Vector3 _position)
    {
        GameObject segment = new GameObject("TerrainSegment" + segments.Count);
        segment.transform.parent = transform;
        GameObject leftSegmentPiece = SpawnSegmentPiece(terrainPrefabs, terrainPrefabLeftPosition, true);
        GameObject centerSegmentPiece = SpawnSegmentPiece(terrainCenterPrefabs, terrainPrefabCenterPosition, false);
        GameObject rightSegmentPiece = SpawnSegmentPiece(terrainPrefabs, terrainPrefabRightPosition, true);
        leftSegmentPiece.transform.parent = segment.transform;
        centerSegmentPiece.transform.parent = segment.transform;
        rightSegmentPiece.transform.parent = segment.transform;
        segment.transform.localPosition = _position;

        segments.Add(segment);
        segmentTotalCount++;

        return segment;
    }

    GameObject SpawnSegmentPiece(GameObject[] _prefabList, Vector3 _position, bool _randomRotation)
    {
        // The index of the random terrain prefab that is chosen to spawn
        int terrainPrefabIndex = Random.Range(0, _prefabList.Length);
        GameObject segmentPiece = Instantiate(_prefabList[terrainPrefabIndex]);
        segmentPiece.transform.position = _position;
        if (_randomRotation)
        {
            // The Y rotation of the random terrain prefab chosen, in increments of 90 (0, 90, 180, 270)
            int terrainPrefabRotation = Random.Range(0, 4) * 90;
            segmentPiece.transform.eulerAngles = new Vector3(0, terrainPrefabRotation, 0);
        }
        return segmentPiece;
    }
}
