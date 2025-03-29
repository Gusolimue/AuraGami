using UnityEngine;

public class CreditsButtonBehavior : MonoBehaviour
{
    [SerializeField] public ParticleSystem highlightCreditsNebula;

    private void Awake()
    {
        highlightCreditsNebula.gameObject.SetActive(false);
    }
    public void OnCreditsButtonEnter()
    {
        highlightCreditsNebula.gameObject.SetActive(true);
    }

    public void OnCreditsButtonExit()
    {
        highlightCreditsNebula.gameObject.SetActive(false);
    }
}
