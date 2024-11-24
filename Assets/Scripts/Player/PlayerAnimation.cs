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
        animator.SetFloat("Speed", playerMovement.MoveDirection.magnitude);
    }

    void CombatAnimation()
    {
        animator.SetBool("IsAttacking", playerCombat.IsAttacking);
    }
}
