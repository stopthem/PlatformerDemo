using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InputDetection inputDetection;
    public GameObject mobileButtons;

    public GameObject joystickButtons;
    void Update()
    {
        if (inputDetection.instance.isMobile && inputDetection.instance.isMovementKeysEnabled)
        {
            ShowMovementKeys();
        }
        else
        {
            HideMovementKeys();
        }

        if (inputDetection.instance.isMobile && inputDetection.instance.isJoystickControlsForMobileEnabled)
        {
            ShowJoystickForMobile();
        }
        else
        {
            HideJoystickForMobile();
        }
        
        if (inputDetection.instance.isGamepadEnabled)
        {
            HideMovementKeys();
            HideJoystickForMobile();
        }
        //test code
        // else if(Application.platform == RuntimePlatform.WindowsEditor)
        // {
        //     ShowButtons();
        // }
    }

    private void HideMovementKeys()
    {
        mobileButtons.gameObject.SetActive(false);
    }
    private void ShowMovementKeys()
    {
        mobileButtons.gameObject.SetActive(true);
    }
    private void ShowJoystickForMobile()
    {
        joystickButtons.gameObject.SetActive(true);
    }
    private void HideJoystickForMobile()
    {
        joystickButtons.gameObject.SetActive(false);
    }
}
