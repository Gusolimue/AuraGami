using UnityEngine;

public class ColourManager : MonoBehaviour
{
    public int localColorNum;
    public static ColourManager Instance;
    AccessibilitySettingsManager asm;

    public bool blackWhiteTest;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (localColorNum != asm.colorNum)
            asm.ColorNumChange();
    }

    public void ColorUpdate(int colorNum)
    {
        localColorNum = colorNum;
    }
}
