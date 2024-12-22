using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    
    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _currentPoint;
    
    public float moveSpeed;
    public float pointOffset;
    public float waitTime;

    private bool _isRunning;
    private bool _isWaiting;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentPoint = pointB.transform;
        _isRunning = true;
    }

    private void Update()
    {
        _animator.SetBool("IsRunning", _isRunning);

        if (!_isWaiting)
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, pointOffset);
        Gizmos.DrawWireSphere(pointB.transform.position, pointOffset);
        
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
