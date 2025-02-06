using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    //private RepeatTerrainBehavior repeatTerrainBehavior;
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
            StartCoroutine(Countdown(3));
            naginiAvatar.SetActive(true);
            yataAvatar.SetActive(true);
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
        isPaused = false;
    }

    /*void Update()
    {
        if (PauseManager.Instance.isPaused == false) TerrainMovement(true);
    }

    public void TerrainMovement(bool _on)
    {
        transform.Translate(Vector3.back * Time.deltaTime * tmpSpeed);

        if (transform.position.z < startPos.z - repeatLength / 2)
        {
            transform.position = startPos;
        }
    }*/

}
