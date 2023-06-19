using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public abstract class ChimeraBaseState : State
    {
        protected ChimeraStateMachine stateMachine;

        public ChimeraBaseState(ChimeraStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void ChangeActiveHead(ChimeraHead head)
        {
            stateMachine.activeHead = head;

            stateMachine.dragonHeadAnimator.SetBool("dragonActive", false);
            stateMachine.lionHeadAnimator.SetBool("lionActive", false);
            stateMachine.goatHeadAnimator.SetBool("goatActive", false);

            switch (head)
            {
                case ChimeraHead.Dragon:
                    stateMachine.dragonHeadAnimator.SetBool("dragonActive", true);
                    break;
                case ChimeraHead.Lion:
                    stateMachine.lionHeadAnimator.SetBool("lionActive", true);
                    break;
                case ChimeraHead.Goat:
                    stateMachine.goatHeadAnimator.SetBool("goatActive", true);
                    break;
            }

            stateMachine.OnHeadChanged?.Invoke(this, head);
        }

        protected void SwitchToGoat()
        {
            stateMachine.SwitchState(new ChimeraGoatIdleState(stateMachine));
        }

        protected void SwitchToLion()
        {
            stateMachine.SwitchState(new ChimeraLionIdleState(stateMachine));
        }

        protected void SwitchToDragon()
        {
            stateMachine.SwitchState(new ChimeraDragonIdleState(stateMachine));
        }
    }
}
