using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject[] settingMenus;
    [SerializeField] SettingsButtonBehavior[] menuButtons;

    private void Awake()
    {
        if (LoadManager.Instance.currentScene >= 1)
        {
            PauseManager.Instance.openPauseMenuAction.action.performed -= PauseManager.Instance.OnPauseButtonPressed;
            this.gameObject.transform.localPosition = new Vector3(-0.34f, 3.44f, 6.43f);
        }
    }

    public void OnBackButtonPressed() // When pressed, destroys Canvas_Settings and instantiates Canvas_FrontEnd.
    {
        if (LoadManager.Instance.currentScene == 0) CanvasManager.Instance.ShowCanvasFE();
        if (LoadManager.Instance.currentScene >= 1) CanvasManager.Instance.ShowCanvasPauseMenu();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnMenuButtonPressed(int menuIndex)
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            bool isSelected = (i == menuIndex);
            menuButtons[i].SetSelected(isSelected);
        }

        for (int i = 0; i < settingMenus.Length; i++)
        {
            settingMenus[i].SetActive(i == menuIndex);
        }
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
    }
}
