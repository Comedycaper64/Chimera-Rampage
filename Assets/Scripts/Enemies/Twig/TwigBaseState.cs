using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Twig
{
    public abstract class TwigBaseState : State
    {
        protected TwigStateMachine stateMachine;

        public TwigBaseState(TwigStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
