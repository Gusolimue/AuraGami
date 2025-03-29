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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mycount = boardcount;
        boardcount++;
        originPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.Instance.isPaused) count += Time.deltaTime / movementSpeed;
        //if (mycount == 0) Debug.Log(count / 60f / LevelManager.Instance.level.soTrack.bpm);
        if (currentBeat > 0)
        {
            transform.position = Vector3.Lerp(lastPos, targetPos, count / ( 60f / LevelManager.Instance.level.soTrack.bpm));
        }
        if(isStopped && (count / (60f / LevelManager.Instance.level.soTrack.bpm)) >= 1)
        {
            StopMovement();
        }
    }
    public void StartMovement()
    {
        currentBeat = 0;
        BeatManager.beatUpdated += UpdateMovementTarget;
    }
    void UpdateMovementTarget()
    {
        lastPos = transform.position;
        targetPos = originPos + (Vector3.back * LevelManager.Instance.spawnDistance / LevelManager.Instance.beatsToPlayer) * currentBeat;
        //Debug.Log(count);
        if (currentBeat > LevelManager.Instance.beatsToPlayer)
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