using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using EditorAttributes;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [Header("Pause Menus")]
    [SerializeField] public GameObject pauseOptions;
    [SerializeField] public GameObject pauseOptionsTutorial;
    [SerializeField] public GameObject settingsOptions;
    [SerializeField] Image bgFadeOut;
    [SerializeField] Color fadeColor;

    [SerializeField] GameObject bgBlur;

    public bool isRestarting;
    private float count;

    private void Awake()
    {
        Instance = this;
        isRestarting = false;
        if (LoadManager.Instance.isTutorial == false)
        {
            Instantiate(pauseOptions, transform);
            bgBlur.SetActive(false);
        }
        else
        {
            Instantiate(pauseOptionsTutorial, transform);
            bgBlur.SetActive(true);
        }
    }

    private void Update()
    {
        count += Time.deltaTime;

        Color curColor = bgFadeOut.color;
        float newAlpha = Mathf.Lerp(curColor.a, fadeColor.a, count / 15);
        bgFadeOut.color = new Color(curColor.r, curColor.g, curColor.b, newAlpha);
    }

    public void InstantiateSettingsMenu()
    {
        Instantiate(settingsOptions, transform);
    }

    public void InstantiatePauseOptions()
    {
        Instantiate(pauseOptions, transform);
    }

    public void InstantiateTutorialPauseOptions()
    {
        Instantiate(pauseOptionsTutorial, transform);
    }

    public void DestroyMenu()
    {
        Destroy(this.gameObject);
    }
}
