using TMPro;
using UnityEngine;
using System.Collections;

public class TutorialCanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorialText;
    public Color fadeInColor;
    public Color fadeOutColor;
    public string tutorialTextContent;
    public float fadeTime = 5f;

    private bool isFadeIn;
    private float count;

    private void Update()
    {
        count += Time.deltaTime;
        if (isFadeIn) tutorialText.color = Color.Lerp(tutorialText.color, fadeInColor, count * fadeTime);
        else tutorialText.color = Color.Lerp(tutorialText.color, fadeOutColor, count * fadeTime);
    }

    public void FadeInText()
    {
        isFadeIn = true;
    }

    public void FadeOutText()
    {
        isFadeIn = false;
    }
}
