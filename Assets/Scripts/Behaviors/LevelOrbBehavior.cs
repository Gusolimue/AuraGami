using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using FMODUnity;

public class LevelOrbBehavior : MonoBehaviour
{
    public static LevelOrbBehavior Instance;
    [SerializeField] EventReference hoverSFX; 
    FMOD.Studio.EventInstance hoverInstance;

    [Header("GameObjects")]
    [SerializeField] GameObject levelOrb;
    [SerializeField] GameObject zephyrBody;
    [SerializeField] GameObject orbPosition;

    [Header("Orb Selection Variables")]
    private bool isSelected;
    public float orbMovementSpeed = 1f;

    [Header("Orb Enter/Exit Variables")]
    [SerializeField] GameObject levelOrbScaleUp;
    [SerializeField] GameObject levelOrbScaleDown;
    //[SerializeField] Material orbMaterial;
    //[SerializeField] Color[] orbColors;
    private bool isEntered;
    private bool isExited;
    public float orbScaleSpeed = 5f;
    private float count;

    private void Awake()
    {
        Instance = this;
        isSelected = false;
        levelOrb.transform.position = orbPosition.transform.position;

        count = 50;

        //orbMaterial = new Material(orbMaterial); // clone if it's from Inspector
        //levelOrb.GetComponent<Renderer>().material = orbMaterial; // ensure assignment
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isSelected) levelOrb.transform.position = Vector3.Lerp(levelOrb.transform.position, zephyrBody.transform.position, Time.deltaTime * orbMovementSpeed);

        if (isEntered && !isSelected) 
        {
            levelOrb.transform.localScale = Vector3.Lerp(levelOrb.transform.localScale, levelOrbScaleUp.transform.localScale, Time.deltaTime * orbScaleSpeed);

            //float musicVolDecrease = Mathf.Lerp(PlayerPrefs.GetFloat("saveMusic"), 0, count * .5f);
            //AudioManager.Instance.SetVolume(eBus.Music, musicVolDecrease);
            
            //orbMaterial.color = Color.Lerp(orbMaterial.color, orbColors[1], Time.deltaTime * orbScaleSpeed);
            //levelOrb.GetComponent<Renderer>().material.color = Color.Lerp(levelOrb.GetComponent<Renderer>().material.color, orbColors[1], Time.deltaTime * orbScaleSpeed);
        }
        else
        {
            levelOrb.transform.localScale = Vector3.Lerp(levelOrb.transform.localScale, levelOrbScaleDown.transform.localScale, Time.deltaTime * orbScaleSpeed);

            //float musicVolIncrease = Mathf.Lerp(0, PlayerPrefs.GetFloat("saveMusic"), count * .5f);
            //AudioManager.Instance.SetVolume(eBus.Music, musicVolIncrease);

            //orbMaterial.color = Color.Lerp(orbMaterial.color, orbColors[0], Time.deltaTime * orbScaleSpeed);
            //levelOrb.GetComponent<Renderer>().material.color = Color.Lerp(levelOrb.GetComponent<Renderer>().material.color, orbColors[0], Time.deltaTime * orbScaleSpeed);
        }

    }

    public void OrbButtonPressed()
    {
        isSelected = true;
        isEntered = false;

        hoverInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_levelOrbPressed);
    }

    public void OnOrbButtonEntered()
    {
        isEntered = true;

        count = 0;
        AudioManager.Instance.SetVolume(eBus.Music, 0);
        hoverInstance = AudioManager.Instance.PlaySFX(hoverSFX);
    }

    public void OnOrbButtonExit()
    {
        isEntered = false;

        AudioManager.Instance.SetVolume(eBus.Music, PlayerPrefs.GetFloat("saveMusic"));
        hoverInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
