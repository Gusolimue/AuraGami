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
    public float messageTime;
    public float fadeTime = 5f;
    public bool textChanging = false;
     bool isFadeIn;
    private string text;
    public float count;
    private Coroutine textCycleCoroutine;

    [Header("AP Bar")]
    [SerializeField] GameObject apBar;

    private void Awake()
    {
        FadeOutText();
    }
    private void Update()
    {
        count += Time.deltaTime;
        if (isFadeIn) tutorialText.color = Color.Lerp(tutorialText.color, fadeInColor, count / fadeTime);
        else tutorialText.color = Color.Lerp(tutorialText.color, fadeOutColor, count / fadeTime);
    }

    public void FadeInText(string[] _texts)
    {
        isFadeIn = textChanging = true;
        Debug.Log("fading text in");
        textCycleCoroutine = StartCoroutine(TextCycle(_texts));
        //if (textCycleCoroutine != null) StopCoroutine(textCycleCoroutine);
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

    IEnumerator TextCycle(string[] texts)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            tutorialText.text = texts[i];
            count = 0;
            //while (count < fadeTime)
            //{
            //    Debug.Log("fading in message");
            //    count += Time.deltaTime;
            //    Debug.Log(count);
            //    tutorialText.color = Color.Lerp(fadeOutColor, fadeInColor, count / fadeTime);
            //    yield return null;
            //}
            isFadeIn = true;
            yield return new WaitForSeconds(messageTime);
            count = 0;
            isFadeIn = false;
            yield return new WaitUntil(() => count > fadeTime);
            //while (count < fadeTime)
            //{
            //    count += Time.deltaTime;
            //    tutorialText.color = Color.Lerp(fadeInColor, fadeOutColor, count / fadeTime);
            //    yield return null;
            //}
        }
        textChanging = false;
        foreach (var currentText in texts)
        {
        }
    }
}
