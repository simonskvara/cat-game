using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerSmash playerSmash;
    [SerializeField] private PlayerHealth playerHealth;

    private Rigidbody2D _rb;

    public Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        MovementAnimation();
        CombatAnimation();
        JumpingAnimation();
        SmashAnimation();
        DeathAnimation();
    }

    void MovementAnimation()
    {
        if (!playerMovement.CanMove())
        {
            animator.SetFloat("Speed", 0);
        }
        
        animator.SetFloat("Speed", playerMovement.MoveDirection.magnitude);
    }

    void CombatAnimation()
    {
        animator.SetBool("IsAttacking", playerCombat.IsAttacking);

        if (playerCombat.IsAttacking && playerCombat.CurrentCombo != 0)
        {
            animator.ResetTrigger("Attack1");
            animator.ResetTrigger("Attack2");
            animator.ResetTrigger("Attack3");
            
            animator.SetTrigger("Attack" + playerCombat.CurrentCombo);
        }
    }

    void JumpingAnimation()
    {
        animator.SetBool("IsJumping", _rb.velocity.y > 0.1);
        animator.SetBool("IsFalling", _rb.velocity.y < -0.1);
    }

    void SmashAnimation()
    {
        animator.SetBool("SmashFalling", playerSmash.SmashFalling);
        animator.SetBool("SmashImpact", playerSmash.SmashImpact);
    }

    void DeathAnimation()
    {
        animator.SetBool("IsDead", playerHealth.IsDead());
    }
    
}
