using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public EventHandler<float> OnNewHealth;

    public Action OnDeath;

    [SerializeField]
    private int maxHealth;

    private int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);

        OnNewHealth?.Invoke(this, (float)health / maxHealth);

        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
}
