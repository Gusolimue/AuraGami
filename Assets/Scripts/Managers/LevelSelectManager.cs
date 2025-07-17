using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance;

    [Header("Level Stars")]
    [SerializeField] Image[] levelStars;
    [SerializeField] Image[] starSizes;

    [SerializeField] Transform selectPos;
    [SerializeField] Transform[] leftPos;
    [SerializeField] Transform[] rightPos;

    [SerializeField] Color[] starColors;

    private float moveSpeed = 10f;
    private int centerIndex = 0;

    private void Awake()
    {
        Instance = this;
        PositionStarsInstant();
    }

    private void Update()
    {
        // Continuously lerp to their target positions and sizes
        for (int i = 0; i < levelStars.Length; i++)
        {
            Transform targetPos = GetStarPos(i);

            levelStars[i].transform.position = Vector3.Lerp(
                levelStars[i].transform.position,
                targetPos.position,
                Time.deltaTime * moveSpeed
            );

           /* bool isCenter = (i == centerIndex);

            levelStars[i].rectTransform.sizeDelta = Vector2.Lerp(
                levelStars[i].rectTransform.sizeDelta,
                isCenter ? starSizes[0].transform.localScale : starSizes[1].transform.localScale,
                Time.deltaTime * moveSpeed
            );

            levelStars[i].color = Color.Lerp(
                levelStars[i].color,
                isCenter ? starColors[0] : starColors[1],
                Time.deltaTime * moveSpeed
            );*/
        }
    }

    private Transform GetStarPos(int starIndex)
    {
        int relativeIndex = (starIndex - centerIndex + levelStars.Length) % levelStars.Length;

        if (starIndex == centerIndex)
            return selectPos;
        else if (relativeIndex == 1 || relativeIndex == -levelStars.Length + 1)
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
    private void PositionStarsInstant()
    {
        // Place them immediately in the right positions on start
        for (int i = 0; i < levelStars.Length; i++)
        {
            levelStars[i].transform.position = GetStarPos(i).position;
        }
    }

    public void MoveRight()
    {
        centerIndex = (centerIndex + 1) % levelStars.Length;
    }

    public void MoveLeft()
    {
        centerIndex = (centerIndex - 1 + levelStars.Length) % levelStars.Length;
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

    public void OnExplorationPressed()
    {
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(3, 1);
    }
}
