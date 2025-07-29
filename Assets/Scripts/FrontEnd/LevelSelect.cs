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

    [Header("Levels")]
    public soLevelStar[] levels;

    [Header("Level Names")]
    [SerializeField] string[] levelNames;
    [SerializeField] TextMeshProUGUI levelNameTXT;

    [Header("Level Star/Button Assets")]
    [SerializeField] Image levelButton;
    [SerializeField] Image levelButtonSizeChange;
    [SerializeField] Color[] levelStarColors;

    private bool isTransitioning;
    private int isHovering;
    private float count;

    private void Awake()
    {
        Instance = this;
        levelNameTXT.text = levels[1].levelName;
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isTransitioning) levelButton.transform.localScale = Vector3.Lerp(levelButton.transform.localScale, levelButtonSizeChange.transform.localScale, count / 200);
        if (isHovering == 1) levelButton.color = Color.Lerp(levelButton.color, levelStarColors[0], Time.deltaTime * 10);
        else if (isHovering == 2) levelButton.color = Color.Lerp(levelButton.color, levelStarColors[1], Time.deltaTime * 10);

    }

    public void ChangeLevels()
    {
        if(AudioManager.idleInstance.isValid()) AudioManager.idleInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        StartCoroutine(LevelTransition());
    }

    public void OnHover()
    {
        isHovering = 1;

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
    }

    public void OnHoverExit()
    {
        isHovering = 2;
    }

    public void RotateLevelsRight()
    {
        if (levels == null || levels.Length < 2)
            return;

        soLevelStar last = levels[levels.Length - 1];
        for (int i = levels.Length - 1; i > 0; i--)
        {
            levels[i] = levels[i - 1];
        }
        levels[0] = last;
    }

    public void RotateLevelsLeft()
    {
        if (levels == null || levels.Length < 2)
            return;

        soLevelStar first = levels[0];
        for (int i = 0; i < levels.Length - 1; i++)
        {
            levels[i] = levels[i + 1];
        }
        levels[levels.Length - 1] = first;
    }

    /*private void LevelSelection(Levels levels)
    {
        switch (levels)
        {
            case Levels.tutorial:
                FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(1, 1);
                break;

            case Levels.exploration:
                FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(3, 1);
                break;

            case Levels.freedom:
                FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(4, 1);
                break;
        }
    }*/

    public void ChangeLevelName()
    {
        StartCoroutine(LevelNameTextTranstion());
        //levelNameTXT.text = levelNames[curIndex];
        levelNameTXT.text = levels[1].levelName;
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
        float color = 1f;
        float fadeInDuration = .5f;

        Color curColor = levelButton.color;
        levelButton.color = new Color(curColor.r, curColor.g, curColor.b, 0);

        while (color > 0f)
        {
            color -= Time.deltaTime / fadeInDuration;
            levelButton.color = new Color(color, color, color, levelButton.color.a);
            yield return new WaitForSecondsRealtime(.01f);
        }
        isHovering = 3;
        yield return new WaitForSeconds(.3f);

        int curLevel = levels[1].whichLevel;
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(curLevel, 1);
    }

    public void OnBackButtonPressed() // When pressed, destroys Canvas_LevelSelect and instantiates Canvas_FrontEnd.
   {
        CanvasManager.Instance.ShowCanvasFE();

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
   }
}
