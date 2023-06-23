using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraGoatWailState : ChimeraBaseState
    {
        private float stateTime = 1f;
        private float stateTimer;

        public ChimeraGoatWailState(ChimeraStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.goatHeadAnimator.SetTrigger("wail");
            stateMachine.goatWailVFX.Play();
            AudioSource.PlayClipAtPoint(
                stateMachine.goatWailSFX,
                stateMachine.transform.position,
                SoundManager.Instance.GetSoundEffectVolume()
            );
            Collider2D[] colliders;
            colliders = Physics2D.OverlapCircleAll(
                stateMachine.transform.position,
                stateMachine.stats.wailRange,
                stateMachine.enemyLayerMask
            );

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent<DryadProjectile>(out DryadProjectile projectile))
                {
                    projectile.DestroyProjectile();
                    continue;
                }

                if (!collider.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
                {
                    continue;
                }

                healthSystem.TakeDamage(stateMachine.stats.wailDamage);
                healthSystem
                    .GetComponent<StateMachine>()
                    .WailDebuff(stateMachine.stats.wailDebuffDuration);
                //Slow enemy
            }
            stateTimer = stateTime;
        }

        public override void Exit()
        {
            stateMachine.cooldowns.SetWailCooldown();
        }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;
            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new ChimeraGoatIdleState(stateMachine));
                return;
            }
        }
    }
}
