using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GuardState : IState<CharController>
{
    public GuardState()
    {
        
    }

    InputAction guardAction;

    float guardInput;

    float guardDir = 0;


    public override void Enter(CharController c)
    {
        guardAction = c.playerInput.actions.FindAction("Guard");

        if (guardAction.phase == InputActionPhase.Waiting)
        {
            return;
        }

        c.canDash = true;

        c.canAttack = false;

        guardInput = guardAction.ReadValue<float>();

        c.animator.Play(c.aGroundGuardAnim);

        c.interuptible = false;

        guardDir = c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>().y;

        if(guardDir == 0) 
        {
            c.GuardHeight = CharController.Height.Mid;
        }
        else
        {
            c.GuardHeight = guardDir > 0 ? CharController.Height.High : CharController.Height.Low;
        }
    }

    public override void Continue(CharController c)
    {
        guardInput = guardAction.ReadValue<float>();
        c.moveInput = c.playerInput.actions.FindAction("Move").ReadValue<float>();
        guardDir = c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>().y;

        if (guardDir == 0)
        {
            c.GuardHeight = CharController.Height.Mid;
        }
        else
        {
            c.GuardHeight = guardDir > 0 ? CharController.Height.High : CharController.Height.Low;
        }

        c.rigid.velocity = Vector2.zero;

        if (c.animator.GetCurrentAnimatorStateInfo(0).IsName(c.aAirGuardAnim))
        {
            c.animator.Play(c.aGroundGuardAnim);
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

        if (c.playerInput.actions.FindAction("Regular Parry").ReadValue<float>() > 0)
        {
            c.parryState.currParryType = ParryState.ParryType.RegularParry;
            c.EnterState(c.parryState);

            return;
        }
        else if (c.playerInput.actions.FindAction("Light Normal").ReadValue<float>() > 0)
        {
            c.parryState.currParryType = ParryState.ParryType.NormalParry;
            c.EnterState(c.parryState);

            return;
        }
        else if (c.playerInput.actions.FindAction("Light Special").ReadValue<float>() > 0)
        {
            c.parryState.currParryType = ParryState.ParryType.SpecialParry;
            c.EnterState(c.parryState);

            return;
        }
        else if (c.playerInput.actions.FindAction("Grab Parry").ReadValue<float>() > 0)
        {
            c.parryState.currParryType = ParryState.ParryType.GrabParry;
            c.EnterState(c.parryState);

            return;
        }

        if (guardInput <= 0)
        {
            c.EnterState(c.idleState);

            return;
        }
    }

    public override void Exit(CharController c)
    {
        c.GuardHeight = CharController.Height.None;
    }
}
