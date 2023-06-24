using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Elemental
{
    public class ElementalQuakeState : ElementalBaseState
    {
        private float stateTimer;
        private float stateTime = 2f;
        private Vector2 playerDirection;
        private bool attacked = false;

        public ElementalQuakeState(ElementalStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.animator.SetTrigger("quake");
            playerDirection =
                stateMachine.playerHealth.transform.position - stateMachine.transform.position;
            stateTimer = stateTime;
            stateMachine.quakeArea.gameObject.SetActive(true);
        }

        public override void Exit() { }

        public override void Tick(float deltaTime)
        {
            if ((stateTimer <= 1f) && !attacked)
            {
                Collider2D[] colliders;
                colliders = Physics2D.OverlapCircleAll(
                    stateMachine.transform.position,
                    stateMachine.stats.quakeRange
                );

                //List<HealthSystem> unitsToDamage = new List<HealthSystem>();
                foreach (Collider2D collider in colliders)
                {
                    if (
                        collider.TryGetComponent<HealthSystem>(out HealthSystem healthSystem)
                        && healthSystem.isPlayer
                    )
                    {
                        healthSystem.TakeDamage(stateMachine.stats.quakeDamage);
                    }
                }

                attacked = true;
            }

            stateTimer -= deltaTime;
            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new ElementalCooldownState(stateMachine));
                return;
            }
        }
    }
}
