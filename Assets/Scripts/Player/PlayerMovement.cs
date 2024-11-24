using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpingPower;
    
    public Vector2 MoveDirection { get; private set; }

    private Rigidbody2D _rb;

    private bool _isFacingRight = true;

    [Header("Ground Check")] 
    public Transform groundCheck;
    public Vector2 groundCheckArea;
    public LayerMask groundCheckLayer;
    
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInput();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpingPower);
        }
        
        if (Input.GetKeyUp(KeyCode.Space) && _rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0f);
        }
        
        Flip();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        MoveDirection = new Vector2(moveX, 0);
    }

    void Move()
    {
        _rb.velocity = new Vector2(MoveDirection.x * moveSpeed, _rb.velocity.y);
    }

    void Flip()
    {
        if (_isFacingRight && MoveDirection.x < 0 || !_isFacingRight && MoveDirection.x > 0)
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

    


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckArea);
    }
}
