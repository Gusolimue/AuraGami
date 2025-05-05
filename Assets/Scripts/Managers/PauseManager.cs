using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public InputActionReference openPauseMenuAction;
    [SerializeField] public GameObject menuBG;
    public bool isPaused = false;

    [Header("Avatars/UI")]
    [SerializeField] public GameObject naginiAvatar;
    [SerializeField] public GameObject yataAvatar;
    [SerializeField] public GameObject progressBar;

    [Header("Unpause Elements")]
    [SerializeField] public TextMeshProUGUI countdownTimer_TXT;
    [SerializeField] public TextMeshProUGUI[] countdownTimer;
    [SerializeField] GameObject countdownTimer_SizeChange;
    [SerializeField] GameObject ogCountdownSize;
    [SerializeField] Color[] countdownColor;
    public float colorTransitionSpeed = 2.5f;
    public int timerOn;

    private void Awake()
    {
        Instance = this;
        countdownTimer_TXT.gameObject.SetActive(false);
        menuBG.SetActive(false);

        openPauseMenuAction.action.Enable();
        openPauseMenuAction.action.performed += OnPauseButtonPressed;
        InputSystem.onDeviceChange += OnDeviceChange;

        for (int i = 0; i < countdownTimer.Length; i++)
        {
            countdownTimer[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (timerOn == 3)
        {
            countdownTimer[0].color = Color.Lerp(countdownTimer[0].color, countdownColor[1], Time.deltaTime * colorTransitionSpeed);
            countdownTimer[0].transform.localScale = Vector3.Lerp(countdownTimer[0].transform.localScale, countdownTimer_SizeChange.transform.localScale,
                Time.deltaTime * colorTransitionSpeed);
        }
        if (timerOn == 2)
        {
            countdownTimer[1].color = Color.Lerp(countdownTimer[1].color, countdownColor[1], Time.deltaTime * colorTransitionSpeed);
            countdownTimer[1].transform.localScale = Vector3.Lerp(countdownTimer[1].transform.localScale, countdownTimer_SizeChange.transform.localScale,
                Time.deltaTime * colorTransitionSpeed);
        }
        if (timerOn == 1)
        {
            countdownTimer[2].color = Color.Lerp(countdownTimer[2].color, countdownColor[1], Time.deltaTime * colorTransitionSpeed);
            countdownTimer[2].transform.localScale = Vector3.Lerp(countdownTimer[2].transform.localScale, countdownTimer_SizeChange.transform.localScale,
                Time.deltaTime * colorTransitionSpeed);
        }

    }

    private void OnDestroy()
    {
        openPauseMenuAction.action.Disable();
        openPauseMenuAction.action.performed -= OnPauseButtonPressed;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    public void OnPauseButtonPressed(InputAction.CallbackContext context)
    {
        if (isPaused == false)
        {
            CanvasManager.Instance.ShowCanvasPauseMenu();
            naginiAvatar.SetActive(false);
            yataAvatar.SetActive(false);
            progressBar.SetActive(false);
            menuBG.SetActive(true);
            isPaused = true;
            BeatManager.Instance.PauseMusicTMP(true);
            Debug.Log("Is Paused!");
        }
        else if (isPaused == true)
        {
            PauseMenu.Instance.OnResumeGameButtonPressed();
            Debug.Log("Is Unpaused!");
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

    public void StartCountdown()
    {
        StartCoroutine(CountdownBehavior());
    }

    private IEnumerator CountdownBehavior()
    {
        openPauseMenuAction.action.performed -= OnPauseButtonPressed;

        countdownTimer[0].color = countdownColor[0]; countdownTimer[0].transform.localScale = ogCountdownSize.transform.localScale;
        countdownTimer[0].gameObject.SetActive(true);
        timerOn = 3;
        yield return new WaitForSeconds(1.5f);

        countdownTimer[1].color = countdownColor[0]; countdownTimer[1].transform.localScale = ogCountdownSize.transform.localScale;
        countdownTimer[1].gameObject.SetActive(true);
        timerOn = 2;
        yield return new WaitForSeconds(1.5f);

        countdownTimer[2].color = countdownColor[0]; countdownTimer[2].transform.localScale = ogCountdownSize.transform.localScale;
        countdownTimer[2].gameObject.SetActive(true);
        timerOn = 1;
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < countdownTimer.Length; i++)
        {
            countdownTimer[i].gameObject.SetActive(false);
        }
        
        BeatManager.Instance.PauseMusicTMP(false);
        isPaused = false;
        openPauseMenuAction.action.performed += OnPauseButtonPressed;
    }
}
