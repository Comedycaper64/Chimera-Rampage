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
            stateMachine.inputManager.SwitchLeftEvent += SwitchToLion;
            stateMachine.inputManager.SwitchRightEvent += SwitchToDragon;
        }

        public override void Exit()
        {
            stateMachine.inputManager.SwitchLeftEvent -= SwitchToLion;
            stateMachine.inputManager.SwitchRightEvent -= SwitchToDragon;
        }

        public override void Tick(float deltaTime) { }
    }
}
