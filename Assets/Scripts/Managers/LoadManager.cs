using UnityEngine;
using UnityEngine.SceneManagement;

public enum eScene { frontEnd, levelOne } // Will contain list of levels. Not in use currently!
public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;
    public int whichScene;

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
        switch((eScene)scene.buildIndex)
        {
            case eScene.frontEnd:
                CanvasManager.Instance.ShowCanvasFE();
                whichScene = 0;
                break;

            case eScene.levelOne:
                whichScene = 1;
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
