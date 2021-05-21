using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyNormalState : IState<CharController>
{
    static readonly HeavyNormalState instance =
       new HeavyNormalState();

    public static HeavyNormalState Instance
    {
        get
        {
            return instance;
        }
    }

    Vector2 dirInput;

    int frameRate;

    static HeavyNormalState() { }

    private HeavyNormalState() { }

    public override void Enter(CharController c)
    {
        frameRate = 0;

        c.canAttack = false;

        dirInput = c.charControls.Character.DirectionalInput.ReadValue<Vector2>();

        if (dirInput == Vector2.zero)
        {
            c.animator.Play(c.aHNNeutralGroundAnim);
        }
        else if(dirInput.x > 0)
        {
            c.GetComponent<SpriteRenderer>().flipX = false;
            c.animator.Play(c.aHNSideGroundAnim);
        }
        else if(dirInput.x < 0)
        {
            c.GetComponent<SpriteRenderer>().flipX = true;
            c.animator.Play(c.aHNSideGroundAnim);
        }
        else if(dirInput.y > 0)
        {
            c.animator.Play(c.aHNUpGroundAnim);
        }
        else if(dirInput.y < 0)
        {
            c.animator.Play(c.aHNDownGroundAnim);
        }

    }

    public override void Continue(CharController c)
    {
        if (dirInput == Vector2.zero)
        {
            if (frameRate > c.hNNeutralGFrames)
            {
                c.EnterState(IdleState.Instance);
            }
        }
        else if (dirInput.x != 0)
        {
            if (frameRate > c.hNSideGFrames)
            {
                c.EnterState(IdleState.Instance);
            }
        }
        else if (dirInput.y > 0)
        {
            if (frameRate > c.hNUpGFrames)
            {
                c.EnterState(IdleState.Instance);
            }
        }
        else if (dirInput.y < 0)
        {
            if (frameRate > c.hNDownGFrames)
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
