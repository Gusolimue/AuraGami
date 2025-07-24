using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccessibilitySettingsManager : MonoBehaviour
{
    public static AccessibilitySettingsManager Instance;
    [Header("Terrain Speed Assets")]
    [SerializeField] TextMeshProUGUI terrainSpeedTXT;

    [Header("Haptics Toggle Assets")]
    [SerializeField] Image toggleFill;
    [SerializeField] Color[] toggleColors;
    private int toggleNum = 1;
    private enum eTerrainSpeedTypes { normal, slow, none};

    private void Awake()
    {
        Instance = this;
        terrainSpeedTXT.text = PlayerPrefs.GetString("terrainText", "Normal");

        toggleNum = PlayerPrefs.GetInt("toggleHaptics", 1);
        if (toggleNum == 1) toggleFill.color = toggleColors[0];
        else if (toggleNum == 2) toggleFill.color = toggleColors[1];
    }

    private void Update()
    {
        if (toggleNum == 1) toggleFill.color = Color.Lerp(toggleFill.color, toggleColors[0], Time.deltaTime * 5);
        else if (toggleNum == 2)toggleFill.color = Color.Lerp(toggleFill.color, toggleColors[1], Time.deltaTime * 5);
    }

    public void ChangeOption(bool direction)
    {
        int tptInt = PlayerPrefs.GetInt("TerrainSpeedIndex");
        if (direction)
        {
            tptInt++;
            if ((eTerrainSpeedTypes)tptInt > eTerrainSpeedTypes.none) tptInt--;
        }
        else
        {
            tptInt--;
            if ((eTerrainSpeedTypes)tptInt < eTerrainSpeedTypes.normal) tptInt++;
        }
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        TerrainSpeedOptions();
        PlayerPrefs.SetInt("TerrainSpeedIndex", tptInt);
    }

    private void TerrainSpeedOptions()
    {
        eTerrainSpeedTypes tpt = (eTerrainSpeedTypes)PlayerPrefs.GetInt("TerrainSpeedIndex");
        switch (tpt)
        {
            case eTerrainSpeedTypes.normal:
                EnvironmentManager.environmentSpeed = 5;
                PlayerPrefs.SetInt("terrainSpeed", 5);

                terrainSpeedTXT.text = "Normal";
                PlayerPrefs.SetString("terrainText", "Normal");
                break;

            case eTerrainSpeedTypes.slow:
                EnvironmentManager.environmentSpeed = 2;
                PlayerPrefs.SetInt("terrainSpeed", 2);

                terrainSpeedTXT.text = "Slow";
                PlayerPrefs.SetString("terrainText", "Slow");
                break;

            case eTerrainSpeedTypes.none:
                EnvironmentManager.environmentSpeed = 0;
                PlayerPrefs.SetInt("terrainSpeed", 0);

                terrainSpeedTXT.text = "No Movement";
                PlayerPrefs.SetString("terrainText", "No Movement");
                break;
        }
    }

    public void ToggleHaptics()
    {
        if (HapticsManager.Instance.hapticsOn == false)
        {
            HapticsManager.Instance.hapticsOn = true;
            toggleNum = 1;
            PlayerPrefs.SetInt("toggleHaptics", toggleNum);
        }
        else
        {
            HapticsManager.Instance.hapticsOn = false;
            toggleNum = 2;
            PlayerPrefs.SetInt("toggleHaptics", toggleNum);
        }

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
    }
}
