using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlice : PlayerController
{
    public float dashSpeed = 8f, dashDuration = .5f, dashLength = 10;
    private float m_originalGravity;

    private bool m_isDashing = false;

    private void Start()
    {
        // keeping gravity for after dash
        m_originalGravity = rigidBody.gravityScale;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // handles dash based on direction
        if (m_isDashing)
        {
            if (direction == 1)
            {
                rigidBody.AddForce(new Vector2(-dashLength, 0) * dashSpeed, ForceMode2D.Impulse);
            }
            else if (direction == 2)
            {
                rigidBody.AddForce(new Vector2(dashLength, 0) * dashSpeed, ForceMode2D.Impulse);
            }
        }
    }

    public override void JumpButton()
    {
        base.JumpButton();

        if (buttonUsed == 1)
        {
            buttonUsed++;
            StartCoroutine(DashCoroutine());
        }

    }

    private IEnumerator DashCoroutine()
    {
        m_isDashing = true;

        // always dashing straight forward
        rigidBody.gravityScale = 0;

        yield return new WaitForSeconds(dashDuration);

        m_isDashing = false;

        // restoring gravity to the original value
        rigidBody.gravityScale = m_originalGravity;
    }
}
