using UnityEngine;

public enum eTargetPositions
{
    topLeft, topCenter, topRight, bottomLeft, bottomCenter, bottomRight,
    middleUpperRight, middleUpperLeft, middleBottomLeft, middleBottomRight,
    farLeft, farRight, middleCenter
}
public class BoardBehavior : MonoBehaviour
{
    [Header("Variables to Adjust")]
    public int moveSpeed;
    [Header("Variables to Set")]
    public GameObject[] spawnPositions; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * transform.forward * moveSpeed;
    }
}
