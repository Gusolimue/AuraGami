using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOrbInteractionBehavior : MonoBehaviour
{
    public InputActionReference openPauseMenuAction;

    private void Awake()
    {
        openPauseMenuAction.action.Enable();
        openPauseMenuAction.action.performed += OnInteractButtonPressed;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zephyr_Hands"))
        {
            
        }
    }

    public void OnInteractButtonPressed(InputAction.CallbackContext context)
    {
     
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                openPauseMenuAction.action.Disable();
                openPauseMenuAction.action.performed -= OnInteractButtonPressed;
                break;
            case InputDeviceChange.Reconnected:
                openPauseMenuAction.action.Enable();
                openPauseMenuAction.action.performed += OnInteractButtonPressed;
                break;
        }
    }
}
