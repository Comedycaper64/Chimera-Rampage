using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Myconid
{
    public class MyconidSpawnState : MyconidBaseState
    {
        float stateTime = 2f;
        float stateTimer;

        public MyconidSpawnState(MyconidStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateTimer = stateTime;
            //Trigger sprouting animation
            stateMachine.animator.SetTrigger("spawn");
        }

        public override void Exit()
        {
            //Trigger unearthing animation, enable colliders
            stateMachine.bodyCollider.enabled = true;
            stateMachine.mushroomCloud.SetupCloud(
                stateMachine.stats.cloudRange,
                stateMachine.stats.cloudHeal
            );
            stateMachine.mushroomCloud.ToggleCloud(true);
        }

        public override void Tick(float deltaTime)
        {
            stateTimer -= deltaTime;
            if (stateTimer <= 0f)
            {
                stateMachine.SwitchState(new MyconidChasingState(stateMachine));
                return;
            }
        }
    }
}
