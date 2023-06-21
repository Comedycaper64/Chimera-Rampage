using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Dryad
{
    public abstract class DryadBaseState : State
    {
        protected DryadStateMachine stateMachine;

        public DryadBaseState(DryadStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
