using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpingPower;
    
    public Vector2 MoveDirection { get; private set; }

    private Rigidbody2D _rb;

    private bool _isFacingRight = true;

    private bool _canMove = true;

    [Header("Ground Check")] 
    public Transform groundCheck;
    public Vector2 groundCheckArea;
    public LayerMask groundCheckLayer;
    
    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Player.Enable();
        _playerControls.Player.Move.performed += ProcessInput;
        _playerControls.Player.Move.canceled += ProcessInput;

        _playerControls.Player.Jump.performed += OnJump;
        _playerControls.Player.Jump.canceled += OnJump;
    }

    private void OnDisable()
    {
        _playerControls.Player.Move.performed -= ProcessInput;
        _playerControls.Player.Move.canceled -= ProcessInput;
        
        _playerControls.Player.Jump.performed -= OnJump;
        _playerControls.Player.Jump.canceled -= OnJump;
        
        _playerControls.Player.Disable();
    }

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_canMove)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!_canMove || PauseMenu.gameIsPaused)
        {
            return;
        }
        Move();
    }

    void ProcessInput(InputAction.CallbackContext context)
    {
        MoveDirection = _playerControls.Player.Move.ReadValue<Vector2>();
    }

    void Move()
    {
        _rb.velocity = new Vector2(MoveDirection.x * moveSpeed, _rb.velocity.y);
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpingPower);
        }

        if (context.canceled && _rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0f);
        }
    }
    
    void Flip()
    {
        if (_isFacingRight && MoveDirection.x < 0 || !_isFacingRight && MoveDirection.x > 0 && _canMove)
        {
            _isFacingRight = !_isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckArea, 0, groundCheckLayer);
    }

    public void StopMovement()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        _canMove = false;
    }

    public void ResumeMovement()
    {
        _canMove = true;
    }

    public bool CanMove()
    {
        return _canMove;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckArea);
    }
}
