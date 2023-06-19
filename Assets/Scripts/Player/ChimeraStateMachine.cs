using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraStateMachine : StateMachine
    {
        public EventHandler<ChimeraHead> OnHeadChanged;

        public InputManager inputManager;
        public HealthSystem chimeraHealth;
        public ChimeraStats chimeraStats;
        public ChimeraCooldowns chimeraCooldowns;
        public ChimeraMovement chimeraMovement;
        public ChimeraCursorPointer chimeraCursor;
        public ChimeraRammingHitbox chimeraRammingHitbox;

        //public ChimeraUI chimeraUI;
        [SerializeField]
        public Animator bodyAnimator;

        [SerializeField]
        public Animator lionHeadAnimator;

        [SerializeField]
        public Animator dragonHeadAnimator;

        [SerializeField]
        public Animator goatHeadAnimator;

        public ChimeraHead activeHead;
        public LayerMask enemyLayerMask;

        [Header("Instantiated Objects")]
        public GameObject emberProjectile;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            chimeraHealth = GetComponent<HealthSystem>();
            chimeraStats = GetComponent<ChimeraStats>();
            chimeraCooldowns = GetComponent<ChimeraCooldowns>();
            chimeraMovement = GetComponent<ChimeraMovement>();
            //chimeraUI = GameObject.FindGameObjectWithTag("ChimeraUI").GetComponent<ChimeraUI>();
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
