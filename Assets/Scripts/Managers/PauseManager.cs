using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public InputActionReference openPauseMenuAction;
    //[SerializeField] public GameObject menuBG;
    //[SerializeField] SpriteRenderer menuBGSprite;
    public bool isPaused = false;
    public bool showPauseMenu;
    public bool isCountingDown;

    [Header("Avatars/UI")]
    [SerializeField] GameObject[] toHide;

    [Header("Unpause Elements")]
    [SerializeField] public TextMeshProUGUI[] countdownTimer;
    [SerializeField] GameObject countdownTimer_SizeChange;
    [SerializeField] GameObject ogCountdownSize;
    [SerializeField] Color[] countdownColor;
    public float colorTransitionSpeed = 2.5f;
    public int timerOn;

    private void Awake()
    {
        Instance = this;
       // menuBG.SetActive(false);

        openPauseMenuAction.action.Enable();
        openPauseMenuAction.action.performed += OnPauseButtonPressed;
        InputSystem.onDeviceChange += OnDeviceChange;

        for (int i = 0; i < countdownTimer.Length; i++)
        {
            countdownTimer[i].gameObject.SetActive(false);
        }

        //menuBGSprite.color = countdownColor[1];
    }

    private void Update()
    {
        if (timerOn == 3)
        {
            countdownTimer[0].color = Color.Lerp(countdownTimer[0].color, countdownColor[1], Time.deltaTime * colorTransitionSpeed);
            countdownTimer[0].transform.localScale = Vector3.Lerp(countdownTimer[0].transform.localScale, countdownTimer_SizeChange.transform.localScale,
                Time.deltaTime * 2);
        }
        if (timerOn == 2)
        {
            countdownTimer[1].color = Color.Lerp(countdownTimer[1].color, countdownColor[1], Time.deltaTime * colorTransitionSpeed);
            countdownTimer[1].transform.localScale = Vector3.Lerp(countdownTimer[1].transform.localScale, countdownTimer_SizeChange.transform.localScale,
                Time.deltaTime * 2);
        }
        if (timerOn == 1)
        {
            countdownTimer[2].color = Color.Lerp(countdownTimer[2].color, countdownColor[1], Time.deltaTime * colorTransitionSpeed);
            countdownTimer[2].transform.localScale = Vector3.Lerp(countdownTimer[2].transform.localScale, countdownTimer_SizeChange.transform.localScale,
                Time.deltaTime * 2);
        }

        //if (isPaused) menuBGSprite.color = Color.Lerp(menuBGSprite.color, countdownColor[1], Time.deltaTime * 5);
        //else menuBGSprite.color = Color.Lerp(menuBGSprite.color, countdownColor[0], Time.deltaTime * 5);

    }

    private void OnDestroy()
    {
        openPauseMenuAction.action.Disable();
        openPauseMenuAction.action.performed -= OnPauseButtonPressed;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    public void OnPauseButtonPressed(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_pause_menuOpen);
            showPauseMenu = true;
            PauseGame(true);
        }
        else if (isPaused)
        {
            PauseGame(false);
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch(change)
        {
            case InputDeviceChange.Disconnected:
                openPauseMenuAction.action.Disable();
                openPauseMenuAction.action.performed -= OnPauseButtonPressed;
                break;
            case InputDeviceChange.Reconnected:
                openPauseMenuAction.action.Enable();
                openPauseMenuAction.action.performed += OnPauseButtonPressed;
                break;
        }
    }

    private void HideAndUnhideObjects(bool enabled)
    {
        foreach (var item in toHide)
        {
            item.SetActive(enabled);
        }
        foreach (var item in AvatarManager.Instance.avatarObjects)
        {
            item.SetActive(enabled);
        }
        foreach (var item in AvatarManager.Instance.cursorObjects)
        {
            item.SetActive(enabled);
        }

    }

    public void PauseGame(bool paused)
    {
        if (!isCountingDown && !FrontEndSceneTransitionManager.Instance.isTransitioning)
        {
            if (paused)
            {
                if (showPauseMenu)
                {
                    CanvasManager.Instance.ShowCanvasPauseMenu();
                    //menuBG.SetActive(true);
                }

                isPaused = true;
                HideAndUnhideObjects(false);
                BeatManager.Instance.PauseMusicTMP(true);
                Debug.Log("Is Paused!");
            }

            if (!paused)
            {
                if (showPauseMenu)
                {
                    HideAndUnhideObjects(true);
                    PauseMenu.Instance.DestroyMenu();
                    //menuBG.SetActive(false);
                    StartCoroutine(CountdownBehavior());
                    Debug.Log("Is Unpaused!");
                }
            }
        }
    }

    private IEnumerator CountdownBehavior()
    {
        isCountingDown = true;
        openPauseMenuAction.action.performed -= OnPauseButtonPressed;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_pause_countdown);

        countdownTimer[0].color = countdownColor[0]; countdownTimer[0].transform.localScale = ogCountdownSize.transform.localScale;
        countdownTimer[0].gameObject.SetActive(true);
        timerOn = 3;
        yield return new WaitForSeconds(1f);

        countdownTimer[1].color = countdownColor[0]; countdownTimer[1].transform.localScale = ogCountdownSize.transform.localScale;
        countdownTimer[1].gameObject.SetActive(true);
        timerOn = 2;
        yield return new WaitForSeconds(1f);

        countdownTimer[2].color = countdownColor[0]; countdownTimer[2].transform.localScale = ogCountdownSize.transform.localScale;
        countdownTimer[2].gameObject.SetActive(true);
        timerOn = 1;
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < countdownTimer.Length; i++)
        {
            countdownTimer[i].gameObject.SetActive(false);
        }
        
        BeatManager.Instance.PauseMusicTMP(false);
        isPaused = false;
        isCountingDown = false;
        openPauseMenuAction.action.performed += OnPauseButtonPressed;
    }
}
