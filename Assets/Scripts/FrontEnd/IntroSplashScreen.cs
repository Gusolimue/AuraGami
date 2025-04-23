using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.PlayerLoop;
using UnityEngine.InputSystem;

public class IntroSplashScreen : MonoBehaviour
{
    [Header("Controller Interactor")]
    public InputActionReference skipIntroControllerAction;

    [Header("Logos/Assets")]
    [SerializeField] Image teamAuragamiLogoSprite;
    [SerializeField] Image fmodLogoSprite;
    [SerializeField] Image unityLogoSprite;
    [Space]
    [SerializeField] GameObject introVideoContainer;
    [SerializeField] Image skipIcon;

    public Color transparent;
    public Color noneTransparent;

    private bool isTeamAuragami;
    private bool isFmod;
    private bool isUnity;

    public float transitionTime = 5f;

    private void Awake()
    {
        StartCoroutine(SplashSequence());

        skipIntroControllerAction.action.Enable();
        skipIntroControllerAction.action.performed += OnPauseButtonPressed;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDestroy()
    {
        skipIntroControllerAction.action.Disable();
        skipIntroControllerAction.action.performed -= OnPauseButtonPressed;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void Update()
    {
       if (isTeamAuragami == true)
       {
            teamAuragamiLogoSprite.color = Color.Lerp(teamAuragamiLogoSprite.color, noneTransparent, Time.deltaTime * 5f);
       }
       if (isTeamAuragami == false)
       {
            teamAuragamiLogoSprite.color = Color.Lerp(teamAuragamiLogoSprite.color, transparent, Time.deltaTime * 5f);
       }

        if (isFmod == true)
        {
            fmodLogoSprite.color = Color.Lerp(fmodLogoSprite.color, noneTransparent, Time.deltaTime * 5f);
        }
        if (isFmod == false)
        {
            fmodLogoSprite.color = Color.Lerp(fmodLogoSprite.color, transparent, Time.deltaTime * 5f);
        }

        if (isUnity == true)
        {
            unityLogoSprite.color = Color.Lerp(unityLogoSprite.color, noneTransparent, Time.deltaTime * 5f);
        }
        if (isTeamAuragami == false)
        {
            unityLogoSprite.color = Color.Lerp(unityLogoSprite.color, transparent, Time.deltaTime * 5f);
        }
    }

    public void OnPauseButtonPressed(InputAction.CallbackContext context)
    {
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
    }

    IEnumerator SplashSequence()
    {
        yield return new WaitForSeconds(26);
        introVideoContainer.SetActive(false);
        isTeamAuragami = true;
        yield return new WaitForSeconds(2);
        isTeamAuragami = false;
        yield return new WaitForSeconds(1);

        isFmod = true;
        yield return new WaitForSeconds(2);
        isFmod = false;
        yield return new WaitForSeconds(1);

        isUnity = true;
        yield return new WaitForSeconds(2);
        isUnity = false;
        yield return new WaitForSeconds(1);

        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                skipIntroControllerAction.action.Disable();
                skipIntroControllerAction.action.performed -= OnPauseButtonPressed;
                break;
            case InputDeviceChange.Reconnected:
                skipIntroControllerAction.action.Enable();
                skipIntroControllerAction.action.performed += OnPauseButtonPressed;
                break;
        }
    }
}
