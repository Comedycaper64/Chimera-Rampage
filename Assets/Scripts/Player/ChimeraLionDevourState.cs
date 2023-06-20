using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraLionDevourState : ChimeraBaseState
    {
        private float stateTime = 0.5f;
        private float stateTimer;

        public ChimeraLionDevourState(ChimeraStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.lionHeadAnimator.SetTrigger("devour");
            Collider2D[] colliders;
            colliders = Physics2D.OverlapCircleAll(
                stateMachine.transform.position,
                stateMachine.stats.devourRange,
                stateMachine.enemyLayerMask
            );
            Vector2 cursorDirection = stateMachine.cursor.GetCursorDirection();
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
                    > (1f - stateMachine.stats.devourArc)
                )
                {
                    stateMachine.health.Heal(
                        Mathf.RoundToInt(
                            Mathf.Min(stateMachine.stats.devourDamage, healthSystem.GetHealth())
                                / 4f
                        )
                    );
                    healthSystem.TakeDamage(stateMachine.stats.devourDamage);
                }
            }
            stateTimer = stateTime;
        }

        public override void Exit()
        {
            stateMachine.cooldowns.SetDevourCooldown();
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
