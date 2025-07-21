using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect Instance;
    public enum Levels { tutorial, exploration, freedom}
    public int curIndex;
    private float fadeInDuration;

    [Header("Level Names")]
    [SerializeField] string[] levelNames;
    [SerializeField] TextMeshProUGUI levelNameTXT;

    [Header("Level Star/Button Assets")]
    [SerializeField] Image levelButton;
    [SerializeField] Image levelButtonSizeChange;

    private bool isTransitioning;
    private float count;

    private void Awake()
    {
        Instance = this;
        levelNameTXT.text = levelNames[curIndex];
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isTransitioning) levelButton.transform.localScale = Vector3.Lerp(levelButton.transform.localScale, levelButtonSizeChange.transform.localScale, count / 200);
    }

    public void ChangeLevels()
    {
        StartCoroutine(LevelTransition());
    }

    private void LevelSelection(Levels levels)
    {
        switch (levels)
        {
            case Levels.tutorial:
                FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(1, 0);
                break;

            case Levels.exploration:
                FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(3, 0);
                break;

            case Levels.freedom:
                FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(4, 2);
                break;
        }
    }

    public void ChangeLevelName()
    {
        StartCoroutine(LevelNameTextTranstion());
        levelNameTXT.text = levelNames[curIndex];       
    }

    public IEnumerator LevelNameTextTranstion()
    {
        float alpha = 0f;
        fadeInDuration = 1f;

        Color curColor = levelNameTXT.color;
        levelNameTXT.color = new Color(curColor.r, curColor.g, curColor.b, 0);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeInDuration;
            levelNameTXT.color = new Color(levelNameTXT.color.r, levelNameTXT.color.g,
                levelNameTXT.color.b, alpha);
            yield return new WaitForSecondsRealtime(.01f);
        }
    }

    private IEnumerator LevelTransition()
    {
        float alpha = 0f;
        float fadeInDuration = .5f;

        Color curColor = levelButton.color;
        levelButton.color = new Color(curColor.r, curColor.g, curColor.b, 0);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeInDuration;
            levelButton.color = new Color(levelButton.color.r, levelButton.color.g,
                levelButton.color.b, alpha);
            yield return new WaitForSecondsRealtime(.01f);
        }
        yield return new WaitForSeconds(.5f);
        isTransitioning = true;
        yield return new WaitForSeconds(.2f);

        Levels curLevel = (Levels)curIndex;
        LevelSelection(curLevel);
    }

    public void OnBackButtonPressed() // When pressed, destroys Canvas_LevelSelect and instantiates Canvas_FrontEnd.
   {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        CanvasManager.Instance.ShowCanvasFE();
        Destroy(this.gameObject);
   }
}
