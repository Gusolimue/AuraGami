using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public InputActionReference openPauseMenuAction;
    public bool isPaused = false;

    [Header("Avatars")]
    [SerializeField] GameObject naginiAvatar;
    [SerializeField] GameObject yataAvatar;

    [Header("Unpause Elements")]
    [SerializeField] public TextMeshProUGUI countdownTimer_TXT;
    public float countdownTimer = 3;
    public bool timerOn = false;

    private void Awake()
    {
        Instance = this;
        countdownTimer_TXT.gameObject.SetActive(false);
        PauseGame(false);
        openPauseMenuAction.action.Enable();
        openPauseMenuAction.action.performed += OnPauseButtonPressed;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void Update()
    {
        OnPauseKeyPressed();

        /*if (timerOn == true)
        {
            Debug.Log("Timer starts!");
            countdownTimer_TXT.gameObject.SetActive(true);
            countdownTimer -= Time.deltaTime;
            timerCountdown(countdownTimer);
            if (countdownTimer > 0f)
            {
                Debug.Log("Timer ends!");
                timerOn = false;
                countdownTimer = 0f;
                countdownTimer_TXT.gameObject.SetActive(false);
            }
        }*/
    }

    private void OnDestroy()
    {
        openPauseMenuAction.action.Disable();
        openPauseMenuAction.action.performed -= OnPauseButtonPressed;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    public void PauseGame(bool _pause)
    {
        if (_pause) Time.timeScale = 0; else Time.timeScale = 1;
    }

    public void OnPauseButtonPressed(InputAction.CallbackContext context)
    {
        if (isPaused == false)
        {
            CanvasManager.Instance.ShowCanvasPauseMenu();
            naginiAvatar.SetActive(false);
            yataAvatar.SetActive(false);
            isPaused = true;
            PauseGame(true);
            Debug.Log("Is Paused!");
        }
        else if (isPaused == true)
        {
            isPaused = false;
            PauseGame(false);
            PauseMenu.Instance.OnResumeGameButtonPressed();
            naginiAvatar.SetActive(true);
            yataAvatar.SetActive(true);
            Debug.Log("Is Unpaused!");
        }
    }

    public void OnPauseKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused == false)
            {
                CanvasManager.Instance.ShowCanvasPauseMenu();
                isPaused = true;
                PauseGame(true);
                Debug.Log("Is Paused!");
            }
            else if (isPaused == true)
            {
                isPaused = false;
                PauseGame(false);
                //StartCoroutine(Countdown(3));
                PauseMenu.Instance.OnResumeGameButtonPressed();
                Debug.Log("Is Unpaused!");
            }
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
    }
}
