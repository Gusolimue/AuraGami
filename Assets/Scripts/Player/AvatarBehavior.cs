using UnityEngine;
//this is currently a dummy behavior used for collision detection and instantiating the avatar model

public class AvatarBehavior : MonoBehaviour
{
    //[Header("Variables to Adjust")]
    [Header("Variables to Set")]
    public eSide side;
    public GameObject avatarObject;
    public bool leftController;

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
}
