using UnityEngine;
using UnityEngine.UI;

public class FrontEnd : MonoBehaviour
{
    //[SerializeField] Button playerButton;

    public void OnPlayButtonPressed()
    {
        
    }

    public void OnQuitPressed() // Will exit game (works for builds only).
    {
        Application.Quit();
    }
}
