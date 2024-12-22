using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePatrol : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _currentPoint;
    
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
    public float attackRadius;
    public Transform attackPoint;
    
    private bool _isRunning;
    private bool _isWaiting;
    private bool _isDead;
    private bool _isAttacking;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        _currentPoint = firstPoint == FirstPoint.PointA ? pointA.transform : pointB.transform;
        
        _isRunning = true;
        _isDead = false;
    }

    private void Update()
    {
        _animator.SetBool("IsRunning", _isRunning);

        if (!_isWaiting && _isRunning && !_isDead)
        {
            Patrol();
        }
        
    }

    private void Patrol()
    {
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

    public void Dead()
    {
        _rb.velocity = Vector2.zero;
        _isRunning = false;
        _isDead = true;
        _animator.SetBool("Dead", true);
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
    }
}
