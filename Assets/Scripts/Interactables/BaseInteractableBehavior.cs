using UnityEngine;
using System.Collections;
//Base Script to source for all in game interactables (targets and hazards)
public enum eSide { left, right, any }
public class BaseInteractableBehavior : MonoBehaviour
{
    public Material leftMat;
    public Material rightMat;
    public Renderer interactableRenderer;
    public eSide side;

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
                interactableRenderer.sharedMaterial = leftMat;
                break;
            case eSide.right:
                interactableRenderer.sharedMaterial = rightMat;
                break;
            case eSide.any:
                break;
            default:
                break;
        }

    }
    public virtual void InteractableMissed()
    {
        //Debug.Log("player missed");
        APManager.Instance.DecreaseAP();
    }

    //public void StartMovement()
    //{

    //    currentBeat = 0;
    //    BeatManager.beatUpdated += UpdateMovementTarget;
    //}

    //void UpdateMovementTarget()
    //{
    //    lastPos = transform.position;
    //    currentBeat++;
    //    targetPos = originPos + (Vector3.back * LevelManager.Instance.spawnDistance / LevelManager.Instance.beatsToPlayer)*currentBeat;
    //    if (currentBeat > LevelManager.Instance.beatsToPlayer + 1)
    //    {
    //        StopTarget();
    //    }
    //    count = 0;
    //}
    void Update()
    {
    }
    //Method called when object's trigger collides with avatar
    public virtual void AvatarCollision()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_target_hit);
        StopTarget();
    }

    private void StopTarget()
    {
        gameObject.SetActive(false);
        
    }
}
