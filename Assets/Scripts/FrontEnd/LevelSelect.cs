using UnityEngine;

public class LevelSelect : MonoBehaviour
{
   public void OnBackButtonPressed() // When pressed, destroys Canvas_LevelSelect and instantiates Canvas_FrontEnd.
   {
        NewAudioManager.Instance.frontEndButtonSFX.Play();
        CanvasManager.Instance.ShowCanvasFE();
        Destroy(this.gameObject);
   }
}
