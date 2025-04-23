using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject[] settingMenus;
    [SerializeField] GameObject settingsBG_TMP;

    private void Awake()
    {
        if (LoadManager.Instance.currentScene == 0) settingsBG_TMP.SetActive(false);
        if (LoadManager.Instance.currentScene >= 1) settingsBG_TMP.SetActive(true);
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
        for (int i = 0; i < settingMenus.Length; i++)
        {
            settingMenus[i].SetActive(i == menuIndex);
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
    }
}
