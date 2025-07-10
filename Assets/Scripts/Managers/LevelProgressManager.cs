using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager Instance;
    [Header("Message")]
    [SerializeField] TextMeshProUGUI messageTXT;
    public string message;

    [Header("Stars")]
    [SerializeField] Image[] stars;
    [SerializeField] Color starActive;

    [Header("Progress Connectors")]
    [SerializeField] Slider progressConnector;
    public float fillSpeed = 10f;
    public bool isProgress;
    public int curStage;

    public float targetValue;
    private float count;

    private void Awake()
    {
        Instance = this;
        curStage = LevelManager.currentStageIndex - 1;

        if (curStage < 4) messageTXT.text = "Do You Continue?";
        if (curStage == 1) StartCoroutine(StarColorChange(1));
        if (curStage == 2) StartCoroutine(StarColorChange(2));
        if (curStage == 3) messageTXT.text = "And You Continue...";
    }

    private void Update()
    {      
        if (curStage == 0)
        {
            targetValue = .34f;
            count += Time.deltaTime;
            progressConnector.value = Mathf.Lerp(progressConnector.value, targetValue, count / fillSpeed);
        }

        if (curStage == 1)
        {
            targetValue = .68f;
            count += Time.deltaTime;
            progressConnector.value = Mathf.Lerp(progressConnector.value, targetValue, count / fillSpeed);

        }

        if (curStage == 2)
        {
            targetValue = 1.02f;
            count += Time.deltaTime;
            progressConnector.value = Mathf.Lerp(progressConnector.value, targetValue, count / fillSpeed);
        }
    }
    
    private IEnumerator StarColorChange(int starNum)
    {
        yield return new WaitForSeconds(.5f);
        float alpha = 0f;
        float colorChangeDuration = 1f;
       
        if (starNum == 1)
        {
            while (alpha < 1f)
            {
                alpha += Time.deltaTime / colorChangeDuration;
                stars[0].color = Color.Lerp(stars[0].color, starActive, alpha);
                yield return null;
            }
        }

        if (starNum == 2)
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

            while (alpha < 1f)
            {
                alpha += Time.deltaTime / colorChangeDuration;
                stars[2].color = Color.Lerp(stars[2].color, starActive, alpha);
                yield return null;
            }
        }
    }
}
