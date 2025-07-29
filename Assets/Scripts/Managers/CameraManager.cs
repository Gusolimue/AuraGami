using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    Camera[] myCams = new Camera[2];
    
    void Start()
    {
        myCams[0] = GameObject.Find("MainCamera").GetComponent<Camera>();

        myCams[1] = GameObject.Find("PresentationCamera").GetComponent<Camera>();

        Display.onDisplaysUpdated += OnDisplaysUpdated;

        mapCameraToDisplay();
    }

    void Update()
    {
        
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
