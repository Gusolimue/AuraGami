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
