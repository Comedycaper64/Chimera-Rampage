using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Elemental
{
    public abstract class ElementalBaseState : State
    {
        protected ElementalStateMachine stateMachine;

        public ElementalBaseState(ElementalStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
