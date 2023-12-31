using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Elemental;
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
        private AudioSource audioSource;
        public GameObject attackProjectile;
        public bool isDead;
        public static Action OnAnyEnemyDeath;

        private void Awake()
        {
            health = GetComponent<HealthSystem>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
            stats = GetComponent<DryadStats>();
            bodyCollider = GetComponent<Collider2D>();
            audioSource = GetComponent<AudioSource>();
            bodyCollider.enabled = false;
            health.SetMaxHealth(stats.health);
            health.OnTakeDamage += Health_OnTakeDamage;
            health.OnDeath += Health_OnDeath;
            ElementalStateMachine.EndGame += Health_OnDeath;
        }

        private void OnDisable()
        {
            health.OnTakeDamage -= Health_OnTakeDamage;
            health.OnDeath -= Health_OnDeath;
            ElementalStateMachine.EndGame -= Health_OnDeath;
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

        private void Health_OnTakeDamage()
        {
            animator.SetTrigger("damage");
            if (!audioSource.isPlaying)
            {
                audioSource.volume = SoundManager.Instance.GetSoundEffectVolume() * 0.25f;
                audioSource.Play();
            }
        }

        private void Health_OnDeath(object sender, EventArgs e)
        {
            OnAnyEnemyDeath?.Invoke();
            SwitchState(new DryadDeathState(this));
        }
    }
}
