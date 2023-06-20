using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Twig
{
    public class TwigAttackingState : TwigBaseState
    {
        private bool hasAttacked;
        float stateTime = 2f;
        float stateTimer;

        public TwigAttackingState(TwigStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            //Trigger attack animation
            stateMachine.animator.SetTrigger("attack");
            hasAttacked = false;
            stateTimer = stateTime;
        }

        public override void Exit() { }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;

            if (!hasAttacked && stateTimer <= (stateTime * (1 - stateMachine.stats.attackTiming)))
            {
                //Attack
                if (
                    Vector2.Distance(
                        stateMachine.playerHealth.transform.position,
                        stateMachine.transform.position
                    ) < stateMachine.stats.damageRange
                )
                {
                    stateMachine.playerHealth.TakeDamage(stateMachine.stats.attackDamage);
                }
                hasAttacked = true;
            }

            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new TwigChasingState(stateMachine));
                return;
            }
        }
    }
}
