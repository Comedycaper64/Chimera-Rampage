using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraGoatRamState : ChimeraBaseState
    {
        private float stateTime = 1f;
        private float stateTimer;
        private Vector3 movementDirection;

        public ChimeraGoatRamState(ChimeraStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.goatHeadAnimator.SetTrigger("ram");
            stateMachine.movement.ToggleMove(false);
            movementDirection = stateMachine.cursor.GetCursorDirection();
            stateMachine.rammingHitbox.ToggleHitbox(
                true,
                stateMachine.stats.ramDamage,
                stateMachine.stats.ramKnockback,
                stateMachine.stats.ramHitboxSize
            );
            stateMachine.health.isInvincible = true;
            stateTime = stateMachine.stats.ramDashTime;
            stateTimer = stateTime;
        }

        public override void Exit()
        {
            stateMachine.movement.ToggleMove(true);
            stateMachine.rammingHitbox.ToggleHitbox(false);
            stateMachine.cooldowns.SetRamCooldown();
            stateMachine.health.isInvincible = false;
        }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;

            stateMachine.transform.position +=
                movementDirection
                * (stateMachine.stats.ramDashDistance / stateMachine.stats.ramDashTime)
                * deltaTime;

            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new ChimeraGoatIdleState(stateMachine));
                return;
            }
        }
    }
}
