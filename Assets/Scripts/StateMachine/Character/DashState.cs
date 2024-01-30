using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightLogic
{
    public class DashState : IState<CharController>
    {
        public DashState()
        {

        }

        InputAction dashAction;

        Vector2 dashDir;

        int frameCount;

        int framesHeldCount;

        int additionalDashFrames;

        bool isP1RightSide;

        public override void Enter(CharController c)
        {
            c.StateType = StateType.Dash;

            if (c.playerInput.actions.FindAction("Dash").phase == InputActionPhase.Waiting)
            {
                //return;
            }

            //c.dashFrames = 12;
            frameCount = 0;
            framesHeldCount = 0;
            additionalDashFrames = 0;

            c.rigid.velocity = Vector2.zero;

            c.canDash = false;

            c.canAttack = false;
            //c.canDJump = false;

            c.interuptible = false;

            c.animator.Play(c.aDashAnim);

            dashDir = c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>();

            if (dashDir.x == 0)
            {
                c.EnterState(StateType.Idle);

                return;
            }
            else
            {
                c.playerData.Stamina--;
                dashDir.x = c.GetComponent<SpriteRenderer>().flipX ? -1 : 1;
            }

            isP1RightSide = CharManager.player1.transform.position.x - CharManager.player2.transform.position.x > 0 ? true : false;
        }

        public override void Continue(CharController c)
        {
            //Startup frames
            if (frameCount < c.dashStartup)
            {
                c.rigid.velocity = Vector2.zero;
                Physics2D.IgnoreLayerCollision(7, 8, true);

                //If dash is pressed again, cancel startup frames
                if (frameCount > 1 && c.playerData.Stamina >= 2 && c.playerInput.actions.FindAction("Dash").ReadValue<float>() > 0)
                {
                    c.playerData.Stamina -= 2;
                    frameCount = c.dashStartup - 1;
                }

                //if the same direction is being held, increase distance of dash
                if (dashDir.x == c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>().x)
                {
                    framesHeldCount++;
                }
            }
            //End of dash
            else if (frameCount > (c.dashFrameLength + c.dashStartup - 1 + additionalDashFrames + 5))
            {
                c.EnterState(StateType.Idle);

                return;
            }
            //Recovery frame test
            else if (frameCount > (c.dashFrameLength + c.dashStartup - 1 + additionalDashFrames))
            {
                c.rigid.velocity = Vector2.zero;

                if (CharManager.player1.transform.position.x - CharManager.player2.transform.position.x > 0 != isP1RightSide)
                {
                    c.GetComponent<SpriteRenderer>().flipX = !c.GetComponent<SpriteRenderer>().flipX;
                }
            }
            //Begin dashing
            else if (frameCount == c.dashStartup)
            {
                //Modify dash distance based on number of frames a direction was held
                if (framesHeldCount < c.dashStartup * 0.3334)
                {
                    additionalDashFrames = 0;
                }
                else if (framesHeldCount < c.dashStartup * 0.6667)
                {
                    additionalDashFrames = c.dashFrameLength / 4;
                }
                else
                {
                    additionalDashFrames = c.dashFrameLength / 2;
                }
            }
            //Dashing
            else
            {
                c.rigid.velocity = new Vector2(c.dashSpeed * dashDir.x, c.dashSpeed * dashDir.y);
            }

            frameCount++;
        }

        public override void Exit(CharController c)
        {
            Physics2D.IgnoreLayerCollision(7, 8, false);
        }
    }
}