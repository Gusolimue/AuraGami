using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject[] creditCanvas;
    [SerializeField] SettingsButtonBehavior[] menuButtons;
    public void OnBackButtonPressed() // When pressed, destroys Canvas_LevelSelect and instantiates Canvas_FrontEnd.
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        CanvasManager.Instance.ShowCanvasFE();
        Destroy(this.gameObject);
    }

    public void OnMenuButtonPressed(int menuIndex)
    {
        for (int i = 0; i < creditCanvas.Length; i++)
        {
            creditCanvas[i].SetActive(i == menuIndex);
        }

        for (int i = 0; i < menuButtons.Length; i++)
        {
            bool isSelected = (i == menuIndex);
            menuButtons[i].SetSelected(isSelected);
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
    }
}
