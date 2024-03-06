using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightLogic
{
    public class ParryState : IState<CharController>
    {
        public ParryState()
        {

        }

        const int PARRY_LENGTH = 20;

        int frameCount;

        InputAction parryAction;

        public override void Enter(CharController c)
        {

            if (c.playerInput.actions.FindAction("Regular Parry").ReadValue<float>() > 0)
            {
                parryAction = c.playerInput.actions.FindAction("Regular Parry");

                c.StateType = StateType.RegularParry;
            }
            else if (c.playerInput.actions.FindAction("Normal Parry").ReadValue<float>() > 0)
            {
                parryAction = c.playerInput.actions.FindAction("Normal Parry");

                c.StateType = StateType.NormalParry;
            }
            else if (c.playerInput.actions.FindAction("Special Parry").ReadValue<float>() > 0)
            {
                parryAction = c.playerInput.actions.FindAction("Special Parry");

                c.StateType = StateType.SpecialParry;
            }
            else if (c.playerInput.actions.FindAction("Grab Parry").ReadValue<float>() > 0)
            {
                parryAction = c.playerInput.actions.FindAction("Grab Parry");

                c.StateType = StateType.GrabParry;
            }

            c.canDash = false;

            c.canAttack = false;

            c.animator.Play(c.aParryAnim);

            c.interuptible = false;

            frameCount = 0;
        }

        public override void Continue(CharController c)
        {
            if (frameCount >= PARRY_LENGTH)
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