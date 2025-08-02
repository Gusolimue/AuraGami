using UnityEngine;
using TMPro;
using System.Collections;

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

    private int tmpInt;
    private enum eColorBlindOptions {none, optionOne, optionTwo};

    [NamedArray(typeof(eColorBlindOptions))]public ColorPallete[] colorPallete;

    [Space]
    [SerializeField] TextMeshProUGUI colorOptionsTXT;

    private void Start()
    {
        SetColorPallet();
    }

    private void Awake()
    {
        Instance = this;
        tmpInt = PlayerPrefs.GetInt("ColorModeIndex", 0);

        colorOptionsTXT.text = PlayerPrefs.GetString("text", "Standard");
    }

    public void ColorOptionsCycle(bool isRight)
    {
        int enumLength = System.Enum.GetNames(typeof(eColorBlindOptions)).Length;

        if (isRight) tmpInt = (tmpInt + 1) % enumLength;
        else tmpInt = (tmpInt - 1 + enumLength) % enumLength;

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        PlayerPrefs.SetInt("ColorModeIndex", tmpInt);
        StartCoroutine(TextTransition());
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

    public void DefaultSettings()
    {
        tmpInt = (int)eColorBlindOptions.none;
        PlayerPrefs.SetInt("ColorModeIndex", tmpInt);
        StartCoroutine(TextTransition());
        SwitchColorOptions();
        SetColorPallet();
    }

    public IEnumerator TextTransition()
    {
        float alpha = 0f;
        float fadeInDuration = 1f;

        Color curColor = colorOptionsTXT.color;
        colorOptionsTXT.color = new Color(curColor.r, curColor.g, curColor.b, 0);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeInDuration;
            colorOptionsTXT.color = new Color(colorOptionsTXT.color.r, colorOptionsTXT.color.g,
                colorOptionsTXT.color.b, alpha);
            yield return new WaitForSecondsRealtime(.01f);
        }
    }
}
