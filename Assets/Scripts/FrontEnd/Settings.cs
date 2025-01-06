using UnityEngine;

public class Settings : MonoBehaviour
{
    public void OnBackButtonPressed() // When pressed, destroys Canvas_Settings and instantiates Canvas_FrontEnd.
    {
        CanvasManager.Instance.ShowCanvasFE();
        Destroy(this.gameObject);
    }
}
