using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraLionSwipeState : ChimeraBaseState
    {
        private float stateTime = 0.3f;
        private float stateTimer;

        public ChimeraLionSwipeState(ChimeraStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.bodyAnimator.SetTrigger("swipe");
            Collider2D[] colliders;
            colliders = Physics2D.OverlapCircleAll(
                stateMachine.transform.position,
                stateMachine.chimeraStats.swipeRange,
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
                    > (1f - stateMachine.chimeraStats.swipeArc)
                )
                {
                    healthSystem.TakeDamage(stateMachine.chimeraStats.swipeDamage);
                }
            }
            stateTimer = stateTime;
        }

        public override void Exit()
        {
            stateMachine.chimeraCooldowns.SetSwipeCooldown();
        }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;
            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new ChimeraLionIdleState(stateMachine));
            }
        }
    }
}
