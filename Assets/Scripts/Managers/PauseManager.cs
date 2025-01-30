using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public InputActionReference openPauseMenuAction;
    public bool isPaused = false;

    private void Awake()
    {
        Instance = this;
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
}
