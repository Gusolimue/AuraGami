using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialCanvasManager : MonoBehaviour
{
    [Header("Tutorial Text")]
    [SerializeField] TextMeshProUGUI tutorialText;
    public Color fadeInColor;
    public Color fadeOutColor;
    public string tutorialTextContent;
    public float fadeTime = 5f;

    private bool isFadeIn;
    private float count;

    [Header("AP Bar")]
    [SerializeField] GameObject apBar;

    private void Awake()
    {
        apBar.SetActive(false);
        isFadeIn = false;
    }
    private void Update()
    {
        count += Time.deltaTime;
        if (isFadeIn) tutorialText.color = Color.Lerp(tutorialText.color, fadeInColor, count * fadeTime);
        else tutorialText.color = Color.Lerp(tutorialText.color, fadeOutColor, count * fadeTime);
    }

    public void FadeInText(string _text)
    {
        Debug.Log("fading text in");
        count = 0;
        tutorialText.text = _text;
        isFadeIn = true;
    }

    public void FadeOutText()
    {

        isFadeIn = false;
        count = 0;
    }

    public void ShowApBar()
    {
        apBar.SetActive(true);
    }
}
