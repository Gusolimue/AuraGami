using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager Instance;
    [SerializeField] Image bgFadeOut;
    [SerializeField] Color fadeColor;

    [Header("Message")]
    [SerializeField] TextMeshProUGUI messageTXT;
    public string message;

    [Header("Stars")]
    [SerializeField] Image[] stars;
    [SerializeField] Color starActive;

    [Header("Progress Connectors")]
    [SerializeField] Slider progressConnector;
    public float fillSpeed;
    private int curStage;

    private float targetValue;
    float additionvalValue;
    private float count;
    public bool isCheat;

    private void Awake()
    {
        Instance = this;
        curStage = LevelManager.currentStageIndex;
        StartCoroutine(PlaySound());
        PauseManager.Instance.ShowLineInteractor();

        if (curStage <= 3) messageTXT.text = "Do You Continue?";
        if (curStage >= 3) messageTXT.text = "And We Continue...";
        if (isCheat)
        {
            curStage = 4;
            messageTXT.text = "And You Continue...";
        }
        additionvalValue = APManager.Instance.lastStageAP;
        targetValue = additionvalValue + curStage;
        targetValue /= 3;
        targetValue += .01f;
    }

    private void Update()
    {
        count += Time.deltaTime;
        progressConnector.value = Mathf.Lerp(progressConnector.value, targetValue, count/fillSpeed);


        Color curColor = bgFadeOut.color;
        float newAlpha = Mathf.Lerp(curColor.a, fadeColor.a, count / 15);
        bgFadeOut.color = new Color(curColor.r, curColor.g, curColor.b, newAlpha);

        if (progressConnector.value >= .34f) stars[0].color = Color.Lerp(stars[0].color, starActive, Time.deltaTime * 15f);
        if (progressConnector.value >= .68f) stars[1].color = Color.Lerp(stars[1].color, starActive, Time.deltaTime * 15f);
        if (progressConnector.value >= 1.02f) stars[2].color = Color.Lerp(stars[2].color, starActive, Time.deltaTime * 15f);
    }

    private IEnumerator PlaySound()
    {
        while (progressConnector.value <= .34f) yield return null;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);

        while (progressConnector.value <= .68f) yield return null;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);

        while (progressConnector.value <= 1.02f) yield return null;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
    }
}
