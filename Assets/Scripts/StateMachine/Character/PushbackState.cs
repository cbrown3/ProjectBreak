using FixMath.NET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Volatile;

namespace FightLogic
{
    public class PushbackState : IState<CharController>
    {

        public PushbackState()
        {

        }

        public int forceAmount;
        int frameCount;

        public override void Enter(CharController c)
        {
            c.StateType = StateType.Pushback;

            frameCount = 0;

            c.body.LinearVelocity = VoltVector2.zero;

            c.canDash = false;

            c.canAttack = false;

            c.interuptible = false;

            //c.animator.Play(c.aDashAnim);
        }

        public override void Continue(CharController c)
        {

            //End of pushback
            if (frameCount > c.pushbackFrameLength)
            {
                forceAmount = 0;
                c.EnterState(StateType.Idle);
                return;
            }
            //Pushback
            else
            {
                Fix64 direction = c.GetComponent<SpriteRenderer>().flipX ? Fix64.One : new Fix64(-1);
                c.body.AddForce(new VoltVector2(direction * new Fix64(forceAmount), Fix64.Zero));
            }

            frameCount++;
        }

        public override void Exit(CharController c)
        {

        }
    }
}