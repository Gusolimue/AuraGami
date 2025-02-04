using UnityEngine;

public class Settings : MonoBehaviour
{
    public void OnBackButtonPressed() // When pressed, destroys Canvas_Settings and instantiates Canvas_FrontEnd.
    {
        if (LoadManager.Instance.whichScene == 0) CanvasManager.Instance.ShowCanvasFE();
        if (LoadManager.Instance.whichScene >= 1) CanvasManager.Instance.ShowCanvasPauseMenu(); 
        Destroy(this.gameObject);
    }
}
