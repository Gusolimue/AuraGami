using System.Collections;
using UnityEngine;
using EditorAttributes;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
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

    public Renderer evolveSphereRenderer;
    public Canvas playerCircCanvas;

    int evolutionCount;
    Color startColor;
    Color transparentColor;
    Color failColor;
    [NamedArray(typeof(eSide))] GameObject[] cursorObjects;
    [NamedArray(typeof(eSide))] AvatarBehavior[] avatarBehaviors;
    Vector3 playerCircStartingPos;

    [Header("Variables to Call")]
    public static AvatarManager Instance;
    AudioManager am;

    float scaleMult;

    private void Awake()
    {
        Instance = this;
        playerCircStartingPos = playerCircTransform.position;
        avatarBehaviors = new AvatarBehavior[2];
        cursorObjects = new GameObject[2];
        for (int i = 0; i < avatarBehaviors.Length; i++)
        {
            avatarBehaviors[i] = avatarObjects[i].GetComponent<AvatarBehavior>();
        }
        GameObject tmpObject = Instantiate(new GameObject("Cursors"), this.transform);
        for (int i = 0; i < cursorObjects.Length; i++)
        {
            cursorObjects[i] = Instantiate(soAvatars[i].CursorPrefab, tmpObject.transform);
        }
        evolutionCount = 0;
        transparentColor = new Color(1, 1, 1, 0);
        startColor = new Color(1, 1, 1, 1);
        failColor = new Color(1, .5f, .5f, 1);
        evolveSphereRenderer.material.color = transparentColor;
    }
    private void Start()
    {
        am = AudioManager.Instance;
        SetScaleHeightVis(PlayerPrefs.GetFloat("playCircleScale", 1f), PlayerPrefs.GetFloat("playCircleHeight", 1f), PlayerPrefs.GetInt("toggleCircle", 2));
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
                avatarObjects[i].transform.position = Vector3.Lerp(avatarObjects[i].transform.position, cursorObjects[i].transform.position, Time.deltaTime * smoothing);
                avatarObjects[i].transform.LookAt(cursorObjects[i].transform.position + new Vector3(0, 0, .2f));
            }
        }
    }
    public void SetScaleHeightVis(float playerScale = 1f, float playerHeight = 1f, int playerVis = 1)
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
        return tmpPos;
    }
    public bool readyMove = false;
    public IEnumerator COTutorialIntro()
    {
        PauseManager.Instance.openPauseMenuAction.action.performed -= PauseManager.Instance.OnPauseButtonPressed;
        float count;
        disableAvatarMovement = true;
        Vector3[] avatarStartingPositions = new Vector3[2];
        for (int i = 0; i < avatarStartingPositions.Length; i++)
        {
            avatarStartingPositions[i] = avatarObjects[i].transform.position;
        }
        float timetill = 3f;
        count = 0;
        while (count < timetill)
        {
            count += Time.deltaTime;
            avatarObjects[(int)eSide.right].transform.position = Vector3.Lerp(avatarStartingPositions[(int)eSide.right], cursorObjects[(int)eSide.right].transform.position, count/timetill);
            yield return null;
        }
        count = 0;
        while (count < timetill)
        {
            count += Time.deltaTime;
            avatarObjects[(int)eSide.right].transform.position = Vector3.Lerp(avatarObjects[(int)eSide.right].transform.position, cursorObjects[(int)eSide.right].transform.position, count / timetill);
            avatarObjects[(int)eSide.left].transform.position = Vector3.Lerp(avatarStartingPositions[(int)eSide.left], cursorObjects[(int)eSide.left].transform.position, count/ timetill);
            yield return null;
        }
        disableAvatarMovement = false;
        PauseManager.Instance.openPauseMenuAction.action.performed += PauseManager.Instance.OnPauseButtonPressed;
    }
    //Gus- this is the method that shows the evolution. it accepts a bool, which is determined by wether or not the player passes the stage. it then proceeds to do the start of the evolution, as that is the same wether the player passes or fails the level. then, it plays the pass or fail animation depending on the value of the bool. see A (pass) and B (fail) comments below
    IEnumerator CoEvolve(bool _pass, bool _tutorial = false)
    {
        am.PlaySFX(am.sfx_avatar_evolveStart);
        PauseManager.Instance.openPauseMenuAction.action.performed -= PauseManager.Instance.OnPauseButtonPressed;
        float count;
        //Gus- here you will want to take control of the avatars away from the player

        //Gus- and then you will want a while loop here to lerp the avatars together (close but not exactly on top of eachother, as you dont want them clipping through each other)
        yield return new WaitForSeconds(1f);

        Vector3[] avatarStartingPositions = new Vector3[2];
        for (int i = 0; i < avatarStartingPositions.Length; i++)
        {
            avatarStartingPositions[i] = avatarObjects[i].transform.position;
        }
        Vector3[] avatarStartingScales = new Vector3[2];
        for (int i = 0; i < avatarStartingScales.Length; i++)
        {
            avatarStartingScales[i] = avatarObjects[i].transform.localScale;
        }

        Vector3 offset = new Vector3(-.1f, 0, 0);
        float scaleAmt = .5f;
        // take control from player
        disableAvatarMovement = true;
        count = 0;
        while (count < 1)
        {
            count += Time.deltaTime;
            // lerp avatars to center
            for (int i = 0; i < avatarObjects.Length; i++)
            {
                offset *= -1;
                avatarObjects[i].transform.position = Vector3.Lerp(avatarStartingPositions[i], evolveSphereRenderer.transform.position + offset, count);
            }
            yield return null;
        }
        count = 0;

        while (count < 1)//Gus- these while loops are just to wait for an amount of time within a coroutine without moving on. you can use these instead of "yield return new WaitForSeconds(float);" if theres code youd like to be running each frame the game is waiting. you will want to do things you only want to happen once outside of the loop (like instantiating or deleting avatars), because if they are inside the loop they will happen every frame until the time has passed
        { //Gus- this while loop should be used just for changing the color of the sphere(after the avatars are already together, so the sphere is closing around them
            count += Time.deltaTime;
            // fade in the sphere
            evolveSphereRenderer.material.color = Color.Lerp(transparentColor, startColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
            for (int i = 0; i < avatarObjects.Length; i++)
            {
                offset *= -1;
                avatarObjects[i].transform.localScale = Vector3.Lerp(avatarStartingScales[i], avatarStartingScales[i] * scaleAmt, count);
            }

            // proceed with sphere fadeout / color change if fail

            yield return null;
        }

        APManager.Instance.DrainAP();
        while (APManager.Instance.isDraining) yield return null;
        count = 0;
        yield return new WaitForSeconds(1f); //Gus- this wait is just to give more time for the sequence.
        if (_pass)//Gus A - here is where the pass animation begins. if the variable is true, all it does is fade the orb back to transparency and reset the ap meter.
        {
            am.PlaySFX(am.sfx_avatar_evolveSuccess);
            //Gus- and then here, outside of the while loop and therefore after you have finished changing the color of the sphere, you can change out the old avatar prefabs for the next evolution. you dont want to do this earlier, because we want the models to stay the same if the player didnt pass.
            if(!_tutorial)
            {
                evolutionCount++;
                GameObject[] avatarReplacements = new GameObject[2];
                for (int i = 0; i < avatarReplacements.Length; i++)
                {
                    avatarReplacements[i] = Instantiate(soAvatars[i].AvatarPrefab[evolutionCount]);
                    avatarReplacements[i].transform.SetParent(avatarObjects[i].transform);
                    avatarReplacements[i].transform.localPosition = avatarBehaviors[i].avatarObject.transform.localPosition;
                    avatarReplacements[i].transform.localScale *= scaleAmt;
                    avatarBehaviors[i].avatarObject.SetActive(false);
                    avatarBehaviors[i].avatarObject = avatarReplacements[i];
                }
            }

            while (count < 1)
            {
                count += Time.deltaTime;
                evolveSphereRenderer.material.color = Color.Lerp(startColor, transparentColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                for (int i = 0; i < avatarObjects.Length; i++)
                {
                    avatarObjects[i].transform.localScale = Vector3.Lerp(avatarStartingScales[i] * scaleAmt, avatarStartingScales[i], count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                }
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
                for (int i = 0; i < avatarObjects.Length; i++)
                {
                    avatarObjects[i].transform.localScale = Vector3.Lerp(avatarStartingScales[i] * scaleAmt, avatarStartingScales[i], count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                }
                yield return null;
            }
        }

        for (int i = 0; i < avatarStartingPositions.Length; i++)
        {
            avatarStartingPositions[i] = avatarObjects[i].transform.position;
        }

        count = 0;

        while (count < 1)
        {
            count += Time.deltaTime;

            // Lerp avatars back to cursor positions
            for (int i = 0; i < avatarObjects.Length; i++)
            {
                avatarObjects[i].transform.position = Vector3.Lerp(avatarStartingPositions[i], cursorObjects[i].transform.position, count);
            }
            yield return null;
        }
        if(_pass || _tutorial)
        {
            disableAvatarMovement = false;

        }
        else if (!_tutorial)
        {
            CanvasManager.Instance.ShowCanvasStageFail();
            PauseManager.Instance.showPauseMenu = false;
            PauseManager.Instance.PauseGame(true);
        }
        PauseManager.Instance.openPauseMenuAction.action.performed += PauseManager.Instance.OnPauseButtonPressed;
        // Give control of the avatars back to the player
        readyMove = true;
    }
    //Gus- Called by the stage manager at the end of a stages section in the music. the StagePassCheck method returns a bool based on if the player has enough points to pass the stage.
    public bool StartEvolve(bool _tutorial = false)
    {
        bool tmpBool = APManager.Instance.StagePassCheck();
        StartCoroutine(CoEvolve(tmpBool, _tutorial));
        return tmpBool;
    }

    public void StartEvolveFinal()
    {
        StartCoroutine(CoEvolveFinal(APManager.Instance.StagePassCheck()));
    }
    //alternate version of this method made for the final check to end level
    IEnumerator CoEvolveFinal(bool _pass)
    {
        PauseManager.Instance.isCountingDown = true;
        float count;
        yield return new WaitForSeconds(1f);

        Vector3[] avatarStartingPositions = new Vector3[2];
        for (int i = 0; i < avatarStartingPositions.Length; i++)
        {
            avatarStartingPositions[i] = avatarObjects[i].transform.position;
        }
        Vector3[] avatarStartingScales = new Vector3[2];
        for (int i = 0; i < avatarStartingScales.Length; i++)
        {
            avatarStartingScales[i] = avatarObjects[i].transform.localScale;
        }

        Vector3 offset = new Vector3(-.1f, 0, 0);
        float scaleAmt = .5f;
        // take control from player
        disableAvatarMovement = true;
        count = 0;
        while (count < 1)
        {
            count += Time.deltaTime;
            // lerp avatars to center
            for (int i = 0; i < avatarObjects.Length; i++)
            {
                offset *= -1;
                avatarObjects[i].transform.position = Vector3.Lerp(avatarStartingPositions[i], evolveSphereRenderer.transform.position + offset, count);
            }
            yield return null;
        }
        count = 0;

        while (count < 1)
        {
            count += Time.deltaTime;
            evolveSphereRenderer.material.color = Color.Lerp(transparentColor, startColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);

            yield return null;
        }

        APManager.Instance.DrainAP();
        while (APManager.Instance.isDraining) yield return null;

        count = 0;
        yield return new WaitForSeconds(1f);
        if (_pass)
        {
            for (int i = 0; i < avatarObjects.Length; i++)
            {
                avatarObjects[i].SetActive(false);
            }

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
        if (_pass)
        {
            Debug.Log("end level");
            CanvasManager.Instance.ShowCanvasLevelEnd();
        }
        else
        {
            CanvasManager.Instance.ShowCanvasStageFail();
            PauseManager.Instance.showPauseMenu = false;
        }
        PauseManager.Instance.openPauseMenuAction.action.performed += PauseManager.Instance.OnPauseButtonPressed;
    }
}
