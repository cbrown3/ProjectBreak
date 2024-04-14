using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class ThrowState : IState<CharController>
    {
        public ThrowState()
        {

        }

        int frameCount;

        public override void Enter(CharController c)
        {
            c.StateType = StateType.Throw;

            frameCount = 0;

            c.animator.Play(c.aThrowAnim);

            c.canAttack = false;

            c.interuptible = false;

            c.canDash = false;
        }

        public override void Continue(CharController c)
        {

            //End of throw animation
            if (frameCount > c.throwFrameLength)
            {
                c.EnterState(StateType.Idle);
                return;
            }

            frameCount++;
        }

        public override void Exit(CharController c)
        {

        }
    }
}