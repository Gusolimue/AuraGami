using UnityEngine;
using UnityEngine.UI;
public enum eControlType {restrictZ, free };
public class AvatarManager : MonoBehaviour
{
    //Sets the size of the player's interaction circle and the avatar's movement circle. Moves avatars according to the player controllers
    //relative to the size of the circles. Provides a reference point for instantiation around the circle.
    [Header("Variables to Adjust")]
    public float playerDiameter;
    public float avatarDiameter;
    public static float playerCircDiameter;
    public static float avatarCircDiameter;

    public bool DisableMovement;

    [Header("Variables to Set")]
    [SerializeField]
    RectTransform playerCircRectTransform;
    [SerializeField]
    RectTransform avatarCircRectTransform;

    [SerializeField]
    Transform playerCircTransform;
    [SerializeField]
    public Transform avatarCircTransform;
    
    [SerializeField]
    Transform rightControllerTransform;
    [SerializeField]
    Transform leftControllerTransform;

    [SerializeField]
    GameObject rightAvatar;
    [SerializeField]
    GameObject leftAvatar;

    [Header("Variables to Call")]
    public static AvatarManager Instance;


    float scaleMult;

    private void Awake()
    {
        Instance = this;
        playerCircDiameter = playerDiameter;
        avatarCircDiameter = avatarDiameter;
    }
    private void Start()
    {
        scaleMult = avatarCircDiameter / playerCircDiameter;
        playerCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerCircDiameter);
        playerCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, playerCircDiameter);
        avatarCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, avatarCircDiameter);
        avatarCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, avatarCircDiameter);
    }
    private void Update()
    {
        if(!DisableMovement)
        {
            rightAvatar.transform.position = GetAvatarPos(true);
            leftAvatar.transform.position = GetAvatarPos(false);
        }
    }
    //Given a bool to determine which controller. Returns a vector3 of the position the avatar will be set to that update
    Vector3 GetAvatarPos(bool right)
    {
        Vector3 tmpPos;
        if(right)
        {
            tmpPos = avatarCircTransform.position;
            tmpPos += (rightControllerTransform.transform.position - playerCircTransform.position) * scaleMult;
            tmpPos = new Vector3(tmpPos.x, tmpPos.y, avatarCircTransform.position.z);
            return tmpPos;
        }
        else
        {
            tmpPos = avatarCircTransform.position;
            tmpPos += (leftControllerTransform.transform.position - playerCircTransform.position) * scaleMult;
            tmpPos = new Vector3(tmpPos.x, tmpPos.y, avatarCircTransform.position.z);
            return tmpPos;
        }
    }

    
}
