using UnityEngine;

public class AccessabilityButtonBehavior : MonoBehaviour
{
    [SerializeField] public GameObject accessabilityConstellationOn;
    [SerializeField] public GameObject accessabilityConstellationOff;

    private void Awake()
    {
        accessabilityConstellationOn.SetActive(false);
        accessabilityConstellationOff.SetActive(true);
    }
    public void OnAccessabilityButtonEnter()
    {
        accessabilityConstellationOn.SetActive(true);
        accessabilityConstellationOff.SetActive(false);
    }

    public void OnAccessabilityButtonExit()
    {
        accessabilityConstellationOn.SetActive(false);
        accessabilityConstellationOff.SetActive(true);
    }
}
