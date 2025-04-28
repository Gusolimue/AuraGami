using UnityEngine;
using UnityEngine.SceneManagement;

public enum eScene { splashScene, frontEnd, levelFreedom, levelExploration } // Will contain list of levels. Not in use currently!
public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;
    public int currentScene;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void IntoLevelSceneTransition() // Basic transition scene transition method that will be called when Play is pressed 
                                  // in the FrontEnd.
    {
        SceneManager.LoadScene("TargetPrototypeScene - Nathan");
    }

    public void IntoFrontEndSceneTransition() 
    {
        SceneManager.LoadScene("FrontEndPrototypeScene - Nathan");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("<color=yellow>SceneLoaded " + scene.name + "</color>");
        switch ((eScene)scene.buildIndex)
        {
            case eScene.splashScene:
                break;

            case eScene.frontEnd:
                //CanvasManager.Instance.ShowCanvasTitleScreen();
                CanvasManager.Instance.ShowCanvasFE();
                //CanvasManager.Instance.ShowCanvasFEPlaytestTutorial();
                //CanvasManager.Instance.ShowCanvasLevelEnd();
                //CanvasManager.Instance.ShowCanvasLevelSelect();
                //CanvasManager.Instance.ShowCanvasSettings();
                //CanvasManager.Instance.ShowCanvasCredits();
                AudioManager.Instance.PlayMusic(AudioManager.Instance.music_menu_titlescreen);
                currentScene = 0;
                break;


            case eScene.levelExploration:
                currentScene = 1;
                break;

            case eScene.levelFreedom:
                currentScene = 2;
                break;

            default:
                break;
        }
    }

    public void LoadScene(eScene scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}
