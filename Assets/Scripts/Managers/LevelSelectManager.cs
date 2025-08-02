using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance;

    [Header("Level Stars")]
    [SerializeField] public Image[] levelStars;
    [SerializeField] Image selectedStar;
    [SerializeField] Image[] starSizes;

    [Header("Level Index Assets")]
    [SerializeField] TextMeshProUGUI levelIndexTXT;
    [SerializeField] TextMeshProUGUI curLevelTXT;

    [Header("Star Positions/Colors")]
    [SerializeField] Transform selectPos;
    [SerializeField] Transform[] leftPos;
    [SerializeField] Transform[] rightPos;
    [SerializeField] Image levelButtonSelectSize;
    [SerializeField] Transform levelButtonPressed;
    private Color colorChange;
    public bool changeColor;
    private bool isLevelTransition;
    private bool isStartingLevel;

    [SerializeField] Color[] starColors;

    private float moveSpeed;
    public int centerIndex = 0;
    private float fadeInDuration;
    private float count;

    private void Awake()
    {
        Instance = this;
        PositionStarsInstant();

        levelIndexTXT.text = LevelSelect.Instance.levels.Length.ToString();
        UpdateCurrentLevel();

        colorChange = new Color(1, 1, 1, 1);
        selectedStar.gameObject.SetActive(false);

        for (int i = 0; i < levelStars.Length; i++)
        {
            if (levelStars[i].transform.position == leftPos[1].position || levelStars[i].transform.position == rightPos[1].position)
            {
                levelStars[i].color = starColors[1];
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < levelStars.Length; i++)
        {
            Transform targetPos = GetStarPos(i);
            moveSpeed += .1f;
            levelStars[i].transform.position = Vector3.Lerp(levelStars[i].transform.position, targetPos.position, Time.deltaTime * moveSpeed);

            bool isCenter = (i == centerIndex);
            if (isCenter && !isStartingLevel) levelStars[i].transform.localScale = Vector2.Lerp(levelStars[i].transform.localScale, starSizes[0].transform.localScale, Time.deltaTime * moveSpeed);
            else if (!isCenter && !isStartingLevel) levelStars[i].transform.localScale = Vector2.Lerp(levelStars[i].transform.localScale, starSizes[1].transform.localScale, Time.deltaTime * moveSpeed);

            bool isVisible = targetPos == selectPos || targetPos == leftPos[0] || targetPos == rightPos[0];
            Color targetColor = levelStars[i].color;
            targetColor.a = isVisible ? 1f : 0f;

            levelStars[i].color = Color.Lerp(levelStars[i].color, targetColor, Time.deltaTime * 15);

            if (targetPos == leftPos[0] && LevelSelect.Instance.levels.Length > 0)
            {
                levelStars[i].color = Color.Lerp(levelStars[i].color, LevelSelect.Instance.levels[0].levelStarColor, Time.deltaTime * 10f);
            }
            else if (targetPos == selectPos && LevelSelect.Instance.levels.Length > 1 && !changeColor)
            {
                if (!isStartingLevel) levelStars[i].color = Color.Lerp(levelStars[i].color, LevelSelect.Instance.levels[1].levelStarColor, Time.deltaTime * 10f);
            }
            else if (targetPos == rightPos[0] && LevelSelect.Instance.levels.Length > 2)
            {
                levelStars[i].color = Color.Lerp(levelStars[i].color, LevelSelect.Instance.levels[2].levelStarColor, Time.deltaTime * 10f);
            }

            if (levelStars[i].transform.position == selectPos.position && changeColor && !isStartingLevel)
            {
                levelStars[i].color = Color.Lerp(levelStars[i].color, colorChange, Time.deltaTime * 5f);
                levelStars[i].transform.localScale = Vector3.Lerp(levelStars[i].transform.localScale, levelButtonSelectSize.transform.localScale, Time.deltaTime * 5f);
            }

            if ( levelStars[i].transform.position == selectPos.position && isLevelTransition) 
            {
                count += Time.deltaTime;
                selectedStar.transform.localScale = Vector3.Lerp(selectedStar.transform.localScale, levelButtonPressed.transform.localScale, count /10);
            }
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

    public void MoveLeft()
    {
        LevelSelect.Instance.RotateLevelsLeft();
        centerIndex = (centerIndex + 1) % levelStars.Length;
        LevelSelect.Instance.curIndex = (LevelSelect.Instance.curIndex + 1) % levelStars.Length;
        moveSpeed = 1f;

        StartCoroutine(CurrentLevelTextTransition());
        LevelSelect.Instance.ChangeLevelName();
        UpdateCurrentLevel();
    }

    public void MoveRight()
    {
        LevelSelect.Instance.RotateLevelsRight();
        centerIndex = (centerIndex - 1 + levelStars.Length) % levelStars.Length;
        LevelSelect.Instance.curIndex = (LevelSelect.Instance.curIndex - 1 + levelStars.Length) % levelStars.Length;
        moveSpeed = 1f;

        StartCoroutine(CurrentLevelTextTransition());
        LevelSelect.Instance.ChangeLevelName();
        UpdateCurrentLevel();
    }

    private void UpdateCurrentLevel()
    {
        curLevelTXT.text = LevelSelect.Instance.levels[1].curStar.ToString();
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

    public IEnumerator LevelTransition()
    {
        isStartingLevel = true;
        for (int i = 0; i < levelStars.Length; i++)
        {
            if (levelStars[i].transform.position == selectPos.position)
            {
                levelStars[i].color = new Color(1, 1, 1, 1);
                float targetColor = 1;

                while (targetColor > 0)
                {
                    targetColor -= Time.deltaTime / .5f;
                    levelStars[i].color = new Color(targetColor, targetColor, targetColor, 1);
                    yield return null;
                }
                yield return new WaitForSeconds(.2f);
                selectedStar.gameObject.SetActive(true);
                isLevelTransition = true;

                int curLevel = LevelSelect.Instance.levels[1].whichLevel;
                FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(curLevel, 1);
            }
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
