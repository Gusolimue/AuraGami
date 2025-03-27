using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject[] settingMenus;

    public void OnBackButtonPressed() // When pressed, destroys Canvas_Settings and instantiates Canvas_FrontEnd.
    {
        if (LoadManager.Instance.currentScene == 0) CanvasManager.Instance.ShowCanvasFE();
        if (LoadManager.Instance.currentScene >= 1) CanvasManager.Instance.ShowCanvasPauseMenu();
        Destroy(this.gameObject);
    }

    public void OnAudioButtonPressed()
    {
        settingMenus[0].SetActive(true); settingMenus[1].SetActive(false);
        settingMenus[2].SetActive(false); settingMenus[3].SetActive(false);
    }

    public void OnGraphicsButtonPressed()
    {
        settingMenus[0].SetActive(false); settingMenus[1].SetActive(true);
        settingMenus[2].SetActive(false); settingMenus[3].SetActive(false);
    }

    public void OnControlsButtonPressed()
    {
        settingMenus[0].SetActive(false); settingMenus[1].SetActive(false);
        settingMenus[2].SetActive(true); settingMenus[3].SetActive(false);
    }

    public void OnAccessabilityButtonPressed()
    {
        settingMenus[0].SetActive(false); settingMenus[1].SetActive(false);
        settingMenus[2].SetActive(false); settingMenus[3].SetActive(true);
    }
}
