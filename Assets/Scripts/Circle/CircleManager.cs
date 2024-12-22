using UnityEngine;
using UnityEngine.UI;
public enum eControlType {restrictZ, free };
public class CircleManager : MonoBehaviour
{
    public static CircleManager Instance;
    public bool restrictedToCircle = true;

    public float playerCircDiameter;
    public float avatarCircDiameter;

    public RectTransform playerCircRectTransform;
    public RectTransform avatarCircRectTransform;

    public Transform playerCircTransform;
    public Transform avatarCircTransform;

    public Transform rightControllerTransform;
    public Transform leftControllerTransform;

    public GameObject rightAvatar;
    public GameObject leftAvatar;

    float scaleMult;
    private void Awake()
    {
        Instance = this;
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
        rightAvatar.transform.position = GetAvatarPos(true);
        leftAvatar.transform.position = GetAvatarPos(false);
        if (true)
        {
            Debug.Log("restricting right");
            rightAvatar.transform.position = 
                RestrictPointToCircle(rightAvatar.transform.position, avatarCircTransform.position, avatarCircDiameter / 2);
            Debug.Log("restricting left");
            leftAvatar.transform.position = 
                RestrictPointToCircle(leftAvatar.transform.position, avatarCircTransform.position, avatarCircDiameter / 2);
            //if (Vector3.Distance(avatarCircTransform.position, rightAvatar.transform.position) > avatarCircDiameter / 2)
            //{
            //}
            //if (Vector3.Distance(avatarCircTransform.position, leftAvatar.transform.position) > avatarCircDiameter / 2)
            //{
            //}
        }
    }
    public static Vector3 RestrictPointToCircle(Vector3 point, Vector3 circleCenter, float radius)

    {
        float diffX = point.x - circleCenter.x;
        float diffY = point.y - circleCenter.y;
        float distance = Mathf.Sqrt(diffX * diffX + diffY * diffY);

        if (distance > radius)
        {
            float normalizedX = diffX / distance;
            float normalizedY = diffY / distance;

            point.x = circleCenter.x + normalizedX * radius;
            point.y = circleCenter.y + normalizedY * radius;
        }
        return point;
    }
    Vector3 GetAvatarPos(bool right)
    {
        Vector3 returnPos;
        if(right)
        {
            returnPos = avatarCircTransform.position;
            returnPos += (rightControllerTransform.transform.position - playerCircTransform.position) * scaleMult;
            returnPos = new Vector3(returnPos.x, returnPos.y, avatarCircTransform.position.z);
            return returnPos;
        }
        else
        {
            returnPos = avatarCircTransform.position;
            returnPos += (leftControllerTransform.transform.position - playerCircTransform.position) * scaleMult;
            returnPos = new Vector3(returnPos.x, returnPos.y, avatarCircTransform.position.z);
            return returnPos;
        }
    }
}
