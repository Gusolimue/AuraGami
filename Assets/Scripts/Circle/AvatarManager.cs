using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
public enum eControlType {restrictZ, free };
public class AvatarManager : MonoBehaviour
{
    //Sets the size of the player's interaction circle and the avatar's movement circle. Moves avatars according to the player controllers
    //relative to the size of the circles. Provides a reference point for instantiation around the circle.
    [Header("Variables to Adjust")]
    public float playerDiameter;
    public float avatarDiameter;
    float playerCircDiameter;
    float avatarCircDiameter;

    //public eSide side;

    Color startColor;
    Color transparentColor;
    Color failColor;

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
    public GameObject rightAvatar;
    [SerializeField]
    public GameObject leftAvatar;

    public GameObject cursorPrefab;
    GameObject rightObject;
    GameObject leftObject;

    public SoAvatar soAvatarLeft;
    public SoAvatar soAvatarRight;

    public AvatarBehavior avatarBehaviorLeft;
    public AvatarBehavior avatarBehaviorRight;

    public Renderer evolveSphereRenderer;

    int evolutionCount;

    [Header("Variables to Call")]
    public static AvatarManager Instance;


    float scaleMult;

    private void Awake()
    {
        Instance = this;
        playerCircDiameter = playerDiameter;
        avatarCircDiameter = avatarDiameter;
        GameObject tmpObject = Instantiate(new GameObject("Movement Targets"), this.transform);
        rightObject = Instantiate(cursorPrefab, tmpObject.transform);
        leftObject = Instantiate(cursorPrefab, tmpObject.transform);
        evolutionCount = 0;
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
            rightObject.transform.position = GetAvatarPos(true);
            leftObject.transform.position = GetAvatarPos(false);
        }
        //rightAvatar.transform.localRotation = Quaternion.LookRotation(rightObject.transform.position + (Vector3.forward / 10), Vector3.up);
        rightAvatar.transform.position = (rightObject.transform.position);
        //leftAvatar.transform.localRotation = Quaternion.LookRotation(leftObject.transform.position + (Vector3.forward / 10), Vector3.up);
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
    //Gus- this is the method that shows the evolution. it accepts a bool, which is determined by wether or not the player passes the stage. it then proceeds to do the start of the evolution, as that is the same wether the player passes or fails the level. then, it plays the pass or fail animation depending on the value of the bool. see A (pass) and B (fail) comments below
    IEnumerator CoEvolve(bool _pass)
    {
        //Gus- here you will want to take control of the avatars away from the player
        //Gus- and then you will want a while loop here to lerp the avatars together (close but not exactly on top of eachother, as you dont want them clipping through each other)
        yield return new WaitForSeconds(1f);
        float count = 0;
        while (count < 1)//Gus- these while loops are just to wait for an amount of time within a coroutine without moving on. you can use these instead of "yield return new WaitForSeconds(float);" if theres code youd like to be running each frame the game is waiting. you will want to do things you only want to happen once outside of the loop (like instantiating or deleting avatars), because if they are inside the loop they will happen every frame until the time has passed
        { //Gus- this while loop should be used just for changing the color of the sphere(after the avatars are already together, so the sphere is closing around them
            count += Time.deltaTime;
            // take control from player

            // lerp avatars to center
            
            // fade in the sphere
            evolveSphereRenderer.material.color = Color.Lerp(transparentColor, startColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
            // change out avatar models for next prefab in the SO scripts
            evolutionCount++;
            GameObject newLeftAvatar = Instantiate(soAvatarLeft.AvatarPrefab[evolutionCount]);
            GameObject newRightAvatar = Instantiate(soAvatarRight.AvatarPrefab[evolutionCount]);

            newLeftAvatar.transform.SetParent(leftAvatar.transform.parent);
            newLeftAvatar.transform.localPosition = leftAvatar.transform.localPosition;

            newRightAvatar.transform.SetParent(rightAvatar.transform.parent);
            newRightAvatar.transform.localPosition = rightAvatar.transform.localPosition;

            
            Destroy(leftAvatar);
            Destroy(rightAvatar);
            // proceed with sphere fadeout / color change if fail

            yield return null;
        }
        count = 0;
        yield return new WaitForSeconds(1f); //Gus- this wait is just to give more time for the sequence.
        if (_pass)//Gus A - here is where the pass animation begins. if the variable is true, all it does is fade the orb back to transparency and reset the ap meter.
        {
            //Gus- and then here, outside of the while loop and therefore after you have finished changing the color of the sphere, you can change out the old avatar prefabs for the next evolution. you dont want to do this earlier, because we want the models to stay the same if the player didnt pass.
            while (count < 1)
            {
                count += Time.deltaTime;
                evolveSphereRenderer.material.color = Color.Lerp(startColor, transparentColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                yield return null;
            }
            //Gus- made this quick change for you. this was only here because i was calling this method twice (one for each avatar) because we're only calling the method once now, we can always reset the ap on success
            //if (eSide.right == side)
            //{
            //    APManager.Instance.ResetAP();
            //}
            APManager.Instance.ResetAP();
        }
        else
        {
            //Gus B -  here is where the fail animation begins. its more complicated, but only slightly. 
            while (count < 1)
            {
                count += Time.deltaTime;
                evolveSphereRenderer.material.color = Color.Lerp(startColor, failColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                yield return null;
            }
            count = 0;
            while (count < 1)
            {
                count += Time.deltaTime;
                evolveSphereRenderer.material.color = Color.Lerp(failColor, transparentColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                yield return null;
            }
            //Gus- see the comment about the similar change above
            //if (eSide.right == side)
            //{
            //    CanvasManager.Instance.ShowCanvasStageFail();
            //    PauseManager.Instance.isPaused = true;
            //    BeatManager.Instance.PauseMusicTMP(true);
            //}
            CanvasManager.Instance.ShowCanvasStageFail();
            PauseManager.Instance.isPaused = true;
            BeatManager.Instance.PauseMusicTMP(true);
        }
    }
    //Gus- Called by the stage manager at the end of a stages section in the music. the StagePassCheck method returns a bool based on if the player has enough points to pass the stage.
    public void StartEvolve()
    {
        StartCoroutine(CoEvolve(APManager.Instance.StagePassCheck()));
    }
}
