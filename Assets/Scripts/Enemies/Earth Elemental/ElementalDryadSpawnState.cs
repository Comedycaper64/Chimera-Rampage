using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Elemental
{
    public class ElementalDryadSpawnState : ElementalBaseState
    {
        private float stateTime = 2f;
        private float stateTimer;
        private bool summoned = false;

        public ElementalDryadSpawnState(ElementalStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.animator.SetTrigger("summon");
            stateTimer = stateTime;
        }

        public override void Exit() { }

        public override void Tick(float deltaTime)
        {
            if ((stateTimer <= 1f) && !summoned && !stateMachine.isDead)
            {
                SpawnDryads();
                summoned = true;
            }

            stateTimer -= deltaTime;
            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new ElementalCooldownState(stateMachine));
                return;
            }
        }

        private void SpawnDryads()
        {
            stateMachine.aliveDryads = 0;
            stateMachine.dryadsActive = true;
            for (int i = 0; i < stateMachine.stats.dryadSpawnNumber; i++)
            {
                Vector2 spawnLocation = new Vector2(
                    Random.Range(stateMachine.arenaMin.x, stateMachine.arenaMax.x),
                    Random.Range(stateMachine.arenaMin.y, stateMachine.arenaMax.y)
                );

                HealthSystem dryadHealth = GameObject
                    .Instantiate(stateMachine.dryadPrefab, spawnLocation, Quaternion.identity)
                    .GetComponent<HealthSystem>();
                dryadHealth.OnDeath += stateMachine.ReduceActiveDryads;
            }
        }
    }
}
