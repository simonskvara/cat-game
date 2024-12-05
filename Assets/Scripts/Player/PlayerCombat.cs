using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    public bool IsAttacking { get; private set; }

    public float attackDamage;
    
    [Header("Attack Point")]
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    [Header("Combo")] 
    public int maxCombo = 3;

    public int CurrentCombo;
    private float comboTimer;
    public float comboResetTime = 1f;

    [Header("Ground Smash")]
    public float groundSmashDamage;
    
    
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _playerMovement.IsGrounded() && !IsAttacking)
        {
            OnAttack();
        }
        
        if (CurrentCombo > 0)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer > comboResetTime)
            {
                ResetCombo();
            }
        }
        
    }


    private void OnAttack()
    {
        IsAttacking = true;
        
        comboTimer = 0f; // Reset the combo timer

        CurrentCombo++;
        if (CurrentCombo > maxCombo)
        {
            CurrentCombo = 1; // Loop back to the first attack
        }
    }
    
    /// <remarks>Called through animation events</remarks>
    public void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (var enemy in enemies)   
        {
            Debug.Log("Hit enemy");
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }
    
    /// <remarks>Call through animation events</remarks>
    public void EndAttack()
    {
        IsAttacking = false;
    }

    private void ResetCombo()
    {
        CurrentCombo = 0;
    }
    
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
