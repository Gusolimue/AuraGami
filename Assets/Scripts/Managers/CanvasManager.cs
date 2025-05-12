using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else if (Instance != this)
        //{
        //    Destroy(gameObject);
        //}
        Instance = this;
    }

    //When called, instantiates the FrontEnd canvas, all other methods act similarly for their respective canvas'.
    //FRONT END CANVAS'

    public void ShowCanvasTitleScreen()
    {
        Instantiate(Resources.Load("Canvas_FrontEnd/" + "Canvas_TitleScreen") as GameObject);
    }
    public void ShowCanvasFE() 
    {
        Instantiate(Resources.Load("Canvas_FrontEnd/" + "Canvas_FE") as GameObject);
    }

    public void ShowCanvasFEPlaytestTutorial()
    {
        Instantiate(Resources.Load("Canvas_FrontEnd/" + "Canvas_FE_Tutorial_Playtest") as GameObject);
    }

    public void ShowCanvasLevelSelect()
    {
        Instantiate(Resources.Load("Canvas_FrontEnd/" + "Canvas_LevelSelect") as GameObject);
    }

    public void ShowCanvasSettings()
    {
        Instantiate(Resources.Load("Canvas_FrontEnd/" + "Canvas_Settings") as GameObject);
    }

    public void ShowCanvasCredits()
    {
        Instantiate(Resources.Load("Canvas_FrontEnd/" + "Canvas_Credits") as GameObject);
    }

    //PAUSE MENU CAVAS'

    public void ShowCanvasPauseMenu()
    {
        Instantiate(Resources.Load("Canvas_PauseMenu/" + "Canvas_PauseMenu") as GameObject);
    }

    //GAME STATE CAVAS'

    public void ShowCanvasStageFail()
    {
        Instantiate(Resources.Load("Canvas_GameStates/" + "Canvas_StageFail") as GameObject);
    }

    public void ShowCanvasLevelEnd()
    {
        Instantiate(Resources.Load("Canvas_GameStates/" + "Canvas_LevelEnd") as GameObject);
    }
}
