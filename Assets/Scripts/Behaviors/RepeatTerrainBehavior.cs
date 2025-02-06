using UnityEngine;

// Moves and repeats the terrain's position to appear like an infinite loop.
public class RepeatTerrainBehavior : MonoBehaviour
{
    [SerializeField]
    private float tmpSpeed = 300;

    private Vector3 startPos;
    private float repeatLength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        repeatLength = transform.localScale.z * 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * tmpSpeed);

        if (transform.position.z < startPos.z - repeatLength / 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z);
        }
    }

}
