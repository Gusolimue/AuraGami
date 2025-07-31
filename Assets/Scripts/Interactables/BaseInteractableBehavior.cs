using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
//Base Script to source for all in game interactables (targets and hazards)
public enum eSide { left, right, any, both }
public class BaseInteractableBehavior : MonoBehaviour
{
    //[SerializeField] static Material leftMat;
    //[SerializeField] static Material rightMat;
    //[SerializeField] static Material anyMat;
    //[SerializeField] static Material bothMat;
    public Renderer interactableRenderer;
    public TargetBaseModelSelect targetBase;
    internal GameObject interactableObject;
    public eSide side;
    public eTargetType type;

    [SerializeField] float fadeOutTime;
    Color startColor;
    Color endColor;
    float startTime;
    bool isFading;
    [HideInInspector] public bool dontDestroy;

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

        interactableObject = targetBase.SelectModel(side);
        List<Material> tmpMats = new List<Material>(0);
        
        switch (side)
        {
            case eSide.left:
                tmpMats.Add(Resources.Load("Materials/" + "Red", typeof(Material)) as Material);
                tmpMats.Add(Resources.Load("Materials/" + "RedGlow", typeof(Material)) as Material);
                break;
            case eSide.right:
                tmpMats.Add(Resources.Load("Materials/" + "Blue", typeof(Material)) as Material);
                tmpMats.Add(Resources.Load("Materials/" + "BlueGlow", typeof(Material)) as Material);
                break;
            case eSide.any:
                tmpMats.Add(Resources.Load("Materials/" + "Orange", typeof(Material)) as Material);
                tmpMats.Add(Resources.Load("Materials/" + "Orange", typeof(Material)) as Material);
                break;
            case eSide.both:
                tmpMats.Add(Resources.Load("Materials/" + "Purple", typeof(Material)) as Material);
                tmpMats.Add(Resources.Load("Materials/" + "PurpleGlow", typeof(Material)) as Material);
                break;
            default:
                break;
        }
        interactableObject.GetComponent<MeshRenderer>().SetMaterials(tmpMats);

    }
    public virtual void InteractableMissed()
    {
        //Debug.Log("player missed");
        APManager.Instance.DecreaseAP(1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_target_miss);
        StopTarget();

    }
    //Method called when object's trigger collides with avatar
    public virtual void AvatarCollision(AvatarBehavior avatarBehavior)
    {
        Debug.Log("target collision");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_target_hit);
        HapticsManager.Instance.TriggerSimpleVibration(side, .1f, .1f);
        if (avatarBehavior.side == eSide.left)
        {
            APVFXManager.Instance.APVfxSpawnNagini(this.transform.position, APManager.Instance.multLevels[APManager.Instance.GetStreakIndex()] * 1);
        }
        else
        {
            APVFXManager.Instance.APVfxSpawnYata(this.transform.position, APManager.Instance.multLevels[APManager.Instance.GetStreakIndex()] * 1);
        }
        StopTarget();
    }

    internal void StopTarget()
    {
        gameObject.SetActive(false);
    }

    public void FadeOutTarget()
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
