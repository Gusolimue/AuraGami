using UnityEngine;

public class FogFollow : MonoBehaviour
{
    Camera cam;
    public bool isEnabled;
    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position;
    }
    private void Awake()
    {
        cam = Camera.main;
        GetComponent<MeshRenderer>().enabled = isEnabled;
    }
}
