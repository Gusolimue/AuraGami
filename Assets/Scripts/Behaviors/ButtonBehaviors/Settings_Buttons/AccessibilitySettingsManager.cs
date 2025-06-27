using UnityEngine;
using TMPro;

[System.Serializable]
public class ColorPallete
{
    [Header("Right Color")]
    public Color rightTargetOutline;
    public Color rightOutlineEmissive;
    public Color rightTarget;

    [Header("Left Color")]
    public Color leftTargetOutline;
    public Color leftOutlineEmissive;
    public Color leftTarget;

    /*[Header("United Material")]
    [SerializeField] Material unitedTargetOutline;
    [SerializeField] Material unitedTarget;*/
}

public class AccessibilitySettingsManager : MonoBehaviour
{
    public static AccessibilitySettingsManager Instance;
    private enum eColorBlindOptions {none, optionOne, optionTwo};
    private eColorBlindOptions curColorOption = eColorBlindOptions.none;

    [NamedArray(typeof(eColorBlindOptions))]public ColorPallete[] colorPallete;

    [Space]
    [SerializeField] TextMeshProUGUI colorOptionsTXT;
    

    private void Awake()
    {
        Instance = this;
        PlayerPrefs.GetInt("ColorModeIndex", 0);
    }

    public void ColorOptionsCycle(bool direction)
    {
        int tmpInt = PlayerPrefs.GetInt("ColorModeIndex");
        if (direction)
        {
            tmpInt++;
            if ((eColorBlindOptions)tmpInt > eColorBlindOptions.optionTwo) tmpInt--;
        }
        else
        {
            tmpInt--;
            if ((eColorBlindOptions)tmpInt < eColorBlindOptions.none) tmpInt++;
        }
        PlayerPrefs.SetInt("ColorModeIndex", tmpInt);
        SwitchColorOptions();
        SetColorPallet();
    }

    private void SwitchColorOptions()
    {
        eColorBlindOptions tmpOption = (eColorBlindOptions)PlayerPrefs.GetInt("ColorModeIndex");
        switch (tmpOption)
        {
            case eColorBlindOptions.none:
                colorOptionsTXT.text = "STANDARD";
                break;

            case eColorBlindOptions.optionOne:
                colorOptionsTXT.text = "OPTION ONE: DEWT-PRO";
                break;

            case eColorBlindOptions.optionTwo:
                colorOptionsTXT.text = "OPTION TWO: TRITAN";
                break;
        }
    }

    private void SetColorPallet()
    {
        int _eColorBlindOption = PlayerPrefs.GetInt("ColorModeIndex");
        (Resources.Load("Materials/" + "Red", typeof(Material)) as Material).color = colorPallete[_eColorBlindOption].leftTarget;
        (Resources.Load("Materials/" + "RedGlow", typeof(Material)) as Material).color = colorPallete[_eColorBlindOption].leftTargetOutline;
        (Resources.Load("Materials/" + "RedGlow", typeof(Material)) as Material).SetColor("_EmissionMap", colorPallete[_eColorBlindOption].leftOutlineEmissive);

        (Resources.Load("Materials/" + "Blue", typeof(Material)) as Material).color = colorPallete[_eColorBlindOption].rightTarget;
        (Resources.Load("Materials/" + "BlueGlow", typeof(Material)) as Material).color = colorPallete[_eColorBlindOption].rightTargetOutline;
        (Resources.Load("Materials/" + "BlueGlow", typeof(Material)) as Material).SetColor("_EmissionMap", colorPallete[_eColorBlindOption].rightOutlineEmissive); 
    }
}
