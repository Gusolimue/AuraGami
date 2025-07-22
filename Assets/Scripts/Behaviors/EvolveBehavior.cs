using System.Collections;
using UnityEngine;
using EditorAttributes;
public class EvolveBehavior : MonoBehaviour
{
    [Header("Variables to Adjust")]
    [Range(0f, 1f)]
    public float cheatAP;
    [Header("Variables to Set")]


    public DissolveBehavior sphere;
    [Header("Variables to Call")]
    public bool readyMove;

    GameObject[] avatars;
    AudioManager audioManager;
    AvatarManager avatarManager;
    int evolutionIndex = 0;
    public void Start()
    {
        audioManager = AudioManager.Instance;
        avatarManager = AvatarManager.Instance;
    }

    //Gus- Called by the stage manager at the end of a stages section in the music. the StagePassCheck method returns a bool based on if the player has enough points to pass the stage.
    [Button]
    public void StartCheatEvolve()
    {
        BeatManager.Instance.PauseMusicTMP(true);
        APManager.Instance.curAP = cheatAP;
        APManager.Instance.IncreaseAP();
        StartEvolve();
    }
    public bool StartEvolve(bool _tutorial = false, bool _final = false)
    {
        bool tmpBool = APManager.Instance.StagePassCheck();
        StartCoroutine(CoEvolve(tmpBool, _tutorial, _final));
        return tmpBool;
    }
    //Gus- this is the method that shows the evolution. it accepts a bool, which is determined by wether or not the player passes the stage. it then proceeds to do the start of the evolution, as that is the same wether the player passes or fails the level. then, it plays the pass or fail animation depending on the value of the bool. see A (pass) and B (fail) comments below
    public IEnumerator CoEvolve(bool _pass, bool _tutorial = false, bool _final = false)
    {
        avatars = avatarManager.avatarObjects;
        audioManager.PlaySFX(audioManager.sfx_avatar_evolveStart);
        PauseManager.Instance.openPauseMenuAction.action.performed -= PauseManager.Instance.OnPauseButtonPressed;
        float count;
        //Gus- here you will want to take control of the avatars away from the player

        //Gus- and then you will want a while loop here to lerp the avatars together (close but not exactly on top of eachother, as you dont want them clipping through each other)
        yield return new WaitForSeconds(1f);

        Vector3[] avatarStartingPositions = new Vector3[2];
        for (int i = 0; i < avatarStartingPositions.Length; i++)
        {
            avatarStartingPositions[i] = avatars[i].transform.position;
        }
        Vector3[] avatarStartingScales = new Vector3[2];
        for (int i = 0; i < avatarStartingScales.Length; i++)
        {
            avatarStartingScales[i] = avatars[i].transform.localScale;
        }

        Vector3 offset = new Vector3(-.1f, 0, 0);
        float scaleAmt = .5f;
        if (_final) scaleAmt = .25f;
        // take control from player
        avatarManager.disableAvatarMovement = true;
        count = 0;
        while (count < 1)
        {
            count += Time.deltaTime;
            // lerp avatars to center
            for (int i = 0; i < avatars.Length; i++)
            {
                avatars[i].transform.position = Vector3.Lerp(avatarStartingPositions[i], sphere.transform.position + offset, count);
                avatars[i].transform.rotation = Quaternion.Slerp(avatars[i].transform.rotation, Quaternion.identity, count);
                offset *= -1;
            }
            yield return null;
        }
        count = 0;

        APManager.Instance.DrainAP();
        while (count < 1)//Gus- these while loops are just to wait for an amount of time within a coroutine without moving on. you can use these instead of "yield return new WaitForSeconds(float);" if theres code youd like to be running each frame the game is waiting. you will want to do things you only want to happen once outside of the loop (like instantiating or deleting avatars), because if they are inside the loop they will happen every frame until the time has passed
        { //Gus- this while loop should be used just for changing the color of the sphere(after the avatars are already together, so the sphere is closing around them
            count += Time.deltaTime;
            // fade in the sphere
            //evolveSphereRenderer.material.color = Color.Lerp(transparentColor, startColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
            for (int i = 0; i < avatars.Length; i++)
            {
                offset *= -1;
                avatars[i].transform.localScale = Vector3.Lerp(avatarStartingScales[i], avatarStartingScales[i] * scaleAmt, count);
            }

            // proceed with sphere fadeout / color change if fail

            yield return null;
        }

        while (APManager.Instance.isDraining) yield return null;
        count = 0;
        yield return new WaitForSeconds(1f); //Gus- this wait is just to give more time for the sequence.
        if (_pass)//Gus A - here is where the pass animation begins. if the variable is true, all it does is fade the orb back to transparency and reset the ap meter.
        {
            audioManager.PlaySFX(audioManager.sfx_avatar_evolveSuccess);
            if (!_tutorial)
            {
                evolutionIndex++;
                if(!_final)
                {
                    avatarManager.SetNewAvatars(evolutionIndex, scaleAmt);
                }
                else
                {
                    foreach (var item in avatars)
                    {
                        item.SetActive(false);
                    }
                }
            }

            while (count < 1)
            {
                count += Time.deltaTime;
                sphere.ResetSphereFill(1f);
                for (int i = 0; i < avatars.Length; i++)
                {
                    avatars[i].transform.localScale = Vector3.Lerp(avatarStartingScales[i] * scaleAmt, avatarStartingScales[i], count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                }
                yield return null;
            }
        }
        else
        {
            count = 0;
            while (count < 1)
            {
                count += Time.deltaTime;
                sphere.ResetSphereFill(1f);
                for (int i = 0; i < avatars.Length; i++)
                {
                    avatars[i].transform.localScale = Vector3.Lerp(avatarStartingScales[i] * scaleAmt, avatarStartingScales[i], count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                }
                yield return null;
            }
        }

        for (int i = 0; i < avatarStartingPositions.Length; i++)
        {
            avatarStartingPositions[i] = avatars[i].transform.position;
        }

        count = 0;

        while (count < 1)
        {
            count += Time.deltaTime;

            // Lerp avatars back to cursor positions
            for (int i = 0; i < avatars.Length; i++)
            {
                avatars[i].transform.position = Vector3.Lerp(avatarStartingPositions[i], avatarManager.cursorObjects[i].transform.position, count);
            }
            yield return null;
        }
        if (_pass && !_final || _tutorial)
        {
            avatarManager.disableAvatarMovement = false;

        }
        else if (!_tutorial)
        {
            CanvasManager.Instance.ShowCanvasLevelProgress();
            PauseManager.Instance.showPauseMenu = false;
            PauseManager.Instance.PauseGame(true);
        }
        PauseManager.Instance.openPauseMenuAction.action.performed += PauseManager.Instance.OnPauseButtonPressed;
        // Give control of the avatars back to the player
        readyMove = true;
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
            avatarStartingPositions[i] = avatars[i].transform.position;
        }
        Vector3[] avatarStartingScales = new Vector3[2];
        for (int i = 0; i < avatarStartingScales.Length; i++)
        {
            avatarStartingScales[i] = avatars[i].transform.localScale;
        }

        Vector3 offset = new Vector3(-.1f, 0, 0);
        float scaleAmt = .5f;
        // take control from player
        //disableAvatarMovement = true;
        count = 0;
        while (count < 1)
        {
            count += Time.deltaTime;
            // lerp avatars to center
            for (int i = 0; i < avatars.Length; i++)
            {
                offset *= -1;
                //avatarObjects[i].transform.position = Vector3.Lerp(avatarStartingPositions[i], evolveSphereRenderer.transform.position + offset, count);
            }
            yield return null;
        }
        count = 0;

        while (count < 1)
        {
            count += Time.deltaTime;
            //evolveSphereRenderer.material.color = Color.Lerp(transparentColor, startColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);

            yield return null;
        }

        APManager.Instance.DrainAP();
        while (APManager.Instance.isDraining) yield return null;

        count = 0;
        yield return new WaitForSeconds(1f);
        if (_pass)
        {
            for (int i = 0; i < avatars.Length; i++)
            {
                avatars[i].SetActive(false);
            }

            while (count < 1)
            {
                count += Time.deltaTime;
                //evolveSphereRenderer.material.color = Color.Lerp(startColor, transparentColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                yield return null;
            }
        }
        else
        {
            //Gus B -  here is where the fail animation begins. its more complicated, but only slightly. 
            while (count < 1)
            {
                count += Time.deltaTime;
                //evolveSphereRenderer.material.color = Color.Lerp(startColor, failColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                yield return null;
            }
            count = 0;
            while (count < 1)
            {
                count += Time.deltaTime;
                //evolveSphereRenderer.material.color = Color.Lerp(failColor, transparentColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
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
