using UnityEngine;
using TMPro;

[System.Serializable]
public class ColorPallete
{
    [Header("Right Material")]
    [SerializeField] Material rightTargetOutline;
    [SerializeField] Material rightTarget;

    [Header("Left Material")]
    [SerializeField] Material leftTargetOutline;
    [SerializeField] Material leftTarget;

    [Header("United Material")]
    [SerializeField] Material unitedTargetOutline;
    [SerializeField] Material unitedTarget;
}

public class AccessibilitySettingsManager : MonoBehaviour
{
    public static AccessibilitySettingsManager Instance;
    private enum eColorBlindOptions {none, optionOne, optionTwo};
    private eColorBlindOptions curColorOption = eColorBlindOptions.none;

    //[NamedArray(typeof(eColorBlindOptions))]public ColorPallete[] colorPallete;

    [SerializeField] TextMeshProUGUI colorOptionsTXT;
    

    private void Awake()
    {
        Instance = this;

        PlayerPrefs.GetString("setNone");
        PlayerPrefs.GetString("setCB1");
        PlayerPrefs.GetString("setCB2");
    }

    public void ColorOptionsCycle(bool direction)
    {
        if (direction)
        {
            curColorOption++;
            if (curColorOption > eColorBlindOptions.optionTwo) curColorOption--;
        }
        else
        {
            curColorOption--;
            if (curColorOption < eColorBlindOptions.none) curColorOption++;
        }
        SwitchColorOptions();
    }

    private void SwitchColorOptions()
    {
        switch (curColorOption)
        {
            case eColorBlindOptions.none:
                colorOptionsTXT.text = "STANDARD";
                PlayerPrefs.SetString("setNone", "NONE");
                PlayerPrefs.Save();
                break;

            case eColorBlindOptions.optionOne:
                colorOptionsTXT.text = "OPTION ONE";
                PlayerPrefs.SetString("setCB1", "OPTION ONE");
                PlayerPrefs.Save();
                break;

            case eColorBlindOptions.optionTwo:
                colorOptionsTXT.text = "OPTION TWO";
                PlayerPrefs.SetString("setCB2", "OPTION TWO");
                PlayerPrefs.Save();
                break;
        }
    }
}
