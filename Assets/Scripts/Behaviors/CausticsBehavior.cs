using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CausticsBehavior : MonoBehaviour
{
    public static CausticsBehavior Instance;
    [SerializeField]
    private float causticsIncrementSpeed = .1f;
    [SerializeField]
    private Material causticsMaterial;

    private float causticsIndex = 0;
    private float causticsIndexMax = 15;

    private void Awake()
    {
        Instance = this;
    }

    IEnumerator IncrementCausticsIndex()
    {
        while (PauseManager.Instance.isPaused == false)
        {
            causticsIndex++;
            if (causticsIndex > causticsIndexMax)
            {
                Debug.Log("Reset caustics index");
                causticsIndex = 0;
            }

            Debug.Log(causticsIndex);

            causticsMaterial.SetFloat("_CausticsArrayIndex", causticsIndex);

            yield return new WaitForSeconds(causticsIncrementSpeed);
        }
    }

    void Start()
    {
        StartCoroutine(IncrementCausticsIndex());
    }
}
