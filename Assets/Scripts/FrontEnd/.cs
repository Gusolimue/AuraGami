using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject[] settingMenus;
    [SerializeField] SettingsButtonBehavior[] menuButtons;
    private GameObject curMenuInstance;

    private void Awake()
    {
        /*if (LoadManager.Instance.currentScene >= 1)
        {
            PauseManager.Instance.openPauseMenuAction.action.performed -= PauseManager.Instance.OnPauseButtonPressed;
            this.gameObject.transform.localPosition = new Vector3(-0.34f, 3.44f, 6.43f);
        }
        */
        curMenuInstance = Instantiate(settingMenus[0],transform);
        menuButtons[0].SetSelected(true);
    }

    public void OnBackButtonPressed() // When pressed, destroys Canvas_Settings and instantiates Canvas_FrontEnd.
    {
        CanvasManager.Instance.ShowCanvasFE();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnPauseBackButtonPressed()
    {
        if (LoadManager.Instance.isTutorial == false) PauseMenu.Instance.InstantiatePauseOptions();
        else PauseMenu.Instance.InstantiateTutorialPauseOptions();
        Destroy(this.gameObject);
    }

    public void OnMenuButtonPressed(int menuIndex)
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
