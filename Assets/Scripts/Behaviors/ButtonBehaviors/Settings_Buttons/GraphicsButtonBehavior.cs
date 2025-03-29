using UnityEngine;

public class GraphicsButtonBehavior : MonoBehaviour
{
    [SerializeField] public GameObject graphicsConstellationOn;
    [SerializeField] public GameObject graphicsConstellationOff;

    private void Awake()
    {
        graphicsConstellationOn.SetActive(false);
        graphicsConstellationOff.SetActive(true);
    }
    public void OnGraphicsButtonEnter()
    {
        graphicsConstellationOn.SetActive(true);
        graphicsConstellationOff.SetActive(false);
    }

    public void OnGraphicsButtonExit()
    {
        graphicsConstellationOn.SetActive(false);
        graphicsConstellationOff.SetActive(true);
    }
}
