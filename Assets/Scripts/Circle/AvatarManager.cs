using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
public enum eControlType {restrictZ, free };
public enum eAvatar {left, right };
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

    public bool disableMovement;
    public bool disableAvatarMovement;

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
        transparentColor = new Color(1, 1, 1, 0);
        startColor = new Color(1, 1, 1, 1);
        failColor = new Color(1, .5f, .5f, 1);
        evolveSphereRenderer.material.color = transparentColor;
    }
    private void Start()
    {
        scaleMult = avatarCircDiameter / playerCircDiameter;
        playerCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerCircDiameter);
        playerCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, playerCircDiameter);
        avatarCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, avatarCircDiameter);
        avatarCircRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, avatarCircDiameter);
        // Temporary for testing, remove later
        //StartCoroutine(CoEvolve(true));
    }
    private void Update()
    {
        if(!disableMovement)
        {
            rightObject.transform.position = GetAvatarPos(true);
            leftObject.transform.position = GetAvatarPos(false);
        }

        if (!disableAvatarMovement)
        {
            rightAvatar.transform.position = (rightObject.transform.position);
            leftAvatar.transform.position = (leftObject.transform.position);
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
    public float count = 0;
    //Gus- this is the method that shows the evolution. it accepts a bool, which is determined by wether or not the player passes the stage. it then proceeds to do the start of the evolution, as that is the same wether the player passes or fails the level. then, it plays the pass or fail animation depending on the value of the bool. see A (pass) and B (fail) comments below
    IEnumerator CoEvolve(bool _pass)
    {
        //Gus- here you will want to take control of the avatars away from the player

        //Gus- and then you will want a while loop here to lerp the avatars together (close but not exactly on top of eachother, as you dont want them clipping through each other)
        yield return new WaitForSeconds(1f);

        Vector3 leftAvatarStartingPosition = leftAvatar.transform.position;
        Vector3 rightAvatarStartingPosition = rightAvatar.transform.position;
        Vector3 startScaleLeft = leftAvatar.transform.localScale;
        Vector3 startScaleRight = rightAvatar.transform.localScale;

        Vector3 offset = new Vector3(.1f, 0, 0);
        float scaleAmt = .8f;
        // take control from player
        disableAvatarMovement = true;
        count = 0;
        while (count < 1)
        {
            count += Time.deltaTime;
            Debug.Log(count);
            // lerp avatars to center
            leftAvatar.transform.position = Vector3.Lerp(leftAvatarStartingPosition, avatarCircTransform.position - offset, count);
            rightAvatar.transform.position = Vector3.Lerp(rightAvatarStartingPosition, avatarCircTransform.position + offset, count);
            leftAvatar.transform.localScale = Vector3.Lerp(startScaleLeft, startScaleLeft *scaleAmt, count);
            rightAvatar.transform.localScale = Vector3.Lerp(startScaleRight, startScaleRight * scaleAmt, count);
            yield return null;
        }
        count = 0;

        while (count < 1)//Gus- these while loops are just to wait for an amount of time within a coroutine without moving on. you can use these instead of "yield return new WaitForSeconds(float);" if theres code youd like to be running each frame the game is waiting. you will want to do things you only want to happen once outside of the loop (like instantiating or deleting avatars), because if they are inside the loop they will happen every frame until the time has passed
        { //Gus- this while loop should be used just for changing the color of the sphere(after the avatars are already together, so the sphere is closing around them
            count += Time.deltaTime;
            // fade in the sphere
            evolveSphereRenderer.material.color = Color.Lerp(transparentColor, startColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
            
            // proceed with sphere fadeout / color change if fail

            yield return null;
        }

        APManager.Instance.ResetAP();
        count = 0;
        yield return new WaitForSeconds(1f); //Gus- this wait is just to give more time for the sequence.
        if (_pass)//Gus A - here is where the pass animation begins. if the variable is true, all it does is fade the orb back to transparency and reset the ap meter.
        {
            //Gus- and then here, outside of the while loop and therefore after you have finished changing the color of the sphere, you can change out the old avatar prefabs for the next evolution. you dont want to do this earlier, because we want the models to stay the same if the player didnt pass.
            evolutionCount++;
            GameObject newLeftAvatarModel = Instantiate(soAvatarLeft.AvatarPrefab[evolutionCount]);
            GameObject newRightAvatarModel = Instantiate(soAvatarRight.AvatarPrefab[evolutionCount]);

            newLeftAvatarModel.transform.SetParent(leftAvatar.transform);
            newLeftAvatarModel.transform.localPosition = avatarBehaviorLeft.avatarObject.transform.localPosition;
            // set newLeftAvatarModel.transform.scale?

            newRightAvatarModel.transform.SetParent(rightAvatar.transform);
            newRightAvatarModel.transform.localPosition = avatarBehaviorRight.avatarObject.transform.localPosition;
            // set newRightAvatarModel.transform.scale?

            Destroy(avatarBehaviorLeft.avatarObject);
            Destroy(avatarBehaviorRight.avatarObject);

            avatarBehaviorLeft.avatarObject = newLeftAvatarModel;
            avatarBehaviorRight.avatarObject = newRightAvatarModel;

            while (count < 1)
            {
                count += Time.deltaTime;
                evolveSphereRenderer.material.color = Color.Lerp(startColor, transparentColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                yield return null;
            }
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
        }

        leftAvatarStartingPosition = leftAvatar.transform.position;
        rightAvatarStartingPosition = rightAvatar.transform.position;

        count = 0;

        while (count < 1)
        {
            count += Time.deltaTime;

            // Lerp avatars back to cursor positions
            leftAvatar.transform.position = Vector3.Lerp(leftAvatarStartingPosition, leftObject.transform.position, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
            rightAvatar.transform.position = Vector3.Lerp(rightAvatarStartingPosition, rightObject.transform.position, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
            leftAvatar.transform.localScale = Vector3.Lerp(startScaleLeft * scaleAmt, startScaleLeft, count);
            rightAvatar.transform.localScale = Vector3.Lerp(startScaleRight * scaleAmt, startScaleRight, count);
            yield return null;
        }
        if(_pass)
        {
            disableAvatarMovement = false;

        }
        else
        {
            CanvasManager.Instance.ShowCanvasStageFail();
            PauseManager.Instance.isPaused = true;
            BeatManager.Instance.PauseMusicTMP(true);
        }

        // Give control of the avatars back to the player
    }
    //Gus- Called by the stage manager at the end of a stages section in the music. the StagePassCheck method returns a bool based on if the player has enough points to pass the stage.
    public void StartEvolve()
    {
        StartCoroutine(CoEvolve(APManager.Instance.StagePassCheck()));
    }
}
