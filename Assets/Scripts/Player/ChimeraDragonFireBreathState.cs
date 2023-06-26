using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraDragonFireBreathState : ChimeraBaseState
    {
        private float stateTime = 1.5f;
        private float stateTimer;

        public ChimeraDragonFireBreathState(ChimeraStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.dragonHeadAnimator.SetTrigger("flameBreath");
            stateMachine.fireBreathVFX.SetActive(true);
            AudioSource.PlayClipAtPoint(
                stateMachine.fireBreathSFX,
                stateMachine.transform.position,
                SoundManager.Instance.GetSoundEffectVolume()
            );
            stateTimer = stateTime;
        }

        public override void Exit()
        {
            stateMachine.cooldowns.SetBreathCooldown();
            stateMachine.fireBreathVFX.SetActive(false);
        }

        public override void Tick(float deltaTime)
        {
            Collider2D[] colliders;
            colliders = Physics2D.OverlapCircleAll(
                stateMachine.transform.position,
                stateMachine.stats.flameBreathAreaRange,
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
                    > (1f - stateMachine.stats.flameBreathAreaArc)
                )
                {
                    healthSystem.TakeDamage(stateMachine.stats.flameBreathDamage * deltaTime);
                }
            }
            stateTimer -= deltaTime;
            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new ChimeraDragonIdleState(stateMachine));
                return;
            }
        }
    }
}
