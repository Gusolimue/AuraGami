using UnityEngine;
using System.Collections;
using UnityEngine.XR;

public class CameraManager : MonoBehaviour
{
    Camera[] myCams = new Camera[2];
    public float smoothing;
    public bool other;
    private void Awake()
    {

        XRSettings.gameViewRenderMode = GameViewRenderMode.None;
    }
    void Start()
    {
        myCams[0] = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        myCams[1] = GameObject.FindGameObjectWithTag("PresentationCamera").GetComponent<Camera>();
        if (other)
        {

            Display.onDisplaysUpdated += OnDisplaysUpdated;

            mapCameraToDisplay();
        }
    }
    void Update()
    {
        myCams[1].transform.rotation = Quaternion.Slerp(myCams[1].transform.rotation, myCams[0].transform.rotation, Time.deltaTime * smoothing);
    }

    void mapCameraToDisplay()
    {
        for (int i = 0; i < myCams.Length; i++)
        {
            myCams[i].targetDisplay = i;
            Display.displays[i].Activate();
        }
    }

    void OnDisplaysUpdated()
    {
        Debug.Log("New Display Connected. Show Display Option Menu ....");
    }
}
