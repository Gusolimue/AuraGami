using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoButtonBehavior : MonoBehaviour
{
    [Header("Info Button")]
    [SerializeField] Image[] infoImageAssets;
    [SerializeField] Image[] stars;
    [SerializeField] Slider[] connectors;
    [SerializeField] TextMeshProUGUI infoTXT;
    [SerializeField] Color[] colorChanges;
    public bool showInfo = false;

    private void Awake()
    {
        foreach (var info in infoImageAssets)
        {
            info.color = colorChanges[1];
        }

        foreach (var stars in stars)
        {
            stars.color = colorChanges[1];
        }

        foreach (var connectors in connectors)
        {
            connectors.value = 0;
        }
        infoTXT.color = colorChanges[1];
    }

    private void Update()
    {
        foreach (var info in infoImageAssets)
        {
            if (showInfo)
            {
                info.color = Color.Lerp(info.color, colorChanges[2], Time.deltaTime * 8);
                infoTXT.color = Color.Lerp(infoTXT.color, colorChanges[0], Time.deltaTime * 8);
            }
            else
            {
                info.color = Color.Lerp(info.color, colorChanges[1], Time.deltaTime * 8);
                infoTXT.color = Color.Lerp(infoTXT.color, colorChanges[1], Time.deltaTime * 8);
            }
        }

        foreach (var connectors in connectors)
        {
            if (showInfo) connectors.value = Mathf.Lerp(connectors.value, 1, Time.deltaTime * 8);
            else connectors.value = Mathf.Lerp(connectors.value, 0, Time.deltaTime * 8);
        }

        foreach (var stars in stars)
        {
            if (showInfo) stars.color = Color.Lerp(stars.color, colorChanges[0], Time.deltaTime * 8);
            else stars.color = Color.Lerp(stars.color, colorChanges[1], Time.deltaTime * 8);
        }
    }
    public void ShowInfo()
    {
        showInfo = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverLarge);
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .1f, .1f);
    }

    public void HideInfo()
    {
        showInfo = false;
    }
}
