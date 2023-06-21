using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public EventHandler<float> OnNewHealth;

    public Action OnDeath;

    private float maxHealth;

    private float health;

    public bool isPlayer;

    public bool isInvincible = false;

    private void Awake()
    {
        health = maxHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            return;
        }

        health = Mathf.Max(0, health - damage);

        OnNewHealth?.Invoke(this, health / maxHealth);

        if (health == 0)
        {
            Die();
        }
    }

    public void Heal(float amountToHeal)
    {
        health = Mathf.Min(maxHealth, health + amountToHeal);
        OnNewHealth?.Invoke(this, health / maxHealth);
    }

    public float GetHealth()
    {
        return health;
    }

    private void Die()
    {
        OnDeath?.Invoke();
        //Destroy(gameObject);
    }
}
