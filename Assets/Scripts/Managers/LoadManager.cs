using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum eScene { splashScene, tutorial, frontEnd, levelFreedom, levelExploration } 
public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;
    public bool isTutorial;
    public int isTitleScreen;

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

            case eScene.tutorial:
                break;

            case eScene.frontEnd:
                if (isTitleScreen == 0)
                {
                    //CanvasManager.Instance.ShowCanvasFE();
                    //CanvasManager.Instance.ShowCanvasLevelSelect();
                    CanvasManager.Instance.ShowCanvasTitleScreen();
                    //CanvasManager.Instance.ShowCanvasSettings();
                    //CanvasManager.Instance.ShowCanvasCredits();
                }
                else if (isTitleScreen == 1)
                {
                    CanvasManager.Instance.ShowCanvasFE();
                    //CanvasManager.Instance.ShowCanvasFEPlaytestTutorial();
                    //CanvasManager.Instance.ShowCanvasLevelEnd();
                    //CanvasManager.Instance.ShowCanvasLevelSelect();
                    //CanvasManager.Instance.ShowCanvasSettings();
                    //CanvasManager.Instance.ShowCanvasCredits();
                }

                break;


            case eScene.levelExploration:
                break;

            case eScene.levelFreedom:
                break;

            default:
                break;
        }
    }

    public void LoadScene (eScene scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    private IEnumerator LoadSceneAsync(eScene scene)
    {
       SceneManager.LoadSceneAsync((int)scene);
       yield return null;
    }
}
