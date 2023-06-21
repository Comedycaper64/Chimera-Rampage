using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Myconid
{
    public class MyconidDeathState : MyconidBaseState
    {
        public MyconidDeathState(MyconidStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            //Trigger death animation
            stateMachine.animator.SetTrigger("die");
            stateMachine.isDead = true;
            stateMachine.bodyCollider.enabled = false;
            stateMachine.mushroomCloud.ToggleCloud(false);
        }

        public override void Exit()
        {
            stateMachine.isDead = false;
        }

        public override void Tick(float deltaTime) { }
    }
}
