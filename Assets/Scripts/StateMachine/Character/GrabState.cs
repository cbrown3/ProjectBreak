using System.Collections;
using System.Collections.Generic;
using Volatile;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightLogic
{
    public class GrabState : IState<CharController>
    {
        public GrabState()
        {

        }

        const int GRAB_LENGTH = 20;

        InputAction grabAction;

        VoltVector2 dirInput;

        int currentFrame;

        public override void Enter(CharController c)
        {
            c.StateType = StateType.Grab;

            grabAction = c.playerInput.actions.FindAction("Grab");

            currentFrame = 0;

            c.canAttack = false;

            c.canDash = false;

            //determine directional input
            dirInput = c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>().ToFixed();

            c.interuptible = false;

            c.animator.Play(c.aGrabAnim);
        }

        public override void Continue(CharController c)
        {
            if (currentFrame >= GRAB_LENGTH)
            {
                c.EnterState(StateType.Idle);
                return;
            }

            c.body.LinearVelocity = VoltVector2.zero;

            currentFrame++;
        }

        public override void Exit(CharController c)
        {

        }
    }
}