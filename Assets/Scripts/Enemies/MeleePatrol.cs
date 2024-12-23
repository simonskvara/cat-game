using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleePatrol : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _currentPoint;
    private Transform _player;
    
    [Header("Patrolling")]
    public GameObject pointA;
    public GameObject pointB;
    
    public enum FirstPoint
    {
        PointA, PointB
    }
    public FirstPoint firstPoint;
    
    public float moveSpeed;
    public float pointOffset;
    public float waitTime;

    [Header("Attack")] 
    public float attackRange;
    public float attackRadius;
    public Transform attackPoint;
    public float attackChargeTime;
    public LayerMask playerLayer;
    
    [Header("Cooldown")]
    public float cooldownTime;
    private float _lastAttackTime = -Mathf.Infinity;
    
    private bool _isRunning;
    private bool _isWaiting;
    private bool _isDead;
    private bool _isAttacking;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.gameObject.GetComponent<PlayerHealth>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        _currentPoint = firstPoint == FirstPoint.PointA ? pointA.transform : pointB.transform;
        
        _isRunning = true;
        _isAttacking = false;
        _isDead = false;
    }

    private void Update()
    {
        _animator.SetBool("IsRunning", _isRunning);
        _animator.SetBool("IsAttacking", _isAttacking);

        if (Vector2.Distance(gameObject.transform.position, _player.transform.position) <= attackRange &&
            !_isAttacking && !_isDead && !_playerHealth.IsDead)
        {
            _rb.velocity = Vector2.zero;
            
            _animator.SetBool("IsRunning", false);
            
            Vector2 point = _player.position - transform.position;
            Flip(point.x);

            if (!_isAttacking && Time.time >= _lastAttackTime + cooldownTime)
            {
                BeginAttack();
                _lastAttackTime = Time.time;
            }
        }
        else if (!_isWaiting && _isRunning && !_isDead && !_isAttacking)
        {
            Patrol();
        }
        
    }

    private void Patrol()
    {
        _isRunning = true;
        
        Vector2 point = _currentPoint.position - transform.position;
        Flip(point.x);
        
        _rb.velocity = _currentPoint == pointB.transform ? new Vector2(moveSpeed, 0) : new Vector2(-moveSpeed, 0);

        if (Vector2.Distance(transform.position, _currentPoint.position) < pointOffset && _currentPoint == pointB.transform)
        {
            StartCoroutine(WaitAtPoint(pointA.transform));
            //_currentPoint = pointA.transform;
        }
        
        if (Vector2.Distance(transform.position, _currentPoint.position) < pointOffset && _currentPoint == pointA.transform)
        {
            StartCoroutine(WaitAtPoint(pointB.transform));
            //_currentPoint = pointB.transform;
        }
    }

    private void Flip(float direction)
    {
        if (direction < 0 && transform.localScale.x < 0 || direction > 0 && transform.localScale.x > 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    IEnumerator WaitAtPoint(Transform point)
    {
        _rb.velocity = Vector2.zero;
        _isWaiting = true;
        _isRunning = false;
        yield return new WaitForSeconds(waitTime);
        _currentPoint = point.transform;
        _isRunning = true;
        _isWaiting = false;
    }

    private void BeginAttack()
    {
        StartCoroutine(Attack());
        _isAttacking = true;
        _isRunning = false;
        _rb.velocity = Vector2.zero;
    }

    public void EndAttack()
    {
        _isAttacking = false;
        _isRunning = true;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackChargeTime);
        _animator.SetTrigger("Attack");
    }

    public void DealDamage()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        

        foreach (var player in players)   
        {
            Debug.Log("Hit PLayer", player.gameObject);
            player.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        }
    }

    public void Hit()
    {
        _rb.velocity = Vector2.zero;
        _animator.SetTrigger("Hit");
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, pointOffset);
        Gizmos.DrawWireSphere(pointB.transform.position, pointOffset);
        
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRadius);
    }
}

