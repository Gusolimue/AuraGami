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

    [Header("Avatars")]
    [SerializeField] SoAvatar yata;
    [SerializeField] SoAvatar nagini;
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

    private void UpdateAvatarColors(SoAvatar avatar, int colorBlindIndex)
    {
        bool isStandard = colorBlindIndex == 0;

        Color baseColor;
        Color emissiveColor;

        if (avatar == nagini)
        {
            baseColor = colorPallete[colorBlindIndex].leftTarget;
            emissiveColor = colorPallete[colorBlindIndex].leftOutlineEmissive;
        }
        else if (avatar == yata)
        {
            baseColor = colorPallete[colorBlindIndex].rightTarget;
            emissiveColor = colorPallete[colorBlindIndex].rightOutlineEmissive;
        }
        else
        {
            baseColor = Color.white;
            emissiveColor = Color.black;
        }

        for (int i = 0; i < avatar.avatarMats.Length; i++)
        {
            Material mat = avatar.avatarMats[i];
            Texture texture;

            if (isStandard) texture = avatar.avatarTextures[i].textures[0];
            else texture = avatar.avatarTextures[i].textures[1];

            mat.SetTexture("_BaseMap", texture);
            if (isStandard) mat.color = Color.white;
            else mat.color = baseColor;

            if (isStandard) mat.SetColor("_EmissionColor", Color.black);
            else 
            {
                mat.SetColor("_EmissionColor", emissiveColor);
            }
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

        UpdateAvatarColors(yata, _eColorBlindOption);
        UpdateAvatarColors(nagini, _eColorBlindOption);
    }
}
