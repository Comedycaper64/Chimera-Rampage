using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraDragonFireBreathState : ChimeraBaseState
    {
        private float stateTime = 1f;
        private float stateTimer;

        public ChimeraDragonFireBreathState(ChimeraStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.dragonHeadAnimator.SetTrigger("flameBreath");
            Collider2D[] colliders;
            colliders = Physics2D.OverlapCircleAll(
                stateMachine.transform.position,
                stateMachine.chimeraStats.flameBreathAreaRange,
                stateMachine.enemyLayerMask
            );
            Vector2 cursorDirection = stateMachine.chimeraCursor.GetCursorDirection();
            //List<HealthSystem> unitsToDamage = new List<HealthSystem>();
            foreach (Collider2D collider in colliders)
            {
                if (!collider.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
                {
                    continue;
                }
                Vector2 colliderDirectionFromChimera = (
                    collider.transform.position - stateMachine.transform.position
                ).normalized;
                if (
                    Vector2.Dot(cursorDirection, colliderDirectionFromChimera)
                    > (1f - stateMachine.chimeraStats.flameBreathAreaArc)
                )
                {
                    healthSystem.TakeDamage(stateMachine.chimeraStats.flameBreathDamage);
                }
            }
            stateTimer = stateTime;
        }

        public override void Exit()
        {
            stateMachine.chimeraCooldowns.SetBreathCooldown();
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
