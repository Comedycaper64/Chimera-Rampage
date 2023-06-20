using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraDragonEmberState : ChimeraBaseState
    {
        private float stateTime = 0.5f;
        private float stateTimer;

        public ChimeraDragonEmberState(ChimeraStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.dragonHeadAnimator.SetTrigger("ember");
            EmberProjectile ember = GameObject
                .Instantiate(
                    stateMachine.emberProjectile,
                    stateMachine.transform.position,
                    Quaternion.identity
                )
                .GetComponent<EmberProjectile>();
            ember.SetupProjectile(
                stateMachine.cursor.GetMouseWorldPosition(),
                stateMachine.stats.emberDamage,
                stateMachine.stats.emberAreaOfEffect
            );
            stateTimer = stateTime;
        }

        public override void Exit()
        {
            stateMachine.cooldowns.SetEmberCooldown();
        }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;
            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new ChimeraDragonIdleState(stateMachine));
                return;
            }
        }
    }
}
