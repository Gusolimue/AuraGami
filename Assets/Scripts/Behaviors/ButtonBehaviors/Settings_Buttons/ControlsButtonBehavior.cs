using UnityEngine;

public class ControlsButtonBehavior : MonoBehaviour
{
    [SerializeField] public GameObject controlsConstellationOn;
    [SerializeField] public GameObject controlsConstellationOff;

    private void Awake()
    {
        controlsConstellationOn.SetActive(false);
        controlsConstellationOff.SetActive(true);
    }
    public void OnControlsButtonEnter()
    {
        controlsConstellationOn.SetActive(true);
        controlsConstellationOff.SetActive(false);
    }

    public void OnControlsButtonExit()
    {
        controlsConstellationOn.SetActive(false);
        controlsConstellationOff.SetActive(true);
    }
}
