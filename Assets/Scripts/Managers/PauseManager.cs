using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public InputActionReference openPauseMenuAction;
    public bool isPaused = false;

    [Header("Avatars/UI")]
    [SerializeField] public GameObject naginiAvatar;
    [SerializeField] public GameObject yataAvatar;
    [SerializeField] public GameObject progressBar;

    [Header("Unpause Elements")]
    [SerializeField] public TextMeshProUGUI countdownTimer_TXT;
    public float countdownTimer = 3;
    public bool timerOn = false;

    private void Awake()
    {
        Instance = this;
        countdownTimer_TXT.gameObject.SetActive(false);
        openPauseMenuAction.action.Enable();
        openPauseMenuAction.action.performed += OnPauseButtonPressed;
        InputSystem.onDeviceChange += OnDeviceChange;
    }
    private void Start()
    {
        //moved this to start because you cant refer to another instance (beatmanager) in awake
        //PauseGame(false);
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
        StartCoroutine(Countdown(3));
    }

    public IEnumerator Countdown(int seconds)
    {
        countdownTimer = seconds;
        countdownTimer_TXT.gameObject.SetActive(true);
        Time.timeScale = 1;

        while (countdownTimer > 0)
        {
            countdownTimer_TXT.text = countdownTimer.ToString();
            Debug.Log(countdownTimer);
            yield return new WaitForSecondsRealtime(1f);
            countdownTimer--;
        }
        countdownTimer_TXT.gameObject.SetActive(false);
        BeatManager.Instance.PauseMusicTMP(false);
        isPaused = false;
    }
}
