using UnityEngine;
using UnityEngine.UI;

public class FrontEnd : MonoBehaviour
{
    //[SerializeField] Button playerButton;

    public void OnPlayButtonPressed()
    {
        
    }

    public void OnLevelsButtonPressed()
    {
        CanvasManager.Instance.ShowCanvasLevelSelect();
        Destroy(this.gameObject);
    }

    public void OnQuitButtonPressed() // Will exit game (works for builds only).
    {
        Application.Quit();
    }
}
