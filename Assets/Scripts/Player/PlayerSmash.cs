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
    public bool SmashFalling;


    private void Update()
    {
        if (SmashFalling && playerMovement.IsGrounded())
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
        playerMovement.StopMovement();
        SmashFalling = true;
    }

    private void SmashFall()
    {
        rb.velocity = new Vector2(rb.velocity.x, -1 * fallingSpeed);
    }

    private void Smash()
    {
        SmashFalling = false;
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(smashPoint.transform.position, smashArea, 0);

        foreach (var enemy in colliders)
        {
            Debug.Log("Enemy Smashed");
        }
    }
    
    
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(smashPoint.position, smashArea);
    }
}
