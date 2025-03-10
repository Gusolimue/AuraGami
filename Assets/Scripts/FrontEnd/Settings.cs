using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject[] settingMenus;
    
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
