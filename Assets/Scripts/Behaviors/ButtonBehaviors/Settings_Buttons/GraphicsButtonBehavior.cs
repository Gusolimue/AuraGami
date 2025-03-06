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
    public void OnAudioButtonEnter()
    {
        graphicsConstellationOn.SetActive(true);
        graphicsConstellationOff.SetActive(false);
    }

    public void OnAudioButtonExit()
    {
        graphicsConstellationOn.SetActive(false);
        graphicsConstellationOff.SetActive(true);
    }
}
