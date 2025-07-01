using UnityEngine;

public class AccessibilitySettingsManager : MonoBehaviour
{
    public int colorNum;
    public static AccessibilitySettingsManager Instance;
    ColourManager cm;

    private void Awake()
    {
        Instance = this;
        colorNum = 1;
    }

    public void ColorNumChange()
    {
        colorNum = cm.localColorNum;
    }

    public void ColorNumOne()
    {
        cm.ColorUpdate(1);
    }

    public void ColorNumTwo()
    {
        cm.ColorUpdate(2);
    }

    public void ColorNumThree()
    {
        cm.ColorUpdate(3);
    }
}
