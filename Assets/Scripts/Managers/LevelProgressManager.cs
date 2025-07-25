using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager Instance;

    [SerializeField] GameObject[] toShow;
    [Header("Message")]
    [SerializeField] TextMeshProUGUI messageTXT;
    public string message;

    [Header("Stars")]
    [SerializeField] Image[] stars;
    [SerializeField] Color starActive;

    [Header("Progress Connectors")]
    [SerializeField] Slider progressConnector;
    public float fillSpeed;
    public bool isProgress;
    public int curStage;

    public float targetValue;
    private float count;
    public bool isCheat;

    private void Awake()
    {
        Instance = this;
        curStage = LevelManager.currentStageIndex;

        toShow[0].SetActive(true);
        toShow[1].SetActive(true);

        if (curStage < 4) messageTXT.text = "Do You Continue?";
        if (curStage == 1) StartCoroutine(StarColorChange(2));
        if (curStage == 2) StartCoroutine(StarColorChange(3));
        if (curStage == 3) 
        {
            StartCoroutine(StarColorChange(4));
            messageTXT.text = "And You Continue...";
        }
        if (isCheat)
        {
            curStage = 4;
            StartCoroutine(StarColorChange(4));
            messageTXT.text = "And You Continue...";
        }
    }

    private void Update()
    {      
        if (curStage == 1)
        {
            targetValue = .34f;
            count += Time.deltaTime;
            progressConnector.value = Mathf.Lerp(progressConnector.value, targetValue, count / fillSpeed);
        }

        if (curStage == 2)
        {
            targetValue = .68f;
            count += Time.deltaTime;
            progressConnector.value = Mathf.Lerp(progressConnector.value, targetValue, count / fillSpeed);

        }

        if (curStage == 3)
        {
            targetValue = 1.02f;
            count += Time.deltaTime;
            progressConnector.value = Mathf.Lerp(progressConnector.value, targetValue, count / fillSpeed);
        }

        if (curStage == 4)
        {
            targetValue = 1.02f;
            count += Time.deltaTime;
            progressConnector.value = Mathf.Lerp(progressConnector.value, targetValue, count / fillSpeed);
        }
    }
    
    private IEnumerator StarColorChange(int starNum)
    {
        yield return new WaitForSeconds(.8f);
        float alpha = 0f;
        float colorChangeDuration = 1f;

        if (starNum == 2)
        {
            while (alpha < 1f)
            {
                alpha += Time.deltaTime / colorChangeDuration;
                stars[0].color = Color.Lerp(stars[0].color, starActive, alpha);
                yield return null;
            }
            alpha = 0;
        }

        if (starNum == 3)
        {
            while (alpha < 1f)
            {
                alpha += Time.deltaTime / colorChangeDuration;
                stars[0].color = Color.Lerp(stars[0].color, starActive, alpha);
                yield return null;
            }
            alpha = 0;
            yield return new WaitForSeconds(.1f);

            while (alpha < 1f)
            {
                alpha += Time.deltaTime / colorChangeDuration;
                stars[1].color = Color.Lerp(stars[1].color, starActive, alpha);
                yield return null;
            }
            alpha = 0;
            yield return new WaitForSeconds(.1f);
        }

        if (starNum == 4)
        {
            while (alpha < 1f)
            {
                alpha += Time.deltaTime / colorChangeDuration;
                stars[0].color = Color.Lerp(stars[0].color, starActive, alpha);
                yield return null;
            }
            alpha = 0;
            yield return new WaitForSeconds(.1f);

            while (alpha < 1f)
            {
                alpha += Time.deltaTime / colorChangeDuration;
                stars[1].color = Color.Lerp(stars[1].color, starActive, alpha);
                yield return null;
            }
            alpha = 0;
            yield return new WaitForSeconds(.1f);

            while (alpha < 1f)
            {
                alpha += Time.deltaTime / colorChangeDuration;
                stars[2].color = Color.Lerp(stars[2].color, starActive, alpha);
                yield return null;
            }
        }
    }
}
