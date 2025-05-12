using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoButtonBehavior : MonoBehaviour
{
    [Header("Info Button")]
    [SerializeField] Image infoBG;
    [SerializeField] TextMeshProUGUI infoTXT;
    [SerializeField] Color[] colorChanges;
    public bool showInfo = false;

    private void Awake()
    {
        infoBG.color = colorChanges[1]; infoTXT.color = colorChanges[1];
    }

    private void Update()
    {
        if (showInfo)
        {
            infoBG.color = Color.Lerp(infoBG.color, colorChanges[2], Time.deltaTime * 8);
            infoTXT.color = Color.Lerp(infoTXT.color, colorChanges[0], Time.deltaTime * 8);
        }
        else
        {
            infoBG.color = Color.Lerp(infoBG.color, colorChanges[1], Time.deltaTime * 8);
            infoTXT.color = Color.Lerp(infoTXT.color, colorChanges[1], Time.deltaTime * 8);
        }
    }
    public void ShowInfo()
    {
        showInfo = true;
    }

    public void HideInfo()
    {
        showInfo = false;
    }
}
