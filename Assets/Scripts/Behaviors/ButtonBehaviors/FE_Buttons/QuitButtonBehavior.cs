using UnityEngine;

public class QuitButtonBehavior : MonoBehaviour
{
    [SerializeField] public ParticleSystem highlightQuitNebula;

    private void Awake()
    {
        highlightQuitNebula.gameObject.SetActive(false);
    }
    public void OnQuitButtonEnter()
    {
        highlightQuitNebula.gameObject.SetActive(true);
    }

    public void OnQuitButtonExit()
    {
        highlightQuitNebula.gameObject.SetActive(false);
    }
}
