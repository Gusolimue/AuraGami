using UnityEngine;

public class SettingsSaveManager : MonoBehaviour
{
    [Header("Accessibility Options")]
    [SerializeField] GameObject playCircle;
    public void Start()
    {
        float scaleX = PlayerPrefs.GetFloat("playCircleScaleX", 1f);
        float scaleY = PlayerPrefs.GetFloat("playCircleScaleY", 1f);
        float scaleZ = PlayerPrefs.GetFloat("playCircleScaleZ", 1f);

        if (playCircle != null)
        {
            playCircle.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            Debug.Log("Loaded playCircle scale: " + playCircle.transform.localScale);
        }
    }
}
