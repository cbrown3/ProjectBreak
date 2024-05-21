using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class ThrownState : IState<CharController>
    {
        public ThrownState()
        {

        }

        public override void Enter(CharController c)
        {
            c.StateType = StateType.Thrown;

            c.animator.Play(c.aThrownAnim);

            c.canAttack = false;

            c.interuptible = false;

            c.canDash = false;
        }

        public override void Continue(CharController c)
        {
            if (c.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                c.EnterState(StateType.HardKnockdown);

                return;
            }
        }

        public override void Exit(CharController c)
        {

        }
    }
}