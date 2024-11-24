using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    public bool IsAttacking { get; private set; }

    public float damage;
    
    [Header("Attack Point")]
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;
    
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
    }


    private void OnAttack()
    {
        IsAttacking = true;
    }

    public void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (var enemy in enemies)   
        {
            Debug.Log("Hit enemy");
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
    
    /// <remarks>Call through animation events</remarks>
    public void EndAttack()
    {
        IsAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
