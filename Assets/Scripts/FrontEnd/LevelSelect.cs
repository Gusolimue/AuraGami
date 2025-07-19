using UnityEngine;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect Instance;
    public enum Levels { tutorial, exploration, freedom}
    public int curIndex;

    [Header("Level Names")]
    [SerializeField] string[] levelNames;
    [SerializeField] TextMeshProUGUI levelNameTXT;

    private void Awake()
    {
        Instance = this;
        levelNameTXT.text = levelNames[curIndex];
    }

    public void ChangeLevels()
    {
        Levels curLevel = (Levels)curIndex;
        Debug.Log(curIndex);
        LevelSelection(curLevel);
    }

    private void LevelSelection(Levels levels)
    {
        switch (levels)
        {
            case Levels.tutorial:
                FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(1, 2);
                break;

            case Levels.exploration:
                FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(3, 2);
                break;

            case Levels.freedom:
                FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(4, 2);
                break;
        }
    }

    public void ChangeLevelName()
    {
        levelNameTXT.text = levelNames[curIndex];       
    }

   public void OnBackButtonPressed() // When pressed, destroys Canvas_LevelSelect and instantiates Canvas_FrontEnd.
   {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_buttonPressed);
        CanvasManager.Instance.ShowCanvasFE();
        Destroy(this.gameObject);
   }
}
