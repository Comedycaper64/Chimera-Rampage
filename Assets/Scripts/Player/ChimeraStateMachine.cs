using System;
using System.Collections;
using System.Collections.Generic;
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

        [Header("Visual Effects")]
        public VisualEffect fireBreathVFX;
        public VisualEffect clawSwipeVFX;
        public VisualEffect goatRamVFX;
        public VisualEffect goatWailVFX;

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
            health.OnDeath += Die;
            LevelManager.OnChimeraRespawn += Respawn;
            DialogueManager.Instance.OnConversationStart += TurnOffMovement;
            DialogueManager.Instance.OnConversationEnd += TurnOnMovement;
        }

        private void OnDisable()
        {
            //Event Unsubscriptions
            health.OnDeath -= Die;
            LevelManager.OnChimeraRespawn -= Respawn;
            DialogueManager.Instance.OnConversationStart -= TurnOffMovement;
            DialogueManager.Instance.OnConversationEnd -= TurnOnMovement;
        }

        private void Die()
        {
            //Trigger death toggle in body animator
            TurnOffMovement();
        }

        public void SetRespawnPoint(Vector3 respawnPoint)
        {
            this.respawnPoint = respawnPoint;
        }

        public void Respawn()
        {
            transform.position = respawnPoint;
            health.Heal(999f);
            TurnOnMovement();
        }

        private void TurnOffMovement()
        {
            movement.ToggleMove(false);
            canUseAbilties = false;
        }

        private void TurnOnMovement()
        {
            movement.ToggleMove(true);
            canUseAbilties = true;
        }
    }
}
