using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class SoftKnockdownState : IState<CharController>
    {
        public SoftKnockdownState()
        {

        }

        public override void Enter(CharController c)
        {
            c.StateType = StateType.SoftKnockdown;

            c.animator.Play(c.aSoftKnockdownAnim);

            c.canAttack = false;

            c.interuptible = false;

            c.canDash = false;
        }

        public override void Continue(CharController c)
        {
            if (c.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                c.EnterState(StateType.Idle);

                return;
            }
        }

        public override void Exit(CharController c)
        {

        }
    }
}