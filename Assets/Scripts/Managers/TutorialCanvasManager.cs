using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor.VersionControl;

public class TutorialCanvasManager : MonoBehaviour
{
    [Header("Tutorial Text")]
    [SerializeField] TextMeshProUGUI tutorialText;
    public Color fadeInColor;
    public Color fadeOutColor;
    public int messageTime;
    public float fadeTime = 5f;

    private bool isFadeIn;
    private string text;
    public float count;

    [Header("AP Bar")]
    [SerializeField] GameObject apBar;

    private void Awake()
    {
        apBar.SetActive(false);
        isFadeIn = false;
    }
    private void Update()
    {
       /* count += Time.deltaTime;
        if (isFadeIn) tutorialText.color = Color.Lerp(tutorialText.color, fadeInColor, count * fadeTime);
        else tutorialText.color = Color.Lerp(tutorialText.color, fadeOutColor, count * fadeTime); */
    }

    public void FadeInText(string[] _texts)
    {
        Debug.Log("fading text in");
        count = 0;
        for (int i = 0; i < _texts.Length; i++)
        {
            text = _texts[i];
            tutorialText.text = text;
        }
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

    IEnumerator TextCycle()
    {
        foreach (var _text in text)
        {
            isFadeIn = true;
            count = 0;
            while (count < fadeTime)
            {
                yield return null;
                count += Time.deltaTime;
                if (isFadeIn) tutorialText.color = Color.Lerp(tutorialText.color, fadeInColor, count / fadeTime);
            }
            yield return new WaitForSeconds(messageTime);

            isFadeIn = false;
            count = 0;
            while (count < fadeTime)
            {
                yield return null;
                count += Time.deltaTime;
                if (!isFadeIn) tutorialText.color = Color.Lerp(tutorialText.color, fadeOutColor, count / fadeTime);
            }
        }
    }
}
