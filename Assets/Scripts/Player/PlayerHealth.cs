using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMovement playerMovement;
    
    public Image[] hearts;

    [SerializeField] private int _currentHealth;

    public GameObject deathScreen;
    
    private bool _isDead;

    public event Action OnHit;
    public event Action OnDeath;
    
    private void Start()
    {
        _currentHealth = 3;
        deathScreen.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < _currentHealth)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage()
    {
        OnHit?.Invoke();
        _currentHealth--;
    }

    private void Die()
    {
        OnDeath?.Invoke();
        playerMovement.StopMovement();
        _isDead = true;
        StartCoroutine(DeathScreen());
    }

    public bool IsDead()
    {
        return _isDead;
    }

    IEnumerator DeathScreen()
    {
        yield return new WaitForSecondsRealtime(1);
        deathScreen.SetActive(true);
    }
}
