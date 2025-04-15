using UnityEngine;

public class BoardBehavior : MonoBehaviour
{
    float count;
    float originSpawnDistance;
    bool isStopped = false;
    public int currentBeat;
    public float movementSpeed;
    public static int boardcount = 0;
    public int mycount = 0;
    Vector3 originPos;
    Vector3 lastPos;
    Vector3 targetPos;
    GameObject testPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init()
    {
        this.transform.position = LevelManager.Instance.GetTrackPointTransform(0).position;
        mycount = boardcount;
        boardcount++;
        if (mycount == 0)
        {
            testPoint = new GameObject("GussyTest");
        }
        StartMovement();
    }
    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.Instance.isPaused) count += Time.deltaTime;
        //if (mycount == 0) Debug.Log(count / 60f / LevelManager.Instance.level.soTrack.bpm);
        if (currentBeat >= 0)
        {
            transform.position = Vector3.Lerp(lastPos, targetPos, count / ( 60f / LevelManager.Instance.level.soTrack.bpm));
        }
        if(isStopped && (count / (60f / LevelManager.Instance.level.soTrack.bpm)) >= 1)
        {
            StopMovement();
        }
    }
    void StartMovement()
    {
        currentBeat = 0;
        UpdateMovementTarget();
        BeatManager.beatUpdated += UpdateMovementTarget;
    }
    void UpdateMovementTarget()
    {
        
        lastPos = transform.position;
        //targetPos = originPos + (Vector3.back * LevelManager.Instance.spawnDistance / LevelManager.beatsToPlayer) * currentBeat;
        targetPos = LevelManager.Instance.GetTrackPointTransform(currentBeat).position;
        if (mycount == 0)
        {
            testPoint.transform.position = targetPos;
            Debug.Log("updated mvmt");
        }
        if (currentBeat > LevelManager.beatsToPlayer)
        {
            isStopped = true;
            BeatManager.beatUpdated -= UpdateMovementTarget;
        }
        count = 0;
        currentBeat++;
    }

    private void StopMovement()
    {
        //BeatManager.beatUpdated -= UpdateMovementTarget;
        foreach (var interactable in this.GetComponentsInChildren<BaseInteractableBehavior>())
        {
            if(interactable.isActiveAndEnabled)
            {
                interactable.InteractableMissed();
            }
        }
        gameObject.SetActive(false);

    }
    private void OnDestroy()
    {

        BeatManager.beatUpdated -= UpdateMovementTarget;
    }
}