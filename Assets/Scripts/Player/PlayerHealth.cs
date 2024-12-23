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
    
    public bool IsDead { get; private set; }

    public GameObject deathScreen;

    public event Action OnHit;
    public event Action OnDeath;
    
    private void Start()
    {
        IsDead = false;
        _currentHealth = 3;
        deathScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) // for testing purposes
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        OnHit?.Invoke();
        _currentHealth--;

        UpdateHearts();
        
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        IsDead = true;
        playerMovement.StopMovement();
        StartCoroutine(DeathScreen());
    }

    private void UpdateHearts()
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
    }

    IEnumerator DeathScreen()
    {
        yield return new WaitForSecondsRealtime(1);
        deathScreen.SetActive(true);
    }
}
