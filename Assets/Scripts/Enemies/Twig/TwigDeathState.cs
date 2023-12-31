using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Twig
{
    public class TwigDeathState : TwigBaseState
    {
        public TwigDeathState(TwigStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            //Trigger death animation
            stateMachine.isDead = true;
            stateMachine.bodyCollider.enabled = false;
            stateMachine.animator.SetTrigger("die");
        }

        public override void Exit()
        {
            stateMachine.isDead = false;
        }

        public override void Tick(float deltaTime) { }
    }
}
