using UnityEngine;
using TMPro;

public class AccessibilitySettingsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI terrainSpeedTXT;
    private enum eTerrainSpeedTypes { normal, slow, none};

    private void Awake()
    {
        terrainSpeedTXT.text = PlayerPrefs.GetString("terrainText");
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
}
