using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOrbBehavior : MonoBehaviour
{
    public static LevelOrbBehavior Instance;

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

    private void Awake()
    {
        Instance = this;
        isSelected = false;
        levelOrb.transform.position = orbPosition.transform.position;

        //orbMaterial = new Material(orbMaterial); // clone if it's from Inspector
        //levelOrb.GetComponent<Renderer>().material = orbMaterial; // ensure assignment
    }

    private void Update()
    {
        if (isSelected == true) levelOrb.transform.position = Vector3.Lerp(levelOrb.transform.position, zephyrBody.transform.position, Time.deltaTime * orbMovementSpeed);

        if (isEntered == true && !isSelected) 
        {
            levelOrb.transform.localScale = Vector3.Lerp(levelOrb.transform.localScale, levelOrbScaleUp.transform.localScale, Time.deltaTime * orbScaleSpeed);
            //orbMaterial.color = Color.Lerp(orbMaterial.color, orbColors[1], Time.deltaTime * orbScaleSpeed);
            //levelOrb.GetComponent<Renderer>().material.color = Color.Lerp(levelOrb.GetComponent<Renderer>().material.color, orbColors[1], Time.deltaTime * orbScaleSpeed);
        }

        if (isExited == true) 
        {
            levelOrb.transform.localScale = Vector3.Lerp(levelOrb.transform.localScale, levelOrbScaleDown.transform.localScale, Time.deltaTime * orbScaleSpeed);
            //orbMaterial.color = Color.Lerp(orbMaterial.color, orbColors[0], Time.deltaTime * orbScaleSpeed);
            //levelOrb.GetComponent<Renderer>().material.color = Color.Lerp(levelOrb.GetComponent<Renderer>().material.color, orbColors[0], Time.deltaTime * orbScaleSpeed);
        }
    }

    public void OrbButtonPressed()
    {
        isSelected = true;
        isEntered = false;
        isExited = true;

        //AudioManager.Instance.PlaySFX(AudioManager.Instance.sfx_frontEnd_levelOrbPressed);
    }

    public void OnOrbButtonEntered()
    {
        isEntered = true;
        isExited = false;
    }

    public void OnOrbButtonExit()
    {
        isEntered = false;
        isExited = true;
    }
}
