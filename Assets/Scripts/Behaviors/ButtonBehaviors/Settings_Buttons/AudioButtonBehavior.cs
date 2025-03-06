using UnityEngine;

public class AudioButtonBehavior : MonoBehaviour
{
    [SerializeField] public GameObject audioConstellationOn;
    [SerializeField] public GameObject audioConstellationOff;

    private void Awake()
    {
        audioConstellationOn.SetActive(false);
        audioConstellationOff.SetActive(true);
    }
    public void OnAudioButtonEnter()
    {
        audioConstellationOn.SetActive(true);
        audioConstellationOff.SetActive(false);
        Debug.Log("Enter");
    }

    public void OnAudioButtonExit()
    {
        audioConstellationOn.SetActive(false);
        audioConstellationOff.SetActive(true);
        Debug.Log("Exit");
    }
}
