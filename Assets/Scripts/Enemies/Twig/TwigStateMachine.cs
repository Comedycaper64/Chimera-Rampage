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
            health.OnDeath += Health_OnDeath;
        }

        private void OnDisable()
        {
            health.OnDeath -= Health_OnDeath;
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

        public IEnumerator DebuffSpeed(float debuffTime)
        {
            float originalSpeed = stats.movementSpeed;
            stats.movementSpeed = stats.movementSpeed / 2;
            yield return new WaitForSeconds(debuffTime);
            stats.movementSpeed = originalSpeed;
        }

        private void Health_OnDeath()
        {
            OnAnyEnemyDeath?.Invoke();
            SwitchState(new TwigDeathState(this));
        }
    }
}
