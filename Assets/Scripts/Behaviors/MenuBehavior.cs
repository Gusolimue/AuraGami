using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuBehavior : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Image[] imageAssets;
    [SerializeField] TextMeshProUGUI[] textAssets;
    [SerializeField] Color fadeColor;

    private float fadeSpeed = 15f;
    private float count;

    [Header("Semi-Transparent UI Elements")]
    [SerializeField] Image[] semiTransparentAssets;
    public bool isSemiTransparent;
    public float alphaAmount;

    private void Awake()
    {
        foreach (Image image in imageAssets)
        {
            Color curColor = image.color;
            image.color = new Color(curColor.r, curColor.g, curColor.b, 0);
        }

        foreach (TextMeshProUGUI text in textAssets)
        {
            Color curColor = text.color;
            text.color = new Color(curColor.r, curColor.g, curColor.b, 0);
        }
    }

    private void Update()
    {
        count += Time.deltaTime;

        foreach (Image image in imageAssets)
        {
            Color curColor = image.color;
            float newAlpha = Mathf.Lerp(curColor.a, fadeColor.a, count / fadeSpeed);
            image.color = new Color(curColor.r, curColor.g, curColor.b, newAlpha);
        }

        foreach (TextMeshProUGUI text in textAssets)
        {
            Color curColor = text.color;
            float newAlpha = Mathf.Lerp(curColor.a, fadeColor.a, count / fadeSpeed);
            text.color = new Color(curColor.r, curColor.g, curColor.b, newAlpha);
        }

        if (isSemiTransparent)
        {
            foreach (Image image in semiTransparentAssets)
            {
                Color curColor = image.color;
                float newAlpha = Mathf.Lerp(curColor.a, alphaAmount, count / fadeSpeed);
                image.color = new Color(curColor.r, curColor.g, curColor.b, alphaAmount);
            }
        }
    }
}
