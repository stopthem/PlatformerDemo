using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private PlayerAlice m_playerAlice;
    private PlayerBob m_playerBob;
    public InputDetection inputDetection;

    [HideInInspector] public int direction;

    [HideInInspector] public Rigidbody2D rigidBody;
    private Transform m_playerTransform;

    private Vector2 m_moveInput;

    [HideInInspector] public int buttonUsed = 0;

    private float m_horizontalMove;
    public float moveSpeed;
    public float jumpSpeed;

    private bool m_moveLeft = false, m_moveRight = false;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        m_playerTransform = GetComponent<Transform>();
        m_playerAlice = GetComponent<PlayerAlice>();
        m_playerBob = GetComponent<PlayerBob>();
    }

    public virtual void Update()
    {
        MoveCharacter();
        GetPlayerDirection();
        Debug.Log(inputDetection.instance.isMobile);
    }

    // handles physics
    public virtual void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(m_horizontalMove, rigidBody.velocity.y);
    }

    // gets player direction for Alice's dash and further use.
    private void GetPlayerDirection()
    {
        if (transform.rotation == Quaternion.Euler(0, 180, 0))
        {
            direction = 1;
        }
        else if (transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            direction = 2;
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

    // handles all moving based on platform and input device
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

            m_horizontalMove = moveSpeed * m_moveInput.x;

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

        //instead of seperate buttons i used getaxisraw for the movement.
        m_moveInput.x = Input.GetAxisRaw("Horizontal");
        m_moveInput.Normalize();

        m_horizontalMove = moveSpeed * m_moveInput.x;
    }

    private void HandleMobileMovement()
    {
        if (m_moveLeft)
        {
            m_horizontalMove = -moveSpeed;

            m_playerTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (m_moveRight)
        {
            m_horizontalMove = moveSpeed;

            m_playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            m_horizontalMove = 0;
        }
    }

    // Handles jump button on click
    public virtual void JumpButton()
    {
        buttonUsed++;

        if (rigidBody.velocity.y == 0)
        {
            buttonUsed = 0;
            rigidBody.velocity = Vector2.up * jumpSpeed;
        }
    }

}
