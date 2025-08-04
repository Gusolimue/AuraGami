using System.Collections;
using UnityEngine;
using EditorAttributes;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class AvatarManager : MonoBehaviour
{
    //Sets the size of the player's interaction circle and the avatar's movement circle. Moves avatars according to the player controllers
    //relative to the size of the circles. Provides a reference point for instantiation around the circle.
    [Header("Variables to Adjust")]
    public float avatarDiameter;

    //public eSide side;


    public bool disableMovement;
    public bool disableAvatarMovement;
    public float smoothing;
    [Header("Variables to Set")]

    [SerializeField]
    Transform playerCircTransform;
    [SerializeField]
    public Transform avatarCircTransform;

    [SerializeField, NamedArray(typeof(eSide))] Transform[] controllerTransforms;
    [NamedArray(typeof(eSide))] public GameObject[] avatarObjects;
    [NamedArray(typeof(eSide))] public SoAvatar[] soAvatars;
    [NamedArray(typeof(eSide))] bool[] avatarMovementDisable;

    public Canvas playerCircCanvas;

    [NamedArray(typeof(eSide))] public GameObject[] cursorObjects;

    public EvolveBehavior evolveBehavior;
    [NamedArray(typeof(eSide))] AvatarBehavior[] avatarBehaviors;

    Vector3 playerCircStartingPos;
    float slowMod = 1f;
    [Header("Variables to Call")]
    public static AvatarManager Instance;

    float scaleMult;

    private void Awake()
    {
        Instance = this;
        playerCircStartingPos = playerCircTransform.position;
        avatarBehaviors = new AvatarBehavior[2];
        cursorObjects = new GameObject[2];
        avatarMovementDisable = new bool[2];
        for (int i = 0; i < avatarBehaviors.Length; i++)
        {
            avatarBehaviors[i] = avatarObjects[i].GetComponent<AvatarBehavior>();
            avatarMovementDisable[i] = false;
        }
        GameObject tmpObject = Instantiate(new GameObject("Cursors"), this.transform);
        for (int i = 0; i < cursorObjects.Length; i++)
        {
            cursorObjects[i] = Instantiate(soAvatars[i].CursorPrefab, tmpObject.transform);
        }
    }
    private void Start()
    {
        SetScaleHeightVis(PlayerPrefs.GetFloat("playCircleScale", .5f), PlayerPrefs.GetFloat("playCircleHeight", .5f), PlayerPrefs.GetInt("toggleCircle", 2));
    }
    private void Update()
    {
        if(!disableMovement)
        {
            for (int i = 0; i < cursorObjects.Length; i++)
            {
                cursorObjects[i].transform.position = GetCursorPos((eSide)i);
            }
        }

        if (!disableAvatarMovement)
        {
            for (int i = 0; i < avatarObjects.Length; i++)
            {
                if (!avatarMovementDisable[i])
                {
                    avatarObjects[i].transform.position = Vector3.Lerp(avatarObjects[i].transform.position, cursorObjects[i].transform.position, Time.deltaTime * smoothing * slowMod);
                    avatarObjects[i].transform.LookAt(cursorObjects[i].transform.position + new Vector3(0, 0, .3f));
                }
            }
        }
    }
    public void SetScaleHeightVis(float playerScale = 1f, float playerHeight = 1f, int playerVis = 2)
    {
        scaleMult = avatarDiameter / playerScale;
        avatarCircTransform.localScale = Vector3.one * avatarDiameter;
        playerCircTransform.localScale = Vector3.one * playerScale;

        playerCircTransform.position = new Vector3(playerCircStartingPos.x, (playerCircStartingPos.y * playerHeight), playerCircStartingPos.z);

        switch (playerVis)
        {
            case 1:
                playerCircCanvas.enabled = true;
                break;
            case 2:
                playerCircCanvas.enabled = false;
                break;
            default:
                playerCircCanvas.enabled = false;
                break;
        }

    }
    //Given a bool to determine which controller. Returns a vector3 of the position the cursor will be set to that update
    Vector3 GetCursorPos(eSide _side)
    {
        Vector3 tmpPos = avatarCircTransform.position;
        tmpPos += (controllerTransforms[(int)_side].transform.position - playerCircTransform.position) * scaleMult;
        tmpPos = new Vector3(tmpPos.x, tmpPos.y, avatarCircTransform.position.z);
        switch (_side)
        {
            case eSide.left:
                tmpPos.x += .15f;
                break;
            case eSide.right:
                tmpPos.x -= .15f;
                break;
            default:
                break;
        }
        return tmpPos;
    }
    public void SetNewAvatars(int _index, float _scale)
    {
        GameObject[] avatarReplacements = new GameObject[2];
        for (int i = 0; i < avatarReplacements.Length; i++)
        {
            avatarReplacements[i] = Instantiate(soAvatars[i].AvatarPrefab[_index]);
            avatarReplacements[i].transform.SetParent(avatarBehaviors[i].avatarObject.transform.parent);
            avatarReplacements[i].transform.localPosition = avatarBehaviors[i].avatarObject.transform.localPosition;
            avatarReplacements[i].transform.localScale *= _scale;
            avatarBehaviors[i].avatarObject.SetActive(false);
            avatarBehaviors[i].avatarObject = avatarReplacements[i];
        }
    }
    public bool StartEvolve(bool _tutorial = false)
    {
        bool tmpBool = APManager.Instance.StagePassCheck();
        StartCoroutine(evolveBehavior.CoEvolve(tmpBool, _tutorial));
        return tmpBool;
    }
    public IEnumerator COTutorialIntro()
    {
        slowMod = .20f;
        TutorialCanvasManager tc = TutorialManager.Instance.tc;
        PauseManager.Instance.openPauseMenuAction.action.performed -= PauseManager.Instance.OnPauseButtonPressed;
        avatarMovementDisable[(int)eSide.left] = true;
        avatarMovementDisable[(int)eSide.right] = true;
        disableAvatarMovement = false;
        Vector3[] avatarStartingPositions = new Vector3[2];
        for (int i = 0; i < avatarStartingPositions.Length; i++)
        {
            avatarStartingPositions[i] = avatarObjects[i].transform.position;
        }
        float timetill = 3f;

        string[] text = { "This Is Yata; The Mind"};
        tc.FadeInText(text);
        avatarMovementDisable[(int)eSide.right] = false;
        yield return new WaitForSeconds(timetill);
        tc.FadeOutText();
        yield return new WaitUntil(() => !tc.textChanging);

        text[0] = "And This Is Nagini; The Body";
        tc.FadeInText(text);
        avatarMovementDisable[(int)eSide.left] = false; 
        yield return new WaitForSeconds(timetill);
        slowMod = 1f;
        tc.FadeOutText();
        yield return new WaitUntil(() => !tc.textChanging);
        slowMod = 1f;
        PauseManager.Instance.openPauseMenuAction.action.performed += PauseManager.Instance.OnPauseButtonPressed;
    }
    //Gus- this is the method that shows the evolution. it accepts a bool, which is determined by wether or not the player passes the stage. it then proceeds to do the start of the evolution, as that is the same wether the player passes or fails the level. then, it plays the pass or fail animation depending on the value of the bool. see A (pass) and B (fail) comments below
    

    public void StartEvolveFinal()
    {
        //StartCoroutine(CoEvolveFinal(APManager.Instance.StagePassCheck()));
    }
}
