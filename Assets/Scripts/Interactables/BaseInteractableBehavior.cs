using UnityEngine;
using System.Collections;
//Base Script to source for all in game interactables (targets and hazards)
public enum eSide { left, right, any }
public class BaseInteractableBehavior : MonoBehaviour
{
    public Color leftColor;
    public Color rightColor;
    public Renderer interactableRenderer;
    eSide side;

    float count;
    float originSpawnDistance;
    public int currentBeat;
    Vector3 originPos;
    Vector3 lastPos;
    Vector3 targetPos;
    private void Awake()
    {
        originPos = transform.position;
    }
    public virtual void InitInteractable(eSide _eSide)
    {
        side = _eSide;
        switch (side)
        {
            case eSide.left:
                //interactableRenderer.material.color = leftColor;
                break;
            case eSide.right:
                //interactableRenderer.material.color = rightColor;
                break;
            case eSide.any:
                break;
            default:
                break;
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
        currentBeat++;
        targetPos = originPos + (Vector3.back * LevelManager.Instance.spawnDistance / LevelManager.Instance.beatsToPlayer)*currentBeat;
        if (currentBeat > LevelManager.Instance.beatsToPlayer + 1)
        {
            StopTarget();
        }
        count = 0;
    }
    void Update()
    {
        // Moves instantiated targets and obstacles foward
        count += Time.deltaTime;
        if(currentBeat > 0)
        {
            transform.position = Vector3.Lerp(lastPos, targetPos, 60f / LevelManager.Instance.level.soTrack.bpm * count);
        }
    }
    //Method called when object's trigger collides with avatar
    public virtual void AvatarCollision()
    {
        StopTarget();
    }

    private void StopTarget()
    {
        BeatManager.beatUpdated -= UpdateMovementTarget;
        gameObject.SetActive(false);
        
    }
    internal virtual void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<AvatarBehavior>())
        {
            if(other.GetComponent<AvatarBehavior>().side == side || other.GetComponent<AvatarBehavior>().side == eSide.any)
            {
                AvatarCollision();
            }
        }
    }
}
