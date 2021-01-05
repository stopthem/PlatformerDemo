using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBob : PlayerController
{
    public float gbSpeed = 30f;

    private bool m_isGroundBreaking = false;

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (m_isGroundBreaking)
        {
            rigidBody.AddForce(new Vector2(0, -gbSpeed), ForceMode2D.Impulse);
            m_isGroundBreaking = false;
        }
    }

    public override void JumpButton()
    {
        base.JumpButton();

        if (buttonUsed == 1)
        {
            buttonUsed++;
            m_isGroundBreaking = true;
        }
        else
        {
            m_isGroundBreaking = false;
        }

    }
}
