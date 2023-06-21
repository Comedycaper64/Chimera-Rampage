using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Myconid
{
    public class MyconidChasingState : MyconidBaseState
    {
        public MyconidChasingState(MyconidStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            //Enable chasing bool in animator
            stateMachine.animator.SetBool("chasing", true);
        }

        public override void Exit()
        {
            //Disable chasing bool in animator
            stateMachine.animator.SetBool("chasing", false);
        }

        public override void Tick(float deltaTime)
        {
            Vector3 directionToMove = (
                stateMachine.playerHealth.transform.position - stateMachine.transform.position
            ).normalized;
            directionToMove.z = 0f;
            stateMachine.transform.position +=
                directionToMove * stateMachine.stats.movementSpeed * deltaTime;
        }
    }
}
