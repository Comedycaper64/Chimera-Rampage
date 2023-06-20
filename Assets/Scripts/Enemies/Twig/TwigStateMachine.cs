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

        private void Health_OnDeath()
        {
            SwitchState(new TwigDeathState(this));
        }
    }
}
