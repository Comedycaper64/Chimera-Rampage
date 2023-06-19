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
            stateMachine.inputManager.SwitchLeftEvent += SwitchToDragon;
            stateMachine.inputManager.SwitchRightEvent += SwitchToGoat;
        }

        public override void Exit()
        {
            stateMachine.inputManager.SwitchLeftEvent -= SwitchToDragon;
            stateMachine.inputManager.SwitchRightEvent -= SwitchToGoat;
        }

        public override void Tick(float deltaTime) { }
    }
}
