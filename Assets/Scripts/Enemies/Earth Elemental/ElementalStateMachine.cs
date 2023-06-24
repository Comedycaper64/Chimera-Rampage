using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Elemental
{
    public class ElementalStateMachine : StateMachine
    {
        public HealthSystem health;
        public HealthSystem playerHealth;
        public ElementalStats stats;
        public Collider2D bodyCollider;
        public Animator animator;
        public bool isDead;
        public bool dryadsActive;
        private Coroutine debuffCoroutine;
        public Collider2D quakeArea;
        public Vector2 arenaMin;
        public Vector2 arenaMax;
        public GameObject rockProjectile;

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
        }

        private void OnDisable()
        {
            health.OnTakeDamage -= Health_OnTakeDamage;
            health.OnDeath -= Health_OnDeath;
        }

        private void Start()
        {
            SwitchState(new ElementalIdleState(this));
        }

        //Do on Chimera Respawn

        public void RespawnElemental(Vector2 spawnLocation)
        {
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

        private void Health_OnDeath()
        {
            if (debuffCoroutine != null)
            {
                StopCoroutine(debuffCoroutine);
            }
            SwitchState(new ElementalDeathState(this));
        }
    }
}
