using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat playerCombat;

    public Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        MovementAnimation();
        CombatAnimation();
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
}
