using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.PlayerLoop;
using UnityEngine.InputSystem;
using TMPro;

public class IntroSplashScreen : MonoBehaviour
{
    public static IntroSplashScreen Instance;
    [Header("Controller Interactor")]
    public InputActionReference skipIntroControllerAction;

    [Header("Logos/Assets")]
    [SerializeField] Image[] logoSprites;
    [Space]
    [SerializeField] GameObject settings;
    //[SerializeField] GameObject introVideoContainer;

    [Header("Skip Icon")]
    [SerializeField] Image skipIcon;
    [SerializeField] TextMeshProUGUI skip_TXT;
    [SerializeField] Slider skipSlider;
    public float holdTime = 2f;
    private float holdTimer = 0f;
    private bool isHolding = false;
    [Space]

    public Color transparent;
    public Color noneTransparent;

    private bool isTeamAuragami;
    private bool isACC;
    private bool isFmod;
    private bool isUnity;

    private bool isSkip;

    public float transitionTime = 5f;
    public int isTutorial;

    private void Awake()
    {
        Instance = this;
        //isTutorial = PlayerPrefs.GetInt("frontEnd");

        if (isTutorial <= 1) 
        {
            settings.SetActive(true);
            LoadManager.Instance.isTutorial = true;
        }
        else if (isTutorial >= 1) StartCoroutine(SplashSequence());
    }

    private void OnEnable()
    {
        if (skipIntroControllerAction != null)
        {
            skipIntroControllerAction.action.started += OnHoldStarted;
            skipIntroControllerAction.action.canceled += OnHoldCanceled;
            skipIntroControllerAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (skipIntroControllerAction != null)
        {
            skipIntroControllerAction.action.started -= OnHoldStarted;
            skipIntroControllerAction.action.canceled -= OnHoldCanceled;
            skipIntroControllerAction.action.Disable();
        }
    }

    private void Update()
    {
        if (isTeamAuragami == true)
        {
            logoSprites[0].color = Color.Lerp(logoSprites[0].color, noneTransparent, Time.deltaTime * transitionTime);
        }
        if (isTeamAuragami == false)
        {
            logoSprites[0].color = Color.Lerp(logoSprites[0].color, transparent, Time.deltaTime * transitionTime);
        }

        if (isACC == true)
        {
            logoSprites[1].color = Color.Lerp(logoSprites[1].color, noneTransparent, Time.deltaTime * transitionTime);
        }
        if (isACC == false)
        {
            logoSprites[1].color = Color.Lerp(logoSprites[1].color, transparent, Time.deltaTime * transitionTime);
        }

        if (isFmod == true)
        {
            logoSprites[2].color = Color.Lerp(logoSprites[2].color, noneTransparent, Time.deltaTime * transitionTime);
        }
        if (isFmod == false)
        {
            logoSprites[2].color = Color.Lerp(logoSprites[2].color, transparent, Time.deltaTime * transitionTime);
        }

        if (isUnity == true)
        {
            logoSprites[3].color = Color.Lerp(logoSprites[3].color, noneTransparent, Time.deltaTime * transitionTime);
        }
        if (isUnity == false)
        {
            logoSprites[3].color = Color.Lerp(logoSprites[3].color, transparent, Time.deltaTime * transitionTime);
        }

        if (isSkip == true)
        {
            skipIcon.color = Color.Lerp(skipIcon.color, noneTransparent, Time.deltaTime * 10f);
            skip_TXT.color = Color.Lerp(skip_TXT.color, noneTransparent, Time.deltaTime * 10f);
        }
        if (isSkip == false)
        {
            skipIcon.color = Color.Lerp(skipIcon.color, transparent, Time.deltaTime * 10f);
            skip_TXT.color = Color.Lerp(skip_TXT.color, transparent, Time.deltaTime * 10f);
        }

        //if (!isSkip) return;

        if (isHolding)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdTime)
            {
                SkipIntro();
            }
        }
        else
        {
            holdTimer -= Time.deltaTime;
        }
        holdTimer = Mathf.Clamp(holdTimer, 0f, holdTime);
        skipSlider.value = holdTimer / holdTime;

    }

    private void OnHoldStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Hold Started");
        isSkip = true;
        isHolding = true;
    }

    private void OnHoldCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Hold Released");
        isSkip = false;
        isHolding = false;
    }

    private void SkipIntro()
    {
        isHolding = false;
        StopAllCoroutines();

        isTutorial++;
        PlayerPrefs.SetInt("frontEnd", isTutorial);
        PlayerPrefs.Save();

        if (isTutorial <= 1) FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(1, 1);
        else FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(2, 1);
    }
  
    public void StartIntro()
    {
        StartCoroutine(SplashSequence());
        settings.SetActive(false);
    }

    IEnumerator SplashSequence()
    {
        /*yield return new WaitForSeconds(26);
        introVideoContainer.SetActive(false);
        yield return new WaitForSeconds(1);*/
        yield return new WaitForSeconds(1);
        isTeamAuragami = true;
        yield return new WaitForSeconds(3);
        isTeamAuragami = false;
        yield return new WaitForSeconds(1);

        isACC = true;
        yield return new WaitForSeconds(3);
        isACC = false;
        yield return new WaitForSeconds(1);

        isFmod = true;
        yield return new WaitForSeconds(3);
        isFmod = false;
        yield return new WaitForSeconds(1);

        isUnity = true;
        yield return new WaitForSeconds(3);
        isUnity = false;
        yield return new WaitForSeconds(1);

        isTutorial++;
        PlayerPrefs.SetInt("frontEnd", isTutorial);
        PlayerPrefs.Save();

        if (isTutorial <= 1) FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(1, 1);
        else if (isTutorial >= 1) FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash(2, 1);
    }
}
