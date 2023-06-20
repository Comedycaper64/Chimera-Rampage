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

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
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

    public void Heal(int amountToHeal)
    {
        health = Mathf.Min(maxHealth, health + amountToHeal);
        OnNewHealth?.Invoke(this, (float)health / maxHealth);
    }

    public int GetHealth()
    {
        return health;
    }

    private void Die()
    {
        OnDeath?.Invoke();
        //Destroy(gameObject);
    }
}
