using UnityEngine;
using UnityEngine.SceneManagement;

public enum eScene { frontEnd, levelOne } // Will contain list of levels. Not in use currently!
public class SceneMgr : MonoBehaviour
{
    public static SceneMgr Instance;

    private void Awake()
    {
        Instance = this;
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
}
