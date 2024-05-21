using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class HardKnockdownState : IState<CharController>
    {
        public HardKnockdownState()
        {

        }

        public override void Enter(CharController c)
        {
            c.StateType = StateType.HardKnockdown;

            c.animator.Play(c.aHardKnockdownAnim);

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