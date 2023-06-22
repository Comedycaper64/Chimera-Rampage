using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Chimera
{
    public class ChimeraDragonIdleState : ChimeraBaseState
    {
        public ChimeraDragonIdleState(ChimeraStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            ChangeActiveHead(ChimeraHead.Dragon);
            stateMachine.inputManager.SecondaryAbilityEvent += FireBreathAbility;
            stateMachine.inputManager.SwitchLeftEvent += SwitchToGoat;
            stateMachine.inputManager.SwitchRightEvent += SwitchToLion;
        }

        public override void Exit()
        {
            stateMachine.inputManager.SecondaryAbilityEvent -= FireBreathAbility;
            stateMachine.inputManager.SwitchLeftEvent -= SwitchToGoat;
            stateMachine.inputManager.SwitchRightEvent -= SwitchToLion;
        }

        public override void Tick(float deltaTime)
        {
            if (
                stateMachine.canUseAbilties
                && stateMachine.inputManager.isPrimaryAbilityHeld
                && (stateMachine.cooldowns.emberCooldown <= 0f)
            )
            {
                stateMachine.SwitchState(new ChimeraDragonEmberState(stateMachine));
                return;
            }
        }

        private void FireBreathAbility()
        {
            if (stateMachine.canUseAbilties && (stateMachine.cooldowns.breathCooldown <= 0f))
            {
                stateMachine.SwitchState(new ChimeraDragonFireBreathState(stateMachine));
            }
        }
    }
}
