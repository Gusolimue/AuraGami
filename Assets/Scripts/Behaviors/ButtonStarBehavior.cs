using UnityEngine;
using System.Collections;

public class ButtonStarBehavior : MonoBehaviour
{
    [SerializeField] public ParticleSystem buttonStars;

    private float buttonStarRadius;
    private void Awake()
    {
        buttonStarRadius = 1.5f;

        //var shape = buttonStars.shape;
        //shape.radius = buttonStarRadius;
    }

    public void OnLevelButtonEnter()
    {
        StartCoroutine(StarRadiusIncrease());
    }

    public IEnumerator StarRadiusIncrease()
    {
        buttonStarRadius = 1.5f;

        while (buttonStarRadius > 1f)
        {
            buttonStarRadius -= .1f;
            var shape = buttonStars.shape;
            shape.radius = buttonStarRadius;
        }
        yield return null;
    }
}
