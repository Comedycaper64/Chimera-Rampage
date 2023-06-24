using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Dryad
{
    public class DryadAttackingState : DryadBaseState
    {
        private bool hasAttacked;
        float stateTime = 2f;
        float stateTimer;

        public DryadAttackingState(DryadStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            //Trigger attack animation
            AttackReset();
        }

        private void AttackReset()
        {
            stateMachine.animator.SetTrigger("attack");
            hasAttacked = false;
            stateTime = stateMachine.stats.attackIntervals;
            stateTimer = stateTime;
        }

        public override void Exit() { }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;

            if (!hasAttacked && stateTimer <= (stateTime * (1 - stateMachine.stats.attackTiming)))
            {
                Vector2 playerDirection = (
                    stateMachine.playerHealth.transform.position - stateMachine.transform.position
                ).normalized;

                float zAngle = Vector3.Angle(Vector3.up, playerDirection);

                Quaternion projectileRotation = Quaternion.Euler(0f, 0f, zAngle);

                if (playerDirection.x >= 0f)
                {
                    projectileRotation.eulerAngles = new Vector3(
                        projectileRotation.eulerAngles.x,
                        projectileRotation.eulerAngles.y,
                        -projectileRotation.eulerAngles.z
                    );
                }

                //Instantiate and setup projectile
                DryadProjectile projectile = GameObject
                    .Instantiate(
                        stateMachine.attackProjectile,
                        stateMachine.transform.position,
                        projectileRotation
                    )
                    .GetComponent<DryadProjectile>();
                projectile.SetupProjectile(
                    stateMachine.stats.attackDamage,
                    stateMachine.stats.attackProjectileSpeed,
                    playerDirection
                );

                hasAttacked = true;
            }

            if (stateTimer <= 0f)
            {
                AttackReset();
            }
        }
    }
}
