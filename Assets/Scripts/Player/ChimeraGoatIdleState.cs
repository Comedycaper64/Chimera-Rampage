using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraGoatIdleState : ChimeraBaseState
    {
        public ChimeraGoatIdleState(ChimeraStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            ChangeActiveHead(ChimeraHead.Goat);
            stateMachine.inputManager.SecondaryAbilityEvent += WailAbility;
            stateMachine.inputManager.SwitchLeftEvent += SwitchToLion;
            stateMachine.inputManager.SwitchRightEvent += SwitchToDragon;
        }

        public override void Exit()
        {
            stateMachine.inputManager.SecondaryAbilityEvent -= WailAbility;
            stateMachine.inputManager.SwitchLeftEvent -= SwitchToLion;
            stateMachine.inputManager.SwitchRightEvent -= SwitchToDragon;
        }

        public override void Tick(float deltaTime)
        {
            if (
                stateMachine.canUseAbilties
                && stateMachine.inputManager.isPrimaryAbilityHeld
                && (stateMachine.cooldowns.ramCooldown <= 0f)
            )
            {
                stateMachine.SwitchState(new ChimeraGoatRamState(stateMachine));
                return;
            }
        }

        private void WailAbility()
        {
            if (stateMachine.canUseAbilties && (stateMachine.cooldowns.wailCooldown <= 0f))
            {
                stateMachine.SwitchState(new ChimeraGoatWailState(stateMachine));
            }
        }
    }
}
