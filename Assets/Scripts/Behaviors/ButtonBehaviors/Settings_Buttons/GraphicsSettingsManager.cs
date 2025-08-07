using UnityEngine;

public class GraphicsSettingsManager : MonoBehaviour
{
    private void Awake()
    {
        CanvasManager.Instance.playerCircle.gameObject.SetActive(false);
    }
}
