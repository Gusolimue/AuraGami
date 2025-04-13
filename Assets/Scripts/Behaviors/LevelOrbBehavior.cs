using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOrbBehavior : MonoBehaviour
{
    public static LevelOrbBehavior Instance;
    [SerializeField] GameObject levelOrb;
    [SerializeField] GameObject zephyrBody;
    [SerializeField] GameObject orbPosition;

    public bool isSelected;
    public float orbMovementSpeed = 1f;

    private void Awake()
    {
        Instance = this;
        isSelected = false;
        levelOrb.transform.position = orbPosition.transform.position;
    }

    private void Update()
    {
        if (isSelected == true) levelOrb.transform.position = Vector3.Lerp(levelOrb.transform.position, zephyrBody.transform.position, Time.deltaTime * orbMovementSpeed);
    }

    public void OrbButtonPressed()
    {
        isSelected = true;
    }
}
