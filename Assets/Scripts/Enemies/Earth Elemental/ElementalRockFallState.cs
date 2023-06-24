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
            stateMachine.StartCoroutine(SpawnRocks());
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
                Vector2 spawnPosition = new Vector2(
                    Random.Range(stateMachine.arenaMin.x, stateMachine.arenaMax.x),
                    Random.Range(stateMachine.arenaMin.y, stateMachine.arenaMax.y)
                );
                EmberProjectile rock = GameObject
                    .Instantiate(
                        stateMachine.rockProjectile,
                        stateMachine.transform.position,
                        Quaternion.identity
                    )
                    .GetComponent<EmberProjectile>();
                rock.SetupProjectile(
                    spawnPosition,
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
