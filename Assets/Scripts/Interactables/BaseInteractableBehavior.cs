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

    public Interactable interactable;
    public int stageIndex;
    public int boardIndex;
    int interactableIndex;
    LevelManager lm;
    internal virtual void Awake()
    {
        lm = LevelManager.Instance;
    }
    public virtual void InitInteractable(eSide _eSide, int _stage, int _board, /*int*/ Interactable _interactable)
    {
        stageIndex = _stage;
        boardIndex = _board;
        //interactableIndex = _interactable;
        //interactable = lm.level.GetStage(stageIndex)[boardIndex].interactables[interactableIndex];
        interactable = _interactable;
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
