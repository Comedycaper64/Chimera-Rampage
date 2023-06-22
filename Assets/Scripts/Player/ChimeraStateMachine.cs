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

        [Header("Animators")]
        //public ChimeraUI chimeraUI;
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
            DialogueManager.Instance.OnConversationStart += TurnOffMovement;
            DialogueManager.Instance.OnConversationEnd += TurnOnMovement;
        }

        private void OnDisable()
        {
            //Event Unsubscriptions
            DialogueManager.Instance.OnConversationStart -= TurnOffMovement;
            DialogueManager.Instance.OnConversationEnd -= TurnOnMovement;
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
