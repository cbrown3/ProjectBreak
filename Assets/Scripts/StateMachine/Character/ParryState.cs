using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParryState : IState<CharController>
{
    public ParryState()
    {
        
    }

    const int PARRY_LENGTH = 18;

    int frameCount;

    InputAction parryAction;

    public ParryType currParryType;

    public enum ParryType
    {
        None,
        RegularParry,
        NormalParry,
        SpecialParry,
        GrabParry
    }

    public override void Enter(CharController c)
    {
        currParryType = ParryType.None;

        if (c.playerInput.actions.FindAction("Regular Parry").ReadValue<float>() > 0)
        {
            parryAction = c.playerInput.actions.FindAction("Regular Parry");

            currParryType = ParryType.RegularParry;
        }
        else if (c.playerInput.actions.FindAction("Normal Parry").ReadValue<float>() > 0)
        {
            parryAction = c.playerInput.actions.FindAction("Normal Parry");

            currParryType = ParryType.NormalParry;
        }
        else if (c.playerInput.actions.FindAction("Special Parry").ReadValue<float>() > 0)
        {
            parryAction = c.playerInput.actions.FindAction("Special Parry");

            currParryType = ParryType.SpecialParry;
        }
        else if (c.playerInput.actions.FindAction("Grab Parry").ReadValue<float>() > 0)
        {
            parryAction = c.playerInput.actions.FindAction("Grab Parry");

            currParryType = ParryType.GrabParry;
        }
        else
        {
            Debug.Log("No parry detected, leaving parry state");

            c.RevertToPreviousState();

            return;
        }


        c.canDash = false;

        c.canAttack = false;

        //TODO: Update Animation
        c.animator.Play(c.aIdleAnim);

        c.interuptible = false;

        frameCount = 0;
    }

    public override void Continue(CharController c)
    {
        if(frameCount >= PARRY_LENGTH)
        {
            c.EnterState(c.idleState);
        }

        frameCount++;
    }

    public override void Exit(CharController c)
    {

    }
}
