using UnityEngine;
using UnityEngine.UI;

public class CreditsButtonBehavior : MonoBehaviour
{
    [SerializeField] public GameObject constellationOn;
    [SerializeField] public GameObject constellationOff;

    private void Awake()
    {
        constellationOn.SetActive(false);
        constellationOff.SetActive(true);
    }


    public void OnButtonEnter()
    {
        constellationOn.SetActive(true);
        constellationOff.SetActive(false);
    }

    public void OnButtonExit()
    {
        constellationOn.SetActive(false);
        constellationOff.SetActive(true);
    }
}
