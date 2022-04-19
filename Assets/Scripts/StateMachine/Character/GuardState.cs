using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GuardState : IState<CharController>
{
    public GuardState()
    {
        stateType = StateType.Guard;
    }

    InputAction guardAction;

    float guardInput;

    public override void Enter(CharController c)
    {
        guardAction = c.playerInput.actions.FindAction("Guard");

        if (guardAction.phase == InputActionPhase.Waiting)
        {
            return;
        }

        c.canDash = false;

        c.canAttack = false;

        guardInput = guardAction.ReadValue<float>();

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
        guardInput = guardAction.ReadValue<float>();
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
