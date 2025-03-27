using UnityEngine;
using System.Collections;

public class SettingsButtonBehavior : MonoBehaviour
{
    [SerializeField] public ParticleSystem highlightSettingsNebula;

    private void Awake()
    {
        highlightSettingsNebula.gameObject.SetActive(false);
    }
    public void OnSettingsButtonEnter()
    {
        highlightSettingsNebula.gameObject.SetActive(true);
    }

    public void OnSettingsButtonExit()
    {
        highlightSettingsNebula.gameObject.SetActive(false);
    }

    /*public IEnumerator StarRadiusIncrease()
    {
        buttonStarRadius = 1.1f;

        while (buttonStarRadius > 1f)
        {
            buttonStarRadius -= .1f;
            var shape = buttonStars.shape;
            shape.radius = buttonStarRadius;
            Debug.Log(buttonStarRadius);
        }
        yield return null;
    }*/
}
