using UnityEngine;
using System.Collections;
using UnityEngine.XR;

public class CameraManager : MonoBehaviour
{
    Camera[] myCams = new Camera[2];
    public float smoothing;

    private void Awake()
    {

        XRSettings.gameViewRenderMode = GameViewRenderMode.None;
    }
    void Start()
    {
        myCams[1] = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        myCams[0] = GameObject.FindGameObjectWithTag("PresentationCamera").GetComponent<Camera>();

        Display.onDisplaysUpdated += OnDisplaysUpdated;

        mapCameraToDisplay();
    }
    void Update()
    {
        myCams[0].transform.rotation = Quaternion.Slerp(myCams[0].transform.rotation, myCams[1].transform.rotation, Time.deltaTime * smoothing);
    }

    void mapCameraToDisplay()
    {
        for (int i = 0; i < Display.displays.Length; i++)
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
