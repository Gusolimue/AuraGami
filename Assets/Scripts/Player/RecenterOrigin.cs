using UnityEngine;
using UnityEngine.InputSystem;

// This will handle the recentering of the player camera.
public class RecenterOrigin : MonoBehaviour
{
    [Header("Variables to Set")]
    [Tooltip("Main Camera")] [SerializeField] private Transform head;
    [Tooltip("XR Origin")] [SerializeField] private Transform origin;
    [Tooltip("Recenter Position")] [SerializeField] private Transform target; // Destination to move to

    [SerializeField] private InputActionProperty recenterButton;

    // Calls the RecenterCamera function when the assigned button is pressed
    void Update()
    {
        if (recenterButton.action.WasPressedThisFrame())
        {
            RecenterCamera();
        }
    }

    // Recenters the player camera to a target position/rotation
    public void RecenterCamera()
    {
        Vector3 offset = head.position - origin.position; // The camera's offset from the XROrigin
        offset.y = 0; // Leaves the feet position on the target instead of the head
        origin.position = target.position - offset; // Moves the camera to be on the target position

        Vector3 targetForward = target.forward;
        targetForward.y = 0; // Ignore the vertical axis
        Vector3 cameraForward = head.forward;
        cameraForward.y = 0; // Ignore the vertical axis
        float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);
        origin.RotateAround(head.position, Vector3.up, angle); // Rotates the camera horizontally to match the target rotation
    }
}
