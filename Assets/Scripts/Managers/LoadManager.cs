using UnityEngine;
using UnityEngine.SceneManagement;

public enum eScene { splashScene, frontEnd, levelFreedom, levelExploration } // Will contain list of levels. Not in use currently!
public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;
    public int isTitleScreen;
    public int currentScene;

    private void Start()
    {
        isTitleScreen = 0;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destory New LoadManager");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("<color=yellow>SceneLoaded " + scene.name + "</color>");
        switch ((eScene)scene.buildIndex)
        {
            case eScene.splashScene:
                break;

            case eScene.frontEnd:
                if (isTitleScreen == 0)
                {
                    //CanvasManager.Instance.ShowCanvasFE();
                    CanvasManager.Instance.ShowCanvasTitleScreen();
                    //CanvasManager.Instance.ShowCanvasSettings();
                }
                else if (isTitleScreen == 1)
                {
                    CanvasManager.Instance.ShowCanvasFE();
                    //CanvasManager.Instance.ShowCanvasFEPlaytestTutorial();
                    //CanvasManager.Instance.ShowCanvasLevelEnd();
                    //CanvasManager.Instance.ShowCanvasLevelSelect();

                    //CanvasManager.Instance.ShowCanvasCredits();
                }

                AudioManager.Instance.PlayMusic(AudioManager.Instance.music_menu_titlescreen);
                currentScene = 0;
                break;


            case eScene.levelExploration:
                currentScene = 1;
                break;

            case eScene.levelFreedom:
                //CanvasManager.Instance.ShowCanvasPauseMenu();
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
