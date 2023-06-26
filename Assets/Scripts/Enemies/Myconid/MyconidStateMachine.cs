using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Myconid
{
    public class MyconidStateMachine : StateMachine
    {
        public HealthSystem health;
        public HealthSystem playerHealth;
        public MyconidStats stats;
        public Collider2D bodyCollider;
        public Animator animator;
        private AudioSource audioSource;
        public MyconidMushroomCloud mushroomCloud;
        public bool isDead;
        public static Action OnAnyEnemyDeath;
        private Coroutine debuffCoroutine;

        private void Awake()
        {
            health = GetComponent<HealthSystem>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
            stats = GetComponent<MyconidStats>();
            bodyCollider = GetComponent<Collider2D>();
            audioSource = GetComponent<AudioSource>();
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
            SwitchState(new MyconidSpawnState(this));
        }

        public void RespawnMyconid(Vector2 spawnLocation)
        {
            transform.position = spawnLocation;
            health.Heal(999);
            SwitchState(new MyconidSpawnState(this));
        }

        public override void WailDebuff(float debuffTime)
        {
            if (isDead)
            {
                return;
            }

            debuffCoroutine = StartCoroutine(DebuffCloud(debuffTime));
        }

        public IEnumerator DebuffCloud(float debuffTime)
        {
            mushroomCloud.ToggleCloud(false);
            yield return new WaitForSeconds(debuffTime);
            mushroomCloud.ToggleCloud(true);
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
            if (debuffCoroutine != null)
            {
                StopCoroutine(debuffCoroutine);
            }
            SwitchState(new MyconidDeathState(this));
        }
    }
}
