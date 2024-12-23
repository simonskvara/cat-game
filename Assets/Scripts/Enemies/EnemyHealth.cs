using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public GameObject deadEnemy;
    private float _currentHealth;
    private bool _isDead;

    public UnityEvent onDeath = new UnityEvent();
    public UnityEvent onHit = new UnityEvent();
    
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0 && !_isDead)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        onHit?.Invoke();
    }

    private void Die()
    {
        onDeath.Invoke();
        _isDead = true;
        GameObject spawnedDeadEnemy = Instantiate(deadEnemy, gameObject.transform.position, Quaternion.identity);
        spawnedDeadEnemy.transform.localScale = transform.localScale;
        // TODO: instantiate death animation
        Destroy(gameObject);
    }
}
