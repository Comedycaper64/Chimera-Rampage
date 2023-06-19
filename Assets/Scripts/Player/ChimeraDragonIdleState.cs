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
            stateMachine.inputManager.SwitchLeftEvent += SwitchToGoat;
            stateMachine.inputManager.SwitchRightEvent += SwitchToLion;
        }

        public override void Exit()
        {
            stateMachine.inputManager.SwitchLeftEvent -= SwitchToGoat;
            stateMachine.inputManager.SwitchRightEvent -= SwitchToLion;
        }

        public override void Tick(float deltaTime) { }
    }
}
