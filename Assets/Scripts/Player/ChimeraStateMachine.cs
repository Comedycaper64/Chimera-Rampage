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
        public VisualEffect fireBreath;

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
        }

        private void OnDisable()
        {
            //Event Unsubscriptions
        }
    }
}
