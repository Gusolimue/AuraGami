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
}
