using System.Collections;
using System.Collections.Generic;
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

        Vector2 dirInput;

        int currentFrame;

        public override void Enter(CharController c)
        {
            grabAction = c.playerInput.actions.FindAction("Grab");

            currentFrame = 0;

            c.canAttack = false;

            c.canDash = false;

            //determine directional input
            dirInput = c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>();

            c.interuptible = false;

            c.animator.Play(c.aGrabAnim);
        }

        public override void Continue(CharController c)
        {
            if (currentFrame >= GRAB_LENGTH)
            {
                c.EnterState(c.idleState);
                return;
            }

            c.rigid.velocity = Vector2.zero;

            currentFrame++;
        }

        public override void Exit(CharController c)
        {

        }
    }
}