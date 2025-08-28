using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
//Base Script to source for all in game interactables (targets and hazards)
public enum eSide { left, right, any, both }
public class BaseInteractableBehavior : MonoBehaviour
{
    [Header("Variables to Set")]
    public Renderer interactableRenderer;
    public TargetBaseModelSelect targetBase;
    [Header("Variables to Call")]
    public eSide side;
    public eTargetType type;
    public Interactable interactable;
    public int stageIndex;
    public int boardIndex;

    internal GameObject interactableObject;

    //called whenever the interactable is first instantiated
    public virtual void InitInteractable(eSide _eSide, int _stage, int _board, /*int*/ Interactable _interactable)
    {
        stageIndex = _stage;
        boardIndex = _board;
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
    //Method called when the board detects an interactable has been missed
    public virtual void InteractableMissed()
    {
        APManager.Instance.DecreaseAP(1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_target_miss);
        StopTarget();

    }
    //Method called when object's trigger collides with avatar
    public virtual void AvatarCollision(AvatarBehavior avatarBehavior)
    {
        Debug.Log("target collision");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_target_hit);
        switch (side)
        {
            case eSide.left:
            case eSide.right:
                HapticsManager.Instance.TriggerSimpleVibration(side, .1f, .1f);
                break;
            case eSide.both:
                HapticsManager.Instance.TriggerSimpleVibration(side, .5f, .1f);
                break;
        }
        
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
    //Method called to permanently disable a target
    internal void StopTarget()
    {
        gameObject.SetActive(false);
    }
}
