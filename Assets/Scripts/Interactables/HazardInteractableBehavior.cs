using UnityEngine;
//Interactable Behavior for hazards that damage player
public class HazardInteractableBehavior : BaseInteractableBehavior
{
    [Header("Variables to Adjust")]
    public int damageAmount = 1; // at 1, obstacles will damage ap the amount of 1 target. 2 = the amount of 2 targets, etc
    public Vector3 smallScale = Vector3.one;
    public Vector3 mediumScale = Vector3.one;
    public Vector3 largeScale = Vector3.one;
    //[Header("Variables to Set")]
    //[Header("Variables to Call")]
    public override void InitInteractable(eSide _eSide, int _stage, int _board, Interactable _interactable)
    {
        base.InitInteractable(_eSide, _stage, _board, _interactable);
        switch (interactable.obstacleSize)
        {
            case eSize.small:
                this.transform.localScale = smallScale;
                break;
            case eSize.medium:
                this.transform.localScale = mediumScale;
                break;
            case eSize.large:
                this.transform.localScale = largeScale;
                break;
            default:
                break;
        }
    }
    private void Start()
    {
        if (interactable.obstacleOrientation == eOrientation.horizontal)
        {
            this.transform.Rotate(0, 0, 90);
            Debug.Log("tried to rotate");
        }

    }
    public override void AvatarCollision(AvatarBehavior avatarBehavior = null)
    {
        APManager.Instance.DecreaseAP(1);
        if(avatarBehavior != null)
        {
            avatarBehavior.ObstacleCollision();
        }
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_obstacle_hit);
        StopTarget();
    }
}
