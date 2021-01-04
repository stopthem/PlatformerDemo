using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public enum Character
    {
        Bob,
        Alice
    }
    public Character character;
    private PlayerAlice m_playerAlice;
    private PlayerBob m_playerBob;
    public InputDetection inputDetection;

    private Rigidbody2D m_rigidBody;
    private Transform m_playerTransform;

    private Vector2 m_moveInput;

    private int m_buttonUsed = 0;
    private int m_direction;

    private float m_horizontalMove;
    private float m_originalGravity;

    private bool m_moveLeft = false, m_moveRight = false;
    private bool m_isDashing, m_isGroundBreaking;
    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_originalGravity = m_rigidBody.gravityScale;

        m_playerTransform = GetComponent<Transform>();
        m_playerAlice = GetComponent<PlayerAlice>();
        m_playerBob = GetComponent<PlayerBob>();
    }

    private void Update()
    {
        MoveCharacter();
        GetPlayerDirection();

    }

    // handles all physics
    private void FixedUpdate()
    {
        m_rigidBody.velocity = new Vector2(m_horizontalMove, m_rigidBody.velocity.y);

        if (m_isDashing)
        {
            if (m_direction == 1)
            {
                m_rigidBody.AddForce(new Vector2(-m_playerAlice.dashLength, 0) * m_playerAlice.dashSpeed, ForceMode2D.Impulse);
            }
            else if (m_direction == 2)
            {
                m_rigidBody.AddForce(new Vector2(m_playerAlice.dashLength, 0) * m_playerAlice.dashSpeed, ForceMode2D.Impulse);
            }
        }

        if (m_isGroundBreaking)
        {
            m_rigidBody.AddForce(new Vector2(0, -m_playerBob.gbSpeed), ForceMode2D.Impulse);
            m_isGroundBreaking = false;
        }
    }

    // getting charachter direction based on Y rotation
    private void GetPlayerDirection()
    {
        if (m_playerTransform.rotation == Quaternion.Euler(0, 180, 0))
        {
            m_direction = 1;
        }
        else if (m_playerTransform.rotation == Quaternion.Euler(0, 0, 0))
        {
            m_direction = 2;
        }
    }
    // Moving buttons hold process
    public void MoveLeft()
    {
        m_moveLeft = true;
    }
    public void StopMoveLeft()
    {
        m_moveLeft = false;
    }
    public void MoveRight()
    {
        m_moveRight = true;

    }
    public void StopMoveRight()
    {
        m_moveRight = false;
    }

    private void MoveCharacter()
    {
        if (inputDetection.instance.isGamepadEnabled)
        {
            HandleGamepadMovement();
        }
        else if (inputDetection.instance.isMobile)
        {
            HandleMobileMovement();
        }
        else //test code for editor
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpButton();
            }
            m_moveInput.x = Input.GetAxisRaw("Horizontal");
            m_moveInput.Normalize();

            if (m_playerAlice != null)
            {
                if (character == Character.Alice)
                {
                    m_horizontalMove = m_playerAlice.moveSpeed * m_moveInput.x;
                }
            }
            else if (character == Character.Bob)
            {
                m_horizontalMove = m_playerBob.moveSpeed * m_moveInput.x;
            }

        }
        if (m_horizontalMove < 0)
        {
            m_playerTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (m_horizontalMove > 0)
        {
            m_playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    private void HandleGamepadMovement()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            JumpButton();
        }

        m_moveInput.x = Input.GetAxisRaw("Horizontal"); //instead of seperate buttons i used getaxisraw for the movement.
        m_moveInput.Normalize();

        if (character == Character.Alice)
        {
            m_horizontalMove = m_playerAlice.moveSpeed * m_moveInput.x;
        }
        else if (character == Character.Bob)
        {
            m_horizontalMove = m_playerBob.moveSpeed * m_moveInput.x;
        }
    }

    private void HandleMobileMovement()
    {
        if (m_moveLeft)
        {
            if (m_playerAlice != null)
            {
                if (character == Character.Alice)
                {
                    m_horizontalMove = -m_playerAlice.moveSpeed;

                }
            }
            else if (character == Character.Bob)
            {
                m_horizontalMove = -m_playerBob.moveSpeed;
            }

            m_playerTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (m_moveRight)
        {
            if (m_playerAlice != null)
            {
                if (character == Character.Alice)
                {
                    m_horizontalMove = m_playerAlice.moveSpeed;
                }
            }
            else if (character == Character.Bob)
            {
                m_horizontalMove = m_playerBob.moveSpeed;
            }

            m_playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            m_horizontalMove = 0;
        }
    }

    // Handles jump button
    public void JumpButton()
    {
        m_buttonUsed++;

        if (m_rigidBody.velocity.y == 0) // is grounded
        {
            m_buttonUsed = 0;
            if (m_playerAlice != null)
            {
                if (character == Character.Alice)
                {
                    m_rigidBody.velocity = Vector2.up * m_playerAlice.jumpSpeed;
                }
            }
            else if (character == Character.Bob)
            {
                m_rigidBody.velocity = Vector2.up * m_playerBob.jumpSpeed;
            }
        }
        else
        {
            //if not grounded
            if (m_playerAlice != null)
            {
                if (character == Character.Alice && m_buttonUsed == 1)
                {
                    m_buttonUsed++;
                    StartCoroutine(DashCoroutine());
                }
            }
            else if (character == Character.Bob && m_buttonUsed == 1)
            {
                m_buttonUsed++;
                m_isGroundBreaking = true;
            }
            else
            {
                m_isGroundBreaking = false;
            }
        }
    }
    private IEnumerator DashCoroutine()
    {
        m_isDashing = true;

        m_rigidBody.gravityScale = 0; // always dashing straight forward

        yield return new WaitForSeconds(m_playerAlice.dashDuration);

        m_isDashing = false;

        m_rigidBody.gravityScale = m_originalGravity;
    }



}
