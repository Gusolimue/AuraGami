using System.Collections;
using UnityEngine;
//this is currently a dummy behavior used for collision detection and instantiating the avatar model

public class AvatarBehavior : MonoBehaviour
{
    //[Header("Variables to Adjust")]
    [Header("Variables to Set")]
    public eSide side;
    public Renderer evolveSphereRenderer;
    Color startColor;
    Color transparentColor;
    Color failColor;
    float evolveTime = 8;
    [SerializeField] GameObject avatarPrefab;
    //[Header("Variables to Call")]
    
    //
    private void Awake()
    {
        transparentColor = new Color(1, 1, 1, 0);
        startColor = new Color(1, 1, 1, 1);
        failColor = new Color(1, .5f, .5f, 1);
        evolveSphereRenderer.material.color = transparentColor;
        //Instantiate(avatarPrefab).transform.SetParent(this.transform);
    }

    public void ObstacleCollision()
    {

    }

    public void TargetCollision()
    {

    }
}
