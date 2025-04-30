using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject[] settingMenus;

    public void OnBackButtonPressed() // When pressed, destroys Canvas_Settings and instantiates Canvas_FrontEnd.
    {
        if (LoadManager.Instance.currentScene == 0) CanvasManager.Instance.ShowCanvasFE();
        if (LoadManager.Instance.currentScene >= 1) CanvasManager.Instance.ShowCanvasPauseMenu();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
    }

    public void OnMenuButtonPressed(int menuIndex)
    {
        for (int i = 0; i < settingMenus.Length; i++)
        {
            settingMenus[i].SetActive(i == menuIndex);
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
    }
}
