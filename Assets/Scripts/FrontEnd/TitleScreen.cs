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
    public float alphaSpeed = 3f;
    private float threshold = 0.01f;
    private bool isFrontEnd;

    public bool isTutorial;

    private void Start()
    {
        AudioManager.idleInstance = AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_titleIdle);
    }

    private void Update()
    {
        if (isFrontEnd == true)
        {
            if (Mathf.Abs(gameTitle_TXT.alpha - transparent.a) < threshold)
            {
                gameTitle_TXT.alpha = transparent.a;
            }
            else
            {
                gameTitle_TXT.alpha = Mathf.Lerp(gameTitle_TXT.alpha, transparent.a, Time.deltaTime * alphaSpeed);
            }

            if (Mathf.Abs(button_TXT.alpha - transparent.a) < threshold)
            {
                button_TXT.alpha = transparent.a;
            }
            else
            {
                button_TXT.alpha = Mathf.Lerp(button_TXT.alpha, transparent.a, Time.deltaTime * alphaSpeed);
            }
        }
    }

    public void OnPressAnywherePressed()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.music_menu_titlescreen); 
        StartCoroutine(TitleTransition());
        HapticsManager.Instance.TriggerSimpleVibration(eSide.both, .5f, .2f);
    }

    IEnumerator TitleTransition()
    {
        isFrontEnd = true;
        yield return new WaitForSeconds(2);
        if (!isTutorial) CanvasManager.Instance.ShowCanvasFE();
        else CanvasManager.Instance.ShowCanvasFEPlaytestTutorial();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
