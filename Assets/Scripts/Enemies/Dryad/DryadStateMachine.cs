using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Dryad
{
    public class DryadStateMachine : StateMachine
    {
        public HealthSystem health;
        public HealthSystem playerHealth;
        public DryadStats stats;
        public Collider2D bodyCollider;
        public Animator animator;
        public GameObject attackProjectile;
        public bool isDead;
        public static Action OnAnyEnemyDeath;

        private void Awake()
        {
            health = GetComponent<HealthSystem>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
            stats = GetComponent<DryadStats>();
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
            SwitchState(new DryadSpawnState(this));
        }

        public void RespawnDryad(Vector2 spawnLocation)
        {
            transform.position = spawnLocation;
            health.Heal(999);
            SwitchState(new DryadSpawnState(this));
        }

        public override void WailDebuff(float debuffTime)
        {
            StartCoroutine(DebuffSpeed(debuffTime));
        }

        public IEnumerator DebuffSpeed(float debuffTime)
        {
            float originalSpeed = stats.attackIntervals;
            stats.attackIntervals = stats.attackIntervals * 2;
            yield return new WaitForSeconds(debuffTime);
            stats.attackIntervals = originalSpeed;
        }

        private void Health_OnDeath()
        {
            OnAnyEnemyDeath?.Invoke();
            SwitchState(new DryadDeathState(this));
        }
    }
}
