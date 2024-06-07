using FixMath.NET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Volatile;

namespace FightLogic
{
    public class RunState : IState<CharController>
    {
        public RunState()
        {

        }

        public override void Enter(CharController c)
        {
            c.StateType = StateType.Run;

            c.ResetGlow();

            c.moveInput = Mathf.Round(c.playerInput.actions.FindAction("Move").ReadValue<float>());

            c.canDash = true;

            c.canAttack = true;

            c.interuptible = true;

            c.body.AddForce(new VoltVector2(c.groundSpeed * c.moveInput.ToFixed(), Fix64.Zero));

            c.animator.Play(c.aRunAnim);
        }

        public override void Continue(CharController c)
        {
            c.moveInput = Mathf.Round(c.playerInput.actions.FindAction("Move").ReadValue<float>());


            c.body.AddForce(new VoltVector2(c.groundSpeed * c.moveInput.ToFixed(), Fix64.Zero));

            if (c.body.LinearVelocity.x > c.groundSpeed)
            {
                c.body.LinearVelocity = new VoltVector2(c.groundSpeed, c.body.LinearVelocity.y);
            }
            else if (c.body.LinearVelocity.x < -c.groundSpeed)
            {
                c.body.LinearVelocity = new VoltVector2(-c.groundSpeed, c.body.LinearVelocity.y);
            }
            else if (c.moveInput == 0)
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