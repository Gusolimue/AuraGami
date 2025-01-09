using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private void Awake()
    {
        Instance = this;
        ShowCanvasFE();
    }

    //When called, instantiates the FrontEnd canvas, all other methods act similarly for their respective canvas'.
    //FRONT END CANVAS'
    public void ShowCanvasFE() 
    {
        Instantiate(Resources.Load("Canvas_FrontEnd/" + "Canvas_FE") as GameObject);
    }

    public void ShowCanvasLevelSelect()
    {
        Instantiate(Resources.Load("Canvas_FrontEnd/" + "Canvas_LevelSelect") as GameObject);
    }

    public void ShowCanvasSettings()
    {
        Instantiate(Resources.Load("Canvas_FrontEnd/" + "Canvas_Settings") as GameObject);
    }

    //PAUSE MENU CAVAS'

    public void ShowCanvasPauseMenu()
    {
        Instantiate(Resources.Load("Canvas_PauseMenu/" + "Canvas_PauseMenu") as GameObject);
    }
}
