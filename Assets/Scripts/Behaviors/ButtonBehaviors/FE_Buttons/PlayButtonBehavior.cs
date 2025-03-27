using UnityEngine;

public class PlayButtonBehavior : MonoBehaviour
{
    [SerializeField] public ParticleSystem highlightPlayNebula;

    private void Awake()
    {
        highlightPlayNebula.gameObject.SetActive(false);
    }
    public void OnPlayButtonEnter()
    {
        highlightPlayNebula.gameObject.SetActive(true);
    }

    public void OnPlayButtonExit()
    {
        highlightPlayNebula.gameObject.SetActive(false);
    }
}
