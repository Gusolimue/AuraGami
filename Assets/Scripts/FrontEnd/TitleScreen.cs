using UnityEngine;
using System.Collections;
using TMPro;
using Unity.Collections;

public class TitleScreen : MonoBehaviour
{
    [Header("Title Variables")]
    [SerializeField] TextMeshProUGUI gameTitle_TXT;
    [SerializeField] TextMeshProUGUI button_TXT;
    public Color transparent;
    public float alphaSpeed = 5f;
    private bool isFrontEnd;

    private void Update()
    {
        if (isFrontEnd == true)
        {
            gameTitle_TXT.alpha = Mathf.Lerp(gameTitle_TXT.alpha, transparent.a, Time.deltaTime * alphaSpeed);
            button_TXT.alpha = Mathf.Lerp(button_TXT.alpha, transparent.a, Time.deltaTime * alphaSpeed);
        }
    }

    public void OnPressAnywherePressed()
    {
        StartCoroutine(TitleTransition());
    }

    IEnumerator TitleTransition()
    {
        isFrontEnd = true;
        yield return new WaitForSeconds(2);
        CanvasManager.Instance.ShowCanvasFE();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
