using UnityEngine;
using System.Collections;

public class UIButtonBehavior : MonoBehaviour
{
    [SerializeField] public ParticleSystem highlightNebula;

    private void Awake()
    {
        highlightNebula.gameObject.SetActive(false);
    }
    public void OnButtonEnter()
    {
        highlightNebula.gameObject.SetActive(true);
    }

    public void OnButtonExit()
    {
        highlightNebula.gameObject.SetActive(false);
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
