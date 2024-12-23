using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        groundCheck = playerMovement.groundCheck;
        groundCheckArea = playerMovement.groundCheckArea;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Mouse0) && !playerMovement.IsGrounded())
        {
            OnSmashStarted();
        }
        
        
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

    private void OnSmashStarted()
    {
        SmashFalling = true;
        playerMovement.StopMovement();
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
        gameObject.layer = LayerMask.NameToLayer("PlatformCollision");
    }

    private void SmashFall()
    {
        rb.velocity = new Vector2(rb.velocity.x, -1 * fallingSpeed);
    }

    private void Smash()
    {
        SmashFalling = false;
        SmashImpact = true;
        
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
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
