﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetection : MonoBehaviour
{
    [HideInInspector]public InputDetection instance;
    public bool isGamepadEnabled = false, isMobile = true;

    public bool isJoystickControlsForMobileEnabled = true;
    public bool isMovementKeysEnabled;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        //searching for gamepad
        if (Input.GetJoystickNames().Length > 0)
        {
            isGamepadEnabled = true;
        }
        else
        {
            isGamepadEnabled = false;
        }
        //searching for a mobile platform
        if (Application.platform == RuntimePlatform.Android && Application.platform == RuntimePlatform.IPhonePlayer)
        {
            isMobile = true;
        }
        // else
        // {
        //     isMobile = false;
        // }
        
    }
}
