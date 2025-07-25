using UnityEngine;
using System.Collections;

// Controls the hight of the terrain and stage transitions
public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public static string stage1StartString = "Stage1Start";
    public static string stage1CheckString = "Stage1Check";

    public static string stage2StartString = "Stage2Start";
    public static string stage2CheckString = "Stage2Check";

    public static string stage3StartString = "Stage3Start";
    public static string stage3CheckString = "Stage3Check";
    public static string endString = "LevelEnd";

    bool stagePassed;
    private void Awake()
    {
        BeatManager.markerUpdated += StageMarkerUpdated;
    }
    private void OnDestroy()
    {

        BeatManager.markerUpdated -= StageMarkerUpdated;
    }
    void StageMarkerUpdated()
    {
        Debug.Log("marker updated");
        string tmpMarker = BeatManager.Instance.timelineInfo.lastMarker;

        if(tmpMarker == stage1CheckString)
        {
            AvatarManager.Instance.evolveBehavior.StartEvolve();
            //AvatarManager.Instance.leftAvatar.GetComponent<AvatarBehavior>().StartEvolve(); 
            //AvatarManager.Instance.rightAvatar.GetComponent<AvatarBehavior>().StartEvolve(); 

            //Debug.Log("player passed: " + APManager.Instance.StagePassCheck());
            //if (!APManager.Instance.StagePassCheck())
            //{
            //    CanvasManager.Instance.ShowCanvasStageFail();
            //    PauseManager.Instance.isPaused = true;
            //    BeatManager.Instance.PauseMusicTMP(true);
            //    //PauseManager.Instance.PauseGame(true);
            //}
            //else
            //{
            //    APManager.Instance.ResetAP();
            //}
        }
        else if (tmpMarker == stage1StartString)
        {
            LevelManager.Instance.StartStage();
        }
        else if (tmpMarker == stage2StartString)
        {
            LevelManager.Instance.StartStage();
        }
        else if (tmpMarker == stage2CheckString)
        {
            AvatarManager.Instance.evolveBehavior.StartEvolve();
            //AvatarManager.Instance.leftAvatar.GetComponent<AvatarBehavior>().StartEvolve();
            //AvatarManager.Instance.rightAvatar.GetComponent<AvatarBehavior>().StartEvolve();

            //if (!APManager.Instance.StagePassCheck())
            //{
            //    //PauseManager.Instance.PauseGame(true);
            //}
            //else
            //{
            //    APManager.Instance.ResetAP();
            //}
        }
        else if (tmpMarker == stage3StartString)
        {
            LevelManager.Instance.StartStage();
        }
        else if (tmpMarker == stage3CheckString)
        {
            AvatarManager.Instance.evolveBehavior.StartEvolve(false, true);
            //AvatarManager.Instance.leftAvatar.GetComponent<AvatarBehavior>().StartEvolve();
            //AvatarManager.Instance.rightAvatar.GetComponent<AvatarBehavior>().StartEvolve();

            //if (!APManager.Instance.StagePassCheck())
            //{
            //    CanvasManager.Instance.ShowCanvasStageFail();
            //    PauseManager.Instance.isPaused = true;
            //    BeatManager.Instance.PauseMusicTMP(true);
            //}
            //else
            //{
            //    APManager.Instance.ResetAP();
            //}
        }
        else if (tmpMarker == endString)
        {
            //Debug.Log("end level");
            //CanvasManager.Instance.ShowCanvasLevelEnd();
            //PauseManager.Instance.isPaused = true;
            //BeatManager.Instance.PauseMusicTMP(true);
        }
    }







































    //[Header("Variables to Adjust")]
    //public float ascendFullDistance; // Determines the height of the environment
    //public float ascendCheckDistance; // Height that environment moves to when you fail to beat the stage.
    //public float ascendCheckTime; // Amount time transitioning from checkDistance and moving down.

    //[Header("Variables to Set")]
    //public GameObject terrain;

    //public int currentStage = 1;
    //public float stageLengthTime;
    //Vector3 originPos;
    //Vector3 currentPos;
    //Vector3 targetPos;
    //float count = 0f;

    //public float stageTimer = 0f; // Length of stage 

    //void Awake()
    //{
    //    Instance = this;
    //}

    //// Sets the starting positions for the terrain;
    //void Start()
    //{
    //    originPos = terrain.transform.position;
    //    currentPos = terrain.transform.position;
    //    targetPos = terrain.transform.position;

    //    StartCoroutine(StageTimer(stageLengthTime));
    //}

    //// Moves the terrain to the target position over time.
    //void Update()
    //{
    //    count += Time.deltaTime;
    //    terrain.transform.position = Vector3.Lerp(currentPos, targetPos, count);
    //    currentPos = terrain.transform.position;
    //}

    //// Raises and/or lowers the terrain and changes currentStage depending on if the stage was passed
    //IEnumerator COAscend(bool _success)
    //{
    //    count = 0f;
    //    if (_success)
    //    {
    //        targetPos = new Vector3(currentPos.x, currentPos.y - ascendFullDistance, currentPos.z);
    //        originPos = new Vector3(originPos.x, originPos.y - ascendFullDistance, originPos.z);
    //        if (currentStage <= 4) StartCoroutine(StageTimer(stageLengthTime));
    //    }
    //    else
    //    {
    //        targetPos = new Vector3(currentPos.x, currentPos.y - ascendCheckDistance, currentPos.z);
    //        yield return new WaitForSeconds(ascendCheckTime);
    //        count = 0f;
    //        targetPos = originPos;

    //        PauseManager.Instance.isPaused = true;
    //        if (APManager.Instance.sigils[2].value == APManager.Instance.sigils[2].maxValue)
    //        {
    //            CanvasManager.Instance.ShowCanvasLevelEnd();
    //        }
    //        else 
    //        {
    //            CanvasManager.Instance.ShowCanvasStageFail();
    //        }
    //    }
    //}

    //// Counts up to a specified time where it then checks to see if the player can progress to the next stage. 
    //public IEnumerator StageTimer(float _stageLengthTime)
    //{
    //    while (stageTimer < _stageLengthTime)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        stageTimer++;
    //    }
    //    stageTimer = 0f;
    //    currentStage++;
    //    if (APManager.Instance.curAP > currentStage) 
    //    {
    //        StartCoroutine(COAscend(true));
    //    }
    //    else 
    //    {
    //        StartCoroutine(COAscend(false));
    //    }
    //}
}
