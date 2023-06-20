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
            stateMachine.animator.SetTrigger("die");
            stateMachine.bodyCollider.enabled = false;
        }

        public override void Exit() { }

        public override void Tick(float deltaTime) { }
    }
}
