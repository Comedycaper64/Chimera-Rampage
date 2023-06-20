using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Twig
{
    public class TwigChasingState : TwigBaseState
    {
        public TwigChasingState(TwigStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            //Enable chasing bool in animator
            stateMachine.animator.SetBool("chasing", true);
        }

        public override void Exit()
        {
            //Disable chasing bool in animator
            stateMachine.animator.SetBool("chasing", false);
        }

        public override void Tick(float deltaTime)
        {
            Vector3 directionToMove = (
                stateMachine.playerHealth.transform.position - stateMachine.transform.position
            ).normalized;
            directionToMove.z = 0f;
            stateMachine.transform.position +=
                directionToMove * stateMachine.stats.movementSpeed * deltaTime;

            if (
                Vector2.Distance(
                    stateMachine.playerHealth.transform.position,
                    stateMachine.transform.position
                ) < stateMachine.stats.attackRange
            )
            {
                stateMachine.SwitchState(new TwigAttackingState(stateMachine));
                return;
            }
        }
    }
}
