using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
//this is currently a dummy behavior used for collision detection and instantiating the avatar model

public class AvatarBehavior : MonoBehaviour
{
    //[Header("Variables to Adjust")]
    [Header("Variables to Set")]
    public eSide side;
    public GameObject avatarObject;
    public UnityEngine.XR.InputDevice controller;

    void TryInitialize()
    {
        // Get all currently connected controllers
        var controllers = new List<UnityEngine.XR.InputDevice>();

        // Create desired characteristics to filter out left and right controllers
        var desiredCharacteristicsLeft = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Controller | UnityEngine.XR.InputDeviceCharacteristics.Left;
        var desiredCharacteristicsRight = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Controller | UnityEngine.XR.InputDeviceCharacteristics.Right;

        // Initialize left and right controllers to contain every connected controller
        var leftControllers = controllers;
        var rightControllers = controllers;

        // Sets the left and right controllers list to contain left and right controllers filtered out by the desired characteristics
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristicsLeft, leftControllers);
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristicsRight, rightControllers);

        if (leftControllers.Count > 0)
        {
            foreach (var device in leftControllers)
            {
                Debug.Log(device.name);
                if (side == eSide.left)
                {
                    Debug.Log("Set left side controller");
                    controller = device;
                }
            }
        }
        else if (rightControllers.Count > 0)
        {
            foreach (var device in rightControllers)
            {
                Debug.Log(device.name);
                if (side == eSide.right)
                {
                    Debug.Log("Set right side controller");
                    controller = device;
                }
            }
        }
        else
        {
            return;
        }
    }

    private void Start()
    {
        TryInitialize();
    }

    void Update()
    {
        if (!controller.isValid)
        {
            TryInitialize();
        }
    }

    public void ObstacleCollision()
    {
        if(avatarObject.GetComponent<Animator>() != null)
        {
            avatarObject.GetComponent<Animator>().SetTrigger("OnDamage");
        }
        else
        {
            Debug.LogError("No animator detected for "+side+ " avatar!");
        }
    }

    public void StreakEnabled()
    {
        if (avatarObject.GetComponent<Animator>() != null)
        {
            avatarObject.GetComponent<Animator>().SetTrigger("OnStreak");
        }
        else
        {
            Debug.LogError("No animator detected for " + side + " avatar!");
        }
    }

    public void TargetCollision()
    {

    }
}
