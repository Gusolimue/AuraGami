using System.Collections;
using UnityEngine;
//this is currently a dummy behavior used for collision detection and instantiating the avatar model

public class AvatarBehavior : MonoBehaviour
{
    //[Header("Variables to Adjust")]
    [Header("Variables to Set")]
    public eSide side;
    public Renderer evolveSphereRenderer;
    public Animator animator;
    Color startColor;
    Color transparentColor;
    Color failColor;
    float evolveTime = 8;
    [SerializeField] GameObject avatarPrefab;
    public GameObject avatarObject;
    //[Header("Variables to Call")]
    
    //
    private void Awake()
    {
        GetNewAnimator();
        //transparentColor = new Color(1, 1, 1, 0);
        //startColor = new Color(1, 1, 1, 1);
        //failColor = new Color(1, .5f, .5f, 1);
        //startColor = transparentColor;
        //failColor = transparentColor;
        //evolveSphereRenderer.material.color = transparentColor;
        //Instantiate(avatarPrefab).transform.SetParent(this.transform);
    }

    public void ObstacleCollision()
    {
        if(avatarObject.GetComponent<Animator>() != null)
        {
            avatarObject.GetComponent<Animator>().SetTrigger("OnDamage");
        }
        else
        {
            Debug.LogError("No animator detected for "+side+ " avatar!");
        }
    }

    public void StreakEnabled()
    {
        if (avatarObject.GetComponent<Animator>() != null)
        {
            avatarObject.GetComponent<Animator>().SetTrigger("OnStreak");
        }
        else
        {
            Debug.LogError("No animator detected for " + side + " avatar!");
        }
    }

    public void TargetCollision()
    {

    }
    public void GetNewAnimator()
    {
    }
}
