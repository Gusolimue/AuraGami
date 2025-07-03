using UnityEngine;

public class SpectatorCamera : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Camera>().enabled = true;
    }
}
