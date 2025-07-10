using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject[] creditCanvas;
    [SerializeField] SettingsButtonBehavior[] menuButtons;
    private GameObject curMenuInstance;

    private void Awake()
    {
        curMenuInstance = Instantiate(creditCanvas[0], transform);
        menuButtons[0].SetSelected(true);
    }

    public void OnBackButtonPressed() // When pressed, destroys Canvas_LevelSelect and instantiates Canvas_FrontEnd.
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        CanvasManager.Instance.ShowCanvasFE();
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
        if (menuIndex >= 0 && menuIndex < creditCanvas.Length) curMenuInstance = Instantiate(creditCanvas[menuIndex], transform);

        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);

    }
}
