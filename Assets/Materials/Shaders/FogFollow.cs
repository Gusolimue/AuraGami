using UnityEngine;

public class FogFollow : MonoBehaviour
{
    public Camera cam;
    public bool isEnabled;
    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position;
    }
    private void Awake()
    {
        GetComponent<MeshRenderer>().enabled = isEnabled;
    }
}
