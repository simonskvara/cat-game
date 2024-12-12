using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    public bool IsAttacking { get; private set; }

    public float attackDamage;
    public float comboFinishDamage;
    private float _originalDamage;
    
    [Header("Attack Point")]
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    [Header("Combo")] 
    public int maxCombo = 3;

    public int CurrentCombo { get; private set; }
    private float _comboTimer;
    public float comboResetTime = 1f;

    
    
    
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _originalDamage = attackDamage;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _playerMovement.IsGrounded() && !IsAttacking)
        {
            OnAttack();
        }
        
        attackDamage = CurrentCombo == 3 ? comboFinishDamage : _originalDamage;
        
        if (CurrentCombo > 0)
        {
            _comboTimer += Time.deltaTime;
            if (_comboTimer > comboResetTime)
            {
                ResetCombo();
            }
        }
    }
    
    private void OnAttack()
    {
        IsAttacking = true;
        
        _comboTimer = 0f;

        CurrentCombo++;
        if (CurrentCombo > maxCombo)
        {
            CurrentCombo = 1;
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
