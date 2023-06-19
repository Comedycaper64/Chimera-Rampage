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
            stateMachine.chimeraMovement.ToggleMove(false);
            movementDirection = stateMachine.chimeraCursor.GetCursorDirection();
            stateMachine.chimeraRammingHitbox.ToggleHitbox(
                true,
                stateMachine.chimeraStats.ramDamage,
                stateMachine.chimeraStats.ramKnockback,
                stateMachine.chimeraStats.ramHitboxSize
            );
            stateTime = stateMachine.chimeraStats.ramDashTime;
            stateTimer = stateTime;
        }

        public override void Exit()
        {
            stateMachine.chimeraMovement.ToggleMove(true);
            stateMachine.chimeraRammingHitbox.ToggleHitbox(false);
            stateMachine.chimeraCooldowns.SetRamCooldown();
        }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;

            stateMachine.transform.position +=
                movementDirection
                * (
                    stateMachine.chimeraStats.ramDashDistance
                    / stateMachine.chimeraStats.ramDashTime
                )
                * deltaTime;

            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new ChimeraGoatIdleState(stateMachine));
                return;
            }
        }
    }
}
