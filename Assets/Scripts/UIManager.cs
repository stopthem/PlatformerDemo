using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InputDetection inputDetection;
    public GameObject mobileButtons;
    void Update()
    {
        if (inputDetection.instance.isGamepadEnabled)
        {
            HideButtons();
        }
        else if (inputDetection.instance.isMobile)
        {
            ShowButtons();
        }
        //test code
        // else if(Application.platform == RuntimePlatform.WindowsEditor)
        // {
        //     ShowButtons();
        // }
    }

    private void HideButtons()
    {
        mobileButtons.gameObject.SetActive(false);
    }
    private void ShowButtons()
    {
        mobileButtons.gameObject.SetActive(true);
    }
}
