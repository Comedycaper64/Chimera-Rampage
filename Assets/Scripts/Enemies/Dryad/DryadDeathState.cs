using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Dryad
{
    public class DryadDeathState : DryadBaseState
    {
        public DryadDeathState(DryadStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            //Trigger death animation
            stateMachine.animator.SetTrigger("death");
            stateMachine.isDead = true;
            stateMachine.bodyCollider.enabled = false;
        }

        public override void Exit()
        {
            stateMachine.isDead = false;
        }

        public override void Tick(float deltaTime) { }
    }
}
