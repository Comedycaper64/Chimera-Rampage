using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Elemental
{
    public class ElementalRockFallState : ElementalBaseState
    {
        private float stateTime = 1f;
        private float stateTimer;

        public ElementalRockFallState(ElementalStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.animator.SetTrigger("rock");
            stateMachine.rockFallCoroutine = stateMachine.StartCoroutine(SpawnRocks());
            stateTimer = stateTime;
        }

        public override void Exit() { }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;
            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new ElementalCooldownState(stateMachine));
                return;
            }
        }

        private IEnumerator SpawnRocks()
        {
            for (int i = 0; i < stateMachine.stats.rockFallNumber; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Vector3 spawnPosition = new Vector2(
                    Random.Range(
                        -stateMachine.stats.rockFallVariance,
                        stateMachine.stats.rockFallVariance
                    ),
                    Random.Range(
                        -stateMachine.stats.rockFallVariance,
                        stateMachine.stats.rockFallVariance
                    )
                );
                EmberProjectile rock = GameObject
                    .Instantiate(
                        stateMachine.rockProjectile,
                        stateMachine.transform.position,
                        Quaternion.identity
                    )
                    .GetComponent<EmberProjectile>();
                rock.SetupProjectile(
                    stateMachine.playerHealth.transform.position + spawnPosition,
                    stateMachine.stats.rockFallDamage,
                    stateMachine.stats.rockFallArea
                );
                // AudioSource.PlayClipAtPoint(
                //     stateMachine.emberLaunchSFX,
                //     stateMachine.transform.position,
                //     SoundManager.Instance.GetSoundEffectVolume()
                // );
            }
        }
    }
}
