using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Twig
{
    public class TwigSpawningState : TwigBaseState
    {
        float stateTime = 2f;
        float stateTimer;

        public TwigSpawningState(TwigStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateTimer = stateTime;
            //Trigger sprouting animation
            stateMachine.animator.SetTrigger("spawn");
        }

        public override void Exit()
        {
            //Trigger unearthing animation, enable colliders
            stateMachine.bodyCollider.enabled = true;
        }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;
            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new TwigChasingState(stateMachine));
                return;
            }
        }
    }
}
