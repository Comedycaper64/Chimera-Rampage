using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Elemental;
using UnityEngine;
using UnityEngine.VFX;

namespace Chimera
{
    public class ChimeraStateMachine : StateMachine
    {
        public EventHandler<ChimeraHead> OnHeadChanged;

        public InputManager inputManager;
        public HealthSystem health;
        public ChimeraStats stats;
        public ChimeraCooldowns cooldowns;
        public ChimeraMovement movement;
        public ChimeraCursorPointer cursor;
        public ChimeraRammingHitbox rammingHitbox;

        [Header("Visual Sprites")]
        public GameObject fireBreathVFX;
        public GameObject clawSwipeVFX;

        //public VisualEffect goatRamVFX;
        public GameObject goatWailVFX;

        //public VisualEffect lionDevourVFX;

        [Header("Sound Effects")]
        public AudioClip emberLaunchSFX;
        public AudioClip fireBreathSFX;
        public AudioClip goatRamSFX;
        public AudioClip goatWailSFX;
        public AudioClip lionDevourSFX;
        public AudioClip lionSwipeSFX;

        [Header("Animators")]
        [SerializeField]
        public Animator bodyAnimator;

        [SerializeField]
        public Animator lionHeadAnimator;

        [SerializeField]
        public Animator dragonHeadAnimator;

        [SerializeField]
        public Animator goatHeadAnimator;

        [Header("Misc")]
        public ChimeraHead activeHead;
        public LayerMask enemyLayerMask;
        public bool canUseAbilties = true;
        private Vector3 respawnPoint;

        [Header("Instantiated Objects")]
        public GameObject emberProjectile;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            health = GetComponent<HealthSystem>();
            stats = GetComponent<ChimeraStats>();
            cooldowns = GetComponent<ChimeraCooldowns>();
            movement = GetComponent<ChimeraMovement>();
            health.SetMaxHealth(stats.health);
        }

        private void Start()
        {
            //Event Subscriptions
            SwitchState(new ChimeraDragonIdleState(this));
            health.OnTakeDamage += TakeDamage;
            health.OnDeath += Die;
            LevelManager.OnChimeraRespawn += Respawn;
            DialogueManager.Instance.OnConversationStart += TurnOffMovement;
            DialogueManager.Instance.OnConversationEnd += TurnOnMovement;
            ElementalStateMachine.EndGame += TurnInvincible;
            TurnOffMovement();
        }

        private void OnDisable()
        {
            //Event Unsubscriptions
            health.OnTakeDamage -= TakeDamage;
            health.OnDeath -= Die;
            LevelManager.OnChimeraRespawn -= Respawn;
            DialogueManager.Instance.OnConversationStart -= TurnOffMovement;
            DialogueManager.Instance.OnConversationEnd -= TurnOnMovement;
            ElementalStateMachine.EndGame -= TurnInvincible;
        }

        private void TurnInvincible(object sender, EventArgs e)
        {
            health.isInvincible = true;
        }

        private void TakeDamage()
        {
            bodyAnimator.SetTrigger("damage");
        }

        private void Die(object sender, EventArgs e)
        {
            //Trigger death toggle in body animator
            bodyAnimator.SetTrigger("die");
            TurnOffMovement();
        }

        public void SetRespawnPoint(Vector3 respawnPoint)
        {
            //Debug.Log(respawnPoint);
            this.respawnPoint = respawnPoint;
        }

        public void Respawn()
        {
            transform.position = respawnPoint;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            health.Heal(999f);
            TurnOnMovement();
        }

        private void TurnOffMovement()
        {
            movement.ToggleMove(false);
            canUseAbilties = false;
        }

        public void TurnOnMovement()
        {
            movement.ToggleMove(true);
            inputManager.isPrimaryAbilityHeld = false;
            canUseAbilties = true;
        }
    }
}
