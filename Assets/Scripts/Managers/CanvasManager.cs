using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private void Awake()
    {
        Instance = this;
        ShowCanvasFE();
    }

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
}
