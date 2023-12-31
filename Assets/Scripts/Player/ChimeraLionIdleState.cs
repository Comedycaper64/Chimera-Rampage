using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraLionIdleState : ChimeraBaseState
    {
        public ChimeraLionIdleState(ChimeraStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            ChangeActiveHead(ChimeraHead.Lion);
            stateMachine.inputManager.SecondaryAbilityEvent += DevourAbility;
            stateMachine.inputManager.SwitchLeftEvent += SwitchToDragon;
            stateMachine.inputManager.SwitchRightEvent += SwitchToGoat;
        }

        public override void Exit()
        {
            stateMachine.inputManager.SecondaryAbilityEvent -= DevourAbility;
            stateMachine.inputManager.SwitchLeftEvent -= SwitchToDragon;
            stateMachine.inputManager.SwitchRightEvent -= SwitchToGoat;
        }

        public override void Tick(float deltaTime)
        {
            if (
                stateMachine.canUseAbilties
                && stateMachine.inputManager.isPrimaryAbilityHeld
                && (stateMachine.cooldowns.swipeCooldown <= 0f)
            )
            {
                stateMachine.SwitchState(new ChimeraLionSwipeState(stateMachine));
                return;
            }
        }

        private void DevourAbility()
        {
            if (stateMachine.canUseAbilties && (stateMachine.cooldowns.devourCooldown <= 0f))
            {
                stateMachine.SwitchState(new ChimeraLionDevourState(stateMachine));
            }
        }
    }
}
