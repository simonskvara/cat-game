using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    private float _currentHealth;

    public UnityEvent onDeath = new UnityEvent();
    
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

    private void Die()
    {
        Debug.Log("Enemy died");
        onDeath.Invoke(); // not yet used for anything
        // TODO: instantiate death animation
        Destroy(gameObject);
    }
}
