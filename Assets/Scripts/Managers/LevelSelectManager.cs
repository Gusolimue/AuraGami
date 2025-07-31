using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance;

    [Header("Level Stars")]
    [SerializeField] public Image[] levelStars;
    [SerializeField] Image[] starSizes;

    [Header("Level Index Assets")]
    [SerializeField] TextMeshProUGUI levelIndexTXT;
    [SerializeField] TextMeshProUGUI curLevelTXT;
    
    [Header("Star Positions/Colors")]
    [SerializeField] Transform selectPos;
    [SerializeField] Transform[] leftPos;
    [SerializeField] Transform[] rightPos;

    [SerializeField] Color[] starColors;

    private float moveSpeed = 5f;
    public int centerIndex = 0;
    private float fadeInDuration;

    private void Awake()
    {
        Instance = this;
        PositionStarsInstant();

        levelIndexTXT.text = levelStars.Length.ToString();
        UpdateCurrentLevel();
    }

    private void Update()
    {
        for (int i = 0; i < levelStars.Length; i++)
        {
            Transform targetPos = GetStarPos(i);

            levelStars[i].transform.position = Vector3.Lerp(levelStars[i].transform.position, targetPos.position, Time.deltaTime * moveSpeed);
         
            bool isCenter = (i == centerIndex);

            levelStars[i].transform.localScale = Vector2.Lerp(levelStars[i].transform.localScale, isCenter ? starSizes[0].transform.localScale : starSizes[1].transform.localScale,
                Time.deltaTime * moveSpeed);

            bool isVisible = targetPos == selectPos || targetPos == leftPos[0] || targetPos == rightPos[0];
            Color targetColor = levelStars[i].color;
            targetColor.a = isVisible ? 1f : 0f;

            levelStars[i].color = Color.Lerp(levelStars[i].color, targetColor, Time.deltaTime * 15);
        }
    }

    private Transform GetStarPos(int starIndex)
    {
        int relativeIndex = (starIndex - centerIndex + levelStars.Length) % levelStars.Length;

        if (starIndex == centerIndex)
            return selectPos;

        if (levelStars.Length <= 3)
        {
            if (relativeIndex == 1 || relativeIndex == -levelStars.Length + 1)
                return rightPos[0];
            else if (relativeIndex == levelStars.Length - 1)
                return leftPos[0];
            else
                return leftPos[leftPos.Length - 1]; 
        }
        else
        {
            if (relativeIndex == 1 || relativeIndex == -levelStars.Length + 1)
                return rightPos[0];
            else if (relativeIndex == 2 || relativeIndex == -levelStars.Length + 2)
                return rightPos[1];
            else if (relativeIndex == levelStars.Length - 1)
                return leftPos[0];
            else if (relativeIndex == levelStars.Length - 2)
                return leftPos[1];
            else
                return leftPos[leftPos.Length - 1];
        }
    }
    private void PositionStarsInstant()
    {
        for (int i = 0; i < levelStars.Length; i++)
        {
            levelStars[i].transform.position = GetStarPos(i).position;
        }
    }

    public void MoveRight()
    {
        centerIndex = (centerIndex + 1) % levelStars.Length;
        LevelSelect.Instance.curIndex = (LevelSelect.Instance.curIndex + 1) % levelStars.Length;
        StartCoroutine(CurrentLevelTextTransition());
        LevelSelect.Instance.ChangeLevelName();
        UpdateCurrentLevel();
    }

    public void MoveLeft()
    {
        centerIndex = (centerIndex - 1 + levelStars.Length) % levelStars.Length;
        LevelSelect.Instance.curIndex = (LevelSelect.Instance.curIndex - 1 + levelStars.Length) % levelStars.Length;
        StartCoroutine(CurrentLevelTextTransition());
        LevelSelect.Instance.ChangeLevelName();
        UpdateCurrentLevel();
    }

    private void UpdateCurrentLevel()
    {
        int curLevel = centerIndex + 1;
        curLevelTXT.text = curLevel.ToString();
    }

    public IEnumerator CurrentLevelTextTransition()
    {
        float alpha = 0f;
        fadeInDuration = 1f;

        Color curColor = curLevelTXT.color;
        curLevelTXT.color = new Color(curColor.r, curColor.g, curColor.b, 0);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeInDuration;
            curLevelTXT.color = new Color(curLevelTXT.color.r, curLevelTXT.color.g,
                curLevelTXT.color.b, alpha);
            yield return new WaitForSecondsRealtime(.01f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Level_Exploration"))
        {
            other.gameObject.SetActive(false);
            FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(3, 1);
            Debug.Log("LEVEL_EXPLORATION");
        }

        if (other.CompareTag("Level_Freedom"))
        {
            other.gameObject.SetActive(false);
            FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(4, 1);
            Debug.Log("LEVEL_FREEDOM");
        }
    }
}
