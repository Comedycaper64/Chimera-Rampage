using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Twig
{
    public class TwigStateMachine : StateMachine
    {
        public HealthSystem health;
        public HealthSystem playerHealth;
        public TwigStats stats;
        public Collider2D bodyCollider;
        public Animator animator;
        public bool isDead;

        public static Action OnAnyEnemyDeath;

        private void Awake()
        {
            health = GetComponent<HealthSystem>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
            stats = GetComponent<TwigStats>();
            bodyCollider = GetComponent<Collider2D>();
            bodyCollider.enabled = false;
            health.SetMaxHealth(stats.health);
            health.OnTakeDamage += Health_OnDamage;
            health.OnDeath += Health_OnDeath;
        }

        private void OnDisable()
        {
            health.OnTakeDamage -= Health_OnDamage;
            health.OnDeath -= Health_OnDeath;
        }

        private void Health_OnDamage()
        {
            animator.SetTrigger("damage");
        }

        private void Start()
        {
            SwitchState(new TwigSpawningState(this));
        }

        public void RespawnTwig(Vector2 spawnLocation)
        {
            transform.position = spawnLocation;
            health.Heal(999);
            SwitchState(new TwigSpawningState(this));
        }

        public override void WailDebuff(float debuffTime)
        {
            StartCoroutine(DebuffSpeed(debuffTime));
        }

        private IEnumerator DebuffSpeed(float debuffTime)
        {
            float originalSpeed = stats.movementSpeed;
            stats.movementSpeed = stats.movementSpeed / 2;
            yield return new WaitForSeconds(debuffTime);
            stats.movementSpeed = originalSpeed;
        }

        // public void Knockback(Vector2 knockBackDirection, float knockBackForce)
        // {
        //     StartCoroutine(KnockbackCoroutine(knockBackDirection, knockBackForce));
        // }

        // private IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float knockBackForce)
        // {
        //     //canMove = false;
        //     GetComponent<Rigidbody2D>()
        //         .AddForce(knockbackDirection * knockBackForce, ForceMode2D.Impulse);
        //     yield return new WaitForSeconds(1f);
        //     //canMove = true;
        // }

        private void Health_OnDeath()
        {
            OnAnyEnemyDeath?.Invoke();
            SwitchState(new TwigDeathState(this));
        }
    }
}
