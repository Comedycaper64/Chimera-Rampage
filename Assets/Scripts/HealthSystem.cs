using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public EventHandler<float> OnNewHealth;

    public Action OnTakeDamage;
    public EventHandler OnDeath;

    private float maxHealth;

    private float health;

    public bool isPlayer;

    public bool isInvincible = false;

    [SerializeField]
    private AudioClip damageSFX;

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

        // if (!isPlayer)
        // {
        //     AudioSource.PlayClipAtPoint(
        //         damageSFX,
        //         transform.position,
        //         SoundManager.Instance.GetSoundEffectVolume()
        //     );
        // }

        if (health == 0)
        {
            Die();
        }
        else
        {
            OnTakeDamage?.Invoke();
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
        OnDeath?.Invoke(this, EventArgs.Empty);
        //Destroy(gameObject);
    }
}
