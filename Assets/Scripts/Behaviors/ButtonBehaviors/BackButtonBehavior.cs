using UnityEngine;
using System.Collections;

public class BackButtonBehavior : MonoBehaviour
{
    [SerializeField] public GameObject backNebulaBG;

    public float scaleDuration = .5f;
    public Vector3 targetScale = new Vector3(210f, 210f, 210f);

    public Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale; // Gets the initial scale of the Nebula.
    }


    public void OnBackButtonEnter()
    {
        //StartCoroutine(IncreaseNebulaScale());
    }

    public void OnBackButtonExit()
    {

    }

    private IEnumerator IncreaseNebulaScale()
    {
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = targetScale;
    }
}
