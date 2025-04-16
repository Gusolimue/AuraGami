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
    //float playerCircDiameter;
    //float avatarCircDiameter;

    public bool DisableMovement;

    [Header("Variables to Set")]
    //[SerializeField]
    //RectTransform playerCircRectTransform;
    //[SerializeField]
    //RectTransform avatarCircRectTransform;

    [SerializeField]
    Transform playerCircTransform;
    [SerializeField]
    public Transform avatarCircTransform;
    
    [SerializeField]
    Transform rightControllerTransform;
    [SerializeField]
    Transform leftControllerTransform;

    [SerializeField]
    public GameObject rightAvatar;
    [SerializeField]
    public GameObject leftAvatar;

    GameObject rightObject;
    GameObject leftObject;

    [Header("Variables to Call")]
    public static AvatarManager Instance;


    float scaleMult;

    private void Awake()
    {
        Instance = this;
        GameObject tmpObject = Instantiate(new GameObject("Movement Targets"), this.transform);
        rightObject = new GameObject("Right Movement Target");
        leftObject = new GameObject("Left Movement Target");
        rightObject.transform.SetParent(tmpObject.transform);
        leftObject.transform.SetParent(tmpObject.transform);
        playerCircTransform.localScale *= playerDiameter;
        avatarCircTransform.localScale *= avatarDiameter;
    }
    private void Start()
    {
        scaleMult = avatarDiameter / playerDiameter;
        //playerCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerCircDiameter);
        //playerCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, playerCircDiameter);
        //avatarCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, avatarCircDiameter);
        //avatarCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, avatarCircDiameter);
    }
    private void Update()
    {
        if(!DisableMovement)
        {
            rightObject.transform.position = GetAvatarPos(true);
            leftObject.transform.position = GetAvatarPos(false);
        }
        rightAvatar.transform.localRotation = Quaternion.LookRotation(rightObject.transform.position + (Vector3.forward / 10), Vector3.up);
        rightAvatar.transform.position = (rightObject.transform.position);
        leftAvatar.transform.localRotation = Quaternion.LookRotation(leftObject.transform.position + (Vector3.forward / 10), Vector3.up);
        leftAvatar.transform.position = (leftObject.transform.position);
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
