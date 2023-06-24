using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Elemental
{
    public class ElementalCooldownState : ElementalBaseState
    {
        private float stateTimer;
        private float stateTime = 2f;

        public ElementalCooldownState(ElementalStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.animator.SetBool("coolingDown", true);
            stateTime = stateMachine.stats.cooldownTime;
            stateTimer = stateTime;
        }

        public override void Exit()
        {
            stateMachine.animator.SetBool("coolingDown", false);
        }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;

            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(GetNextState());
            }
        }

        private ElementalBaseState GetNextState()
        {
            float totalChance =
                stateMachine.stats.quakeChance
                + stateMachine.stats.rockFallChance
                + stateMachine.stats.dryadSpawnChance;
            float chance = Random.Range(0f, totalChance);

            if ((chance <= stateMachine.stats.dryadSpawnChance) && !stateMachine.dryadsActive)
            {
                return new ElementalDryadSpawnState(stateMachine);
            }
            else if (
                chance <= (stateMachine.stats.dryadSpawnChance + stateMachine.stats.quakeChance)
            )
            {
                return new ElementalQuakeState(stateMachine);
            }
            else
            {
                return new ElementalRockFallState(stateMachine);
            }
        }
    }
}
