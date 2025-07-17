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

public class ColorBlindChangeManager : MonoBehaviour
{
    public static ColorBlindChangeManager Instance;
    [Header("Materials")]
    [SerializeField] Material leftTarget;
    [SerializeField] Material leftTargetOutline;
    [Space]
    [SerializeField] Material rightTarget;
    [SerializeField] Material rightTargetOutline;
    private enum eColorBlindOptions {none, optionOne, optionTwo};

    [NamedArray(typeof(eColorBlindOptions))]public ColorPallete[] colorPallete;

    [Space]
    [SerializeField] TextMeshProUGUI colorOptionsTXT;


    

    private void Awake()
    {
        Instance = this;
        PlayerPrefs.GetInt("ColorModeIndex", 0);

        colorOptionsTXT.text = PlayerPrefs.GetString("text");
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
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        PlayerPrefs.SetInt("ColorModeIndex", tmpInt);
        PlayerPrefs.Save();
        SwitchColorOptions();
        SetColorPallet();
    }

    private void SwitchColorOptions()
    {
        eColorBlindOptions tmpOption = (eColorBlindOptions)PlayerPrefs.GetInt("ColorModeIndex");
        switch (tmpOption)
        {
            case eColorBlindOptions.none:
                colorOptionsTXT.text = "Standard";
                PlayerPrefs.SetString("text", "Standard");
                break;

            case eColorBlindOptions.optionOne:
                colorOptionsTXT.text = "Option One - Dewt-Pro";
                PlayerPrefs.SetString("text", "Option One - Dewt-Pro");
                break;

            case eColorBlindOptions.optionTwo:
                colorOptionsTXT.text = "Option Two - Tritan";
                PlayerPrefs.SetString("text", "Option Two - Tritan");
                break;
        }
    }

    private void SetColorPallet()
    {
        int _eColorBlindOption = PlayerPrefs.GetInt("ColorModeIndex");

        leftTarget.color = colorPallete[_eColorBlindOption].leftTarget;
        leftTargetOutline.color = colorPallete[_eColorBlindOption].leftTargetOutline;

        rightTarget.color = colorPallete[_eColorBlindOption].rightTarget;
        rightTargetOutline.color = colorPallete[_eColorBlindOption].rightTargetOutline;

        leftTargetOutline.SetColor("_Emissive", colorPallete[_eColorBlindOption].leftOutlineEmissive);

        rightTargetOutline.SetColor("_Emissive", colorPallete[_eColorBlindOption].rightOutlineEmissive);
    }
}
