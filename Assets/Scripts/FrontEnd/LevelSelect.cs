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
    [SerializeField] Color[] levelStarColors;

    private bool isTransitioning;
    private int isHovering;
    private float count;

    private void Awake()
    {
        Instance = this;
        levelNameTXT.text = levels[1].levelName;
    }

    public void ChangeLevels()
    {
        if(AudioManager.idleInstance.isValid()) AudioManager.idleInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        StartCoroutine(LevelSelectManager.Instance.LevelTransition());
    }

    public void OnHover()
    {
        LevelSelectManager.Instance.changeColor = true;
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .2f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_menuHoverSmall);
    }

    public void OnHoverExit()
    {
        LevelSelectManager.Instance.changeColor = false;
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
    public void ChangeLevelName()
    {
        StartCoroutine(LevelNameTextTranstion());
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

    public void OnBackButtonPressed() // When pressed, destroys Canvas_LevelSelect and instantiates Canvas_FrontEnd.
   {
        CanvasManager.Instance.ShowCanvasFE();

        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        Destroy(this.gameObject);
   }
}
