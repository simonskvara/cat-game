using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    public Animator animator;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        MovementAnimation();
    }

    void MovementAnimation()
    {
        animator.SetFloat("Speed", _playerMovement.MoveDirection.magnitude);
    }
}
