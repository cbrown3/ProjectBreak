using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightNormalState : IState<CharController>
{
    static readonly LightNormalState instance =
       new LightNormalState();

    public static LightNormalState Instance
    {
        get
        {
            return instance;
        }
    }

    Vector2 dirInput;

    int frameRate;

    static LightNormalState() { }

    private LightNormalState() { }

    public override void Enter(CharController c)
    {
        frameRate = 0;

        c.canAttack = false;

        dirInput = c.charControls.Character.DirectionalInput.ReadValue<Vector2>();

        if (dirInput == Vector2.zero)
        {
            c.animator.Play(c.aLNNeutralGroundAnim);
        }
        else if (dirInput.x > 0)
        {
            c.GetComponent<SpriteRenderer>().flipX = false;
            c.animator.Play(c.aLNSideGroundAnim);
        }
        else if (dirInput.x < 0)
        {
            c.GetComponent<SpriteRenderer>().flipX = true;
            c.animator.Play(c.aLNSideGroundAnim);
        }
        else if (dirInput.y > 0)
        {
            c.animator.Play(c.aLNUpGroundAnim);
        }
        else if (dirInput.y < 0)
        {
            c.animator.Play(c.aLNDownGroundAnim);
        }
    }

    public override void Continue(CharController c)
    {
        if (dirInput == Vector2.zero)
        {
            if (frameRate > c.lNNeutralGFrames)
            {
                c.EnterState(IdleState.Instance);
            }
        }
        else if (dirInput.x != 0)
        {
            if (frameRate > c.lNSideGFrames)
            {
                c.EnterState(IdleState.Instance);
            }
        }
        else if (dirInput.y > 0)
        {
            if (frameRate > c.lNUpGFrames)
            {
                c.EnterState(IdleState.Instance);
            }
        }
        else if (dirInput.y < 0)
        {
            if (frameRate > c.lNDownGFrames)
            {
                c.EnterState(IdleState.Instance);
            }
        }

        frameRate++;
    }

    public override void Exit(CharController c)
    {

    }
}
