using UnityEngine;
using System.Collections;

// Controls the hight of the terrain and stage transitions
public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("Variables to Adjust")]
    public float ascendFullDistance;
    public float ascendCheckDistance;
    public float ascendCheckTime;

    [Header("Variables to Set")]
    public GameObject terrain;

    int currentStage = 0;
    Vector3 originPos;
    Vector3 currentPos;
    Vector3 targetPos;
    float count = 0f;

    void Awake()
    {
        instance = this;
    }

    // Sets the starting positions for the terrain;
    void Start()
    {
        originPos = terrain.transform.position;
        currentPos = terrain.transform.position;
        targetPos = terrain.transform.position;

        StartCoroutine(COAscend(true));
    }

    // Moves the terrain to the target position over time.
    void Update()
    {
        count += Time.deltaTime;
        terrain.transform.position = Vector3.Lerp(currentPos, targetPos, count);
        currentPos = terrain.transform.position;
    }

    // Takes the current sigil amount to check if it is high enough to clear the stage (passing the answer as a bool into COAscend())
    void StageThresholdReached(APManager _currentSigilInfo)
    {
        StartCoroutine(COAscend(_currentSigilInfo.curSigil >= currentStage + 1));
    }

    // Raises and/or lowers the terrain and changes currentStage depending on if the stage was passed
    IEnumerator COAscend(bool _success)
    {
        count = 0f;
        if (_success)
        {
            targetPos = new Vector3(currentPos.x, currentPos.y - ascendFullDistance, currentPos.z);
            originPos = new Vector3(originPos.x, originPos.y - ascendFullDistance, originPos.z);
            currentStage++;
        }
        else
        {
            targetPos = new Vector3(currentPos.x, currentPos.y - ascendCheckDistance, currentPos.z);
            yield return new WaitForSeconds(ascendCheckTime);
            count = 0f;
            targetPos = originPos;
        }
    }
}
