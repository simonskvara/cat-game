using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSmash : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Rigidbody2D rb;
    
    [Header("Ground Smash")]
    public float groundSmashDamage;
    public Transform smashPoint;
    public Vector2 smashArea;
    
    public float fallingSpeed;
    public bool SmashFalling { get; private set; }
    public bool SmashImpact { get; private set; }

    [Header("When to smash")]
    [HideInInspector] public Transform groundCheck;
    [HideInInspector] public Vector2 groundCheckArea;
    public LayerMask smashLayer;


    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Player.Enable();
        _playerControls.Player.Attack.performed += OnSmashStarted;
    }

    private void OnDisable()
    {
        _playerControls.Player.Disable();
        _playerControls.Player.Attack.performed -= OnSmashStarted;
    }

    private void Start()
    {
        groundCheck = playerMovement.groundCheck;
        groundCheckArea = playerMovement.groundCheckArea;
    }

    private void Update()
    {
        if (SmashFalling && ShouldSmash())
        {
            Smash();
        }
    }

    private void FixedUpdate()
    {
        if (SmashFalling)
        {
            SmashFall();
        }
    }

    private void OnSmashStarted(InputAction.CallbackContext context)
    {
        if (playerMovement.MoveDirection.y < -0.1f && Math.Abs(playerMovement.MoveDirection.x) < 0.5f &&
            !playerMovement.IsGrounded() && !SmashFalling)
        {
            SmashFalling = true;
            playerMovement.StopMovement();
            gameObject.layer = LayerMask.NameToLayer("PlatformCollision");
        }
    }

    private void SmashFall()
    {
        rb.velocity = new Vector2(rb.velocity.x, -1 * fallingSpeed);
    }

    private void Smash()
    {
        SmashFalling = false;
        SmashImpact = true;
        
        gameObject.layer = LayerMask.NameToLayer("Player");
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(smashPoint.transform.position, smashArea, 0);

        foreach (var hit in colliders)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy)
            {
                enemy.TakeDamage(groundSmashDamage);
            }
        }
    }

    public void ImpactEnd()
    {
        SmashImpact = false;
    }

    public void InterruptSmashFall()
    {
        SmashFalling = false;
    }

    bool ShouldSmash()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckArea, 0, smashLayer);
    }
    
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(smashPoint.position, smashArea);
    }
}
