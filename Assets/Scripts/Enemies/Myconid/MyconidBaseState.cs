using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Myconid
{
    public abstract class MyconidBaseState : State
    {
        protected MyconidStateMachine stateMachine;

        public MyconidBaseState(MyconidStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
