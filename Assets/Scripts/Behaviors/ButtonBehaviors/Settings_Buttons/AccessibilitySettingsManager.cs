using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AccessibilitySettingsManager : MonoBehaviour
{
    public static AccessibilitySettingsManager Instance;
    [Header("Terrain Speed Assets")]
    [SerializeField] TextMeshProUGUI terrainSpeedTXT;

    [Header("Haptics Toggle Assets")]
    [SerializeField] Image toggleFill;
    [SerializeField] Image hoverFill;
    [SerializeField] Color[] toggleColors;
    private int toggleNum = 1;
    private int tptInt;
    private bool isHover;

    private enum eTerrainSpeedTypes { normal, slow, none};

    private void Awake()
    {
        Instance = this;
        terrainSpeedTXT.text = PlayerPrefs.GetString("terrainText", "Normal");
        tptInt = PlayerPrefs.GetInt("TerrainSpeedIndex", 0);

        toggleNum = PlayerPrefs.GetInt("toggleHaptics", 1);
        if (toggleNum == 1) toggleFill.color = toggleColors[0];
        else if (toggleNum == 2) toggleFill.color = toggleColors[1];

        CanvasManager.Instance.playerCircle.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (toggleNum == 1) toggleFill.color = Color.Lerp(toggleFill.color, toggleColors[0], Time.deltaTime * 5);
        else if (toggleNum == 2)toggleFill.color = Color.Lerp(toggleFill.color, toggleColors[1], Time.deltaTime * 5);

        hoverFill.color = Color.Lerp(hoverFill.color, isHover ? toggleColors[2] : toggleColors[1], Time.deltaTime * 5);
    }

    public void ChangeOption(bool isRight)
    {
        int enumLength = System.Enum.GetNames(typeof(eTerrainSpeedTypes)).Length;

        if (isRight) tptInt = (tptInt + 1) % enumLength;
        else tptInt = (tptInt - 1 + enumLength) % enumLength;

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        StartCoroutine(TextTransition());
        TerrainSpeedOptions();
        PlayerPrefs.SetInt("TerrainSpeedIndex", tptInt);
    }

    private void TerrainSpeedOptions()
    {
        eTerrainSpeedTypes tpt = (eTerrainSpeedTypes)tptInt;
        Debug.Log("Current TerrainSpeedType: " + tpt);
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_levelOrbPressed);
    }

    public void OnHover()
    {
        isHover = true;

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .1f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
    }

    public void ExitHover()
    {
        isHover = false;
    }

    public void DefaultSettings()
    {
        tptInt = (int)eTerrainSpeedTypes.normal;
        PlayerPrefs.SetInt("TerrainSpeedIndex", tptInt);
        StartCoroutine(TextTransition());
        TerrainSpeedOptions();

        toggleNum = 1;
        PlayerPrefs.SetInt("toggleHaptics", toggleNum);
        HapticsManager.Instance.hapticsOn = true;

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_levelOrbPressed);
    }

    public IEnumerator TextTransition()
    {
        float alpha = 0f;
        float fadeInDuration = 1f;

        Color curColor = terrainSpeedTXT.color;
        terrainSpeedTXT.color = new Color(curColor.r, curColor.g, curColor.b, 0);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeInDuration;
            terrainSpeedTXT.color = new Color(terrainSpeedTXT.color.r, terrainSpeedTXT.color.g,
                terrainSpeedTXT.color.b, alpha);
            yield return new WaitForSecondsRealtime(.01f);
        }
    }
}
