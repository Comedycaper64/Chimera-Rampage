using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Elemental
{
    public class ElementalStateMachine : StateMachine
    {
        public static EventHandler EndGame;

        public HealthSystem health;
        public HealthSystem playerHealth;
        public ElementalStats stats;
        public Collider2D bodyCollider;
        public Animator animator;
        public bool isDead;
        public bool dryadsActive = false;
        private Coroutine debuffCoroutine;
        public Coroutine rockFallCoroutine;
        public Collider2D quakeArea;
        public Vector2 arenaMin;
        public Vector2 arenaMax;
        public Vector2 spawnLocation;
        public GameObject rockProjectile;
        public GameObject dryadPrefab;
        public int aliveDryads;
        public Conversation endOfGameDialogue;

        private void Awake()
        {
            health = GetComponent<HealthSystem>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
            stats = GetComponent<ElementalStats>();
            bodyCollider = GetComponent<Collider2D>();
            bodyCollider.enabled = false;
            health.SetMaxHealth(stats.health);
            health.OnTakeDamage += Health_OnTakeDamage;
            health.OnDeath += Health_OnDeath;
            LevelManager.OnChimeraRespawn += RespawnElemental;
        }

        private void OnDisable()
        {
            health.OnTakeDamage -= Health_OnTakeDamage;
            health.OnDeath -= Health_OnDeath;
            LevelManager.OnChimeraRespawn -= RespawnElemental;
        }

        private void Start()
        {
            SwitchState(new ElementalIdleState(this));
        }

        //Do on Chimera Respawn

        public void RespawnElemental()
        {
            if (rockFallCoroutine != null)
            {
                StopCoroutine(rockFallCoroutine);
            }

            transform.position = spawnLocation;
            health.Heal(9999f);
            SwitchState(new ElementalIdleState(this));
        }

        public override void WailDebuff(float debuffTime)
        {
            if (isDead)
            {
                return;
            }

            //debuffCoroutine = StartCoroutine(DebuffCloud(debuffTime));
        }

        private void Health_OnTakeDamage()
        {
            animator.SetTrigger("damage");
        }

        private void Health_OnDeath(object sender, EventArgs e)
        {
            if (debuffCoroutine != null)
            {
                StopCoroutine(debuffCoroutine);
            }
            if (rockFallCoroutine != null)
            {
                StopCoroutine(rockFallCoroutine);
            }
            EndGame?.Invoke(this, EventArgs.Empty);
            SwitchState(new ElementalDeathState(this));
        }

        public void ReduceActiveDryads(object sender, EventArgs e)
        {
            HealthSystem dryadHealth = (HealthSystem)sender;
            dryadHealth.OnDeath -= ReduceActiveDryads;
            aliveDryads--;
            if (aliveDryads <= 0)
            {
                dryadsActive = false;
            }
        }
    }
}
