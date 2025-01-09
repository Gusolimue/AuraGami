using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
        PauseGame(false);
    }

    private void Update()
    {
        OnPauseButtonPressed();
    }

    public void PauseGame(bool _pause)
    {
        if (_pause) Time.timeScale = 0; else Time.timeScale = 1;
    }

    public void OnPauseButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused == false)
            {
                CanvasManager.Instance.ShowCanvasPauseMenu();
                isPaused = true;
                PauseGame(true);
            }
            else
            {
                isPaused = false;
                PauseGame(false);
                PauseMenu.Instance.OnResumeGameButtonPressed();
            }
        }     
    }
}
