using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Elemental
{
    public class ElementalDeathState : ElementalBaseState
    {
        public ElementalDeathState(ElementalStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.animator.SetTrigger("death");
            stateMachine.isDead = true;
            stateMachine.bodyCollider.enabled = false;
            DialogueManager.Instance.StartConversation(stateMachine.endOfGameDialogue);
        }

        public override void Exit() { }

        public override void Tick(float deltaTime) { }
    }
}
