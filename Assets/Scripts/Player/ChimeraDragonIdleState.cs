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
            stateMachine.inputManager.SecondaryAbilityEvent += FireBreath;
            stateMachine.inputManager.SwitchLeftEvent += SwitchToGoat;
            stateMachine.inputManager.SwitchRightEvent += SwitchToLion;
        }

        public override void Exit()
        {
            stateMachine.inputManager.SecondaryAbilityEvent -= FireBreath;
            stateMachine.inputManager.SwitchLeftEvent -= SwitchToGoat;
            stateMachine.inputManager.SwitchRightEvent -= SwitchToLion;
        }

        public override void Tick(float deltaTime)
        {
            if (
                stateMachine.inputManager.isPrimaryAbilityHeld
                && (stateMachine.chimeraCooldowns.emberCooldown <= 0f)
            )
            {
                stateMachine.SwitchState(new ChimeraDragonEmberState(stateMachine));
                return;
            }
        }

        private void FireBreath()
        {
            if (stateMachine.chimeraCooldowns.breathCooldown <= 0f)
            {
                stateMachine.SwitchState(new ChimeraDragonFireBreathState(stateMachine));
            }
        }
    }
}
