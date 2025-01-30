using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public InputActionReference openPauseMenuAction;
    public bool isPaused = false;

    [Header("Unpause Elements")]
    [SerializeField] public TextMeshProUGUI countdownTimer_TXT;
    public int countdownTimer = 3;

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
            isPaused = true;
            PauseGame(true);
            Debug.Log("Is Paused!");
        }
        else if (isPaused == true)
        {
            isPaused = false;
            PauseGame(false);
            PauseMenu.Instance.OnResumeGameButtonPressed();
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

    string timerCountdown(float time)
    {
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0}", seconds);
    }


    public IEnumerator UnpauseCountdown()
    {
        countdownTimer = 3;
        countdownTimer_TXT.gameObject.SetActive(true);
        PauseGame(false);
        while (countdownTimer > -1)
        {
            Debug.Log("Starting UnPause Countdown");
            countdownTimer_TXT.text = timerCountdown(countdownTimer);
            countdownTimer -= 1;
            yield return new WaitForSecondsRealtime(1f);
            yield return null; // Wait for the next frame
        }
        countdownTimer_TXT.gameObject.SetActive(false);

        isPaused = false;
    }
}
