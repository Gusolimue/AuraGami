using UnityEngine;
using System.Collections;
//Base Script to source for all in game interactables (targets and hazards)
public enum eSide { left, right, any, both }
public class BaseInteractableBehavior : MonoBehaviour
{
    public Material leftMat;
    public Material rightMat;
    public Material anyMat;
    public Material bothMat;
    public Renderer interactableRenderer;
    public eSide side;

    [SerializeField] float fadeOutTime;
    Color startColor;
    Color endColor;
    float startTime;
    bool isFading;

    public Interactable interactable;
    public int stageIndex;
    public int boardIndex;
    int interactableIndex;
    LevelManager lm;
    internal virtual void Awake()
    {
        lm = LevelManager.Instance;
        isFading = false;
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
                interactableRenderer.sharedMaterial = anyMat;
                break;
            case eSide.both:
                interactableRenderer.sharedMaterial = bothMat;
                break;
            default:
                break;
        }

    }
    public virtual void InteractableMissed()
    {
        //Debug.Log("player missed");
        APManager.Instance.DecreaseAP(.5f);
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

    private void FadeOutTarget()
    {
        startColor = interactableRenderer.material.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        startTime = Time.time;
        isFading = true;
    }

    void Update()
    {
        if (isFading)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp(elapsedTime / fadeOutTime, 0f, 1f);
            Color lerpedColor = Color.Lerp(startColor, endColor, t);
            interactableRenderer.material.color = lerpedColor;
        }
    }
}
