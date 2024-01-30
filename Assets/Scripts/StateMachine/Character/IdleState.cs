using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class IdleState : IState<CharController>
    {
        public IdleState()
        {

        }

        public override void Enter(CharController c)
        {
            c.StateType = StateType.Idle;

            c.rigid.velocity = new Vector2(0, c.rigid.velocity.y);

            c.canDash = true;

            c.canAttack = true;

            c.animator.Play(c.aIdleAnim);

            c.interuptible = true;
        }

        public override void Continue(CharController c)
        {
            /*c.moveInput = c.playerInput.actions.FindAction("Move").ReadValue<float>();

            if (c.moveInput != 0)
            {
                c.EnterState(c.runState);
            }
            else if(c.jumpInput != 0)
            {
                c.EnterState(c.jumpState);
            }
            */
        }

        public override void Exit(CharController c)
        {

        }
    }
}