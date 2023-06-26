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
        private AudioSource audioSource;

        public static Action OnAnyEnemyDeath;

        private void Awake()
        {
            health = GetComponent<HealthSystem>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
            stats = GetComponent<TwigStats>();
            bodyCollider = GetComponent<Collider2D>();
            audioSource = GetComponent<AudioSource>();
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
            if (!audioSource.isPlaying)
            {
                audioSource.volume = SoundManager.Instance.GetSoundEffectVolume() * 0.25f;
                audioSource.Play();
            }
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

        private void Health_OnDeath(object sender, EventArgs e)
        {
            OnAnyEnemyDeath?.Invoke();
            SwitchState(new TwigDeathState(this));
        }
    }
}
