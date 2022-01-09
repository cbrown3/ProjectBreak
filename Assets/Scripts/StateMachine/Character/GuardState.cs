using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardState : IState<CharController>
{
    public GuardState() { }

    float guardInput;

    public override void Enter(CharController c)
    {
        c.canDash = false;

        c.canAttack = false;

        guardInput = c.playerInput.actions.FindAction("Guard").ReadValue<float>();

        if (c.isGrounded)
        {
            c.animator.Play(c.aGroundGuardAnim);
        }
        else
        {
            c.animator.Play(c.aAirGuardAnim);
        }

        c.interuptible = false;
    }

    public override void Continue(CharController c)
    {
        guardInput = c.playerInput.actions.FindAction("Guard").ReadValue<float>();
        c.moveInput = c.playerInput.actions.FindAction("Move").ReadValue<float>();

        if (c.isGrounded)
        {
            c.rigid.velocity = Vector2.zero;

            if (c.animator.GetCurrentAnimatorStateInfo(0).IsName(c.aAirGuardAnim))
            {
                c.animator.Play(c.aGroundGuardAnim);
            }
        }


        if (c.moveInput > 0)
        {
            c.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(c.moveInput < 0)
        {
            c.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (!c.GetComponent<SpriteRenderer>().flipX)
        {
            c.colliders.transform.rotation = Quaternion.identity;
        }
        else
        {
            c.colliders.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        
        if (guardInput <= 0)
        {
            c.EnterState(c.idleState);
        }
    }

    public override void Exit(CharController c)
    {
        
    }
}
