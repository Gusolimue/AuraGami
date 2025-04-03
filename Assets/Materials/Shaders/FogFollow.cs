using UnityEngine;

public class FogFollow : MonoBehaviour
{
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position;
    }
}
