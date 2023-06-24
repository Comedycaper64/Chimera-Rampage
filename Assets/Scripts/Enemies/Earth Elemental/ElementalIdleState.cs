using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Elemental
{
    public class ElementalIdleState : ElementalBaseState
    {
        public ElementalIdleState(ElementalStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.animator.SetBool("idle", true);
        }

        public override void Exit()
        {
            stateMachine.animator.SetBool("idle", true);
        }

        public override void Tick(float deltaTime)
        {
            if (
                Vector3.Distance(
                    stateMachine.transform.position,
                    stateMachine.playerHealth.transform.position
                ) < stateMachine.stats.encounterStartRange
            )
            {
                stateMachine.SwitchState(new ElementalCooldownState(stateMachine));
            }
        }
    }
}
