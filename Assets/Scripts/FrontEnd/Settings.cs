using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject[] settingMenus;
    [SerializeField] SettingsButtonBehavior[] menuButtons;
    private GameObject curMenuInstance;

    private void Awake()
    {
        curMenuInstance = Instantiate(settingMenus[0],transform);
        menuButtons[0].SetSelected(true);
    }

    public void OnBackButtonPressed() // When pressed, destroys Canvas_Settings and instantiates Canvas_FrontEnd.
    {
        CanvasManager.Instance.ShowCanvasFE();

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnPauseBackButtonPressed() // A seperate method for a seperate back button tied to the pause menu.
    {
        if (LoadManager.Instance.isTutorial == false) PauseMenu.Instance.InstantiatePauseOptions();
        else PauseMenu.Instance.InstantiateTutorialPauseOptions();

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        Destroy(this.gameObject);
    }

    public void OnMenuButtonPressed(int menuIndex) // A method that is tied to several settings menu buttons. I assigned each button a menuIndex number that is tied to a menu. When pressed, the corresponding menu will instantiate.
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            bool isSelected = (i == menuIndex);
            menuButtons[i].SetSelected(isSelected);
        }

        if (curMenuInstance != null) Destroy(curMenuInstance);
        if (menuIndex >= 0 && menuIndex < settingMenus.Length) curMenuInstance = Instantiate(settingMenus[menuIndex], transform);
 
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
    }
}
