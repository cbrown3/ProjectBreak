using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackState : IState<CharController>
{
    static readonly NormalAttackState instance =
       new NormalAttackState();

    public static NormalAttackState Instance
    {
        get
        {
            return instance;
        }
    }

    Vector2 dirInput;

    int currentFrame;

    int attackFrames;

    float isHeavy;

    static NormalAttackState() { }

    private NormalAttackState() { }

    public override void Enter(CharController c)
    {
        currentFrame = 0;

        c.canAttack = false;

        //determine directional input
        dirInput = c.charControls.Character.DirectionalInput.ReadValue<Vector2>();

        //determine attack type
        isHeavy = c.charControls.Character.HeavyNormal.ReadValue<float>();

        c.interuptible = false;

        //determine which attack animation to play, and length of attack
        if (dirInput == Vector2.zero)
        {
            if(c.isGrounded)
            {
                c.animator.Play(c.aNNeutralGroundAnim);
                attackFrames = c.nNeutralGFrames;
            }
            else
            {
                c.animator.Play(c.aNNeutralAerialAnim);
                attackFrames = c.nNeutralAFrames;
            }
        }
        else if (dirInput.x > 0)
        {
            c.GetComponent<SpriteRenderer>().flipX = false;

            if (c.isGrounded)
            {
                c.animator.Play(c.aNSideGroundAnim);
                attackFrames = c.nSideGFrames;
            }
            else
            {

            }
        }
        else if (dirInput.x < 0)
        {
            c.GetComponent<SpriteRenderer>().flipX = true;

            if (c.isGrounded)
            {
                c.animator.Play(c.aNSideGroundAnim);
                attackFrames = c.nSideGFrames;
            }
            else
            {

            }
        }
        else if (dirInput.y > 0)
        {
            if (c.isGrounded)
            {
                c.animator.Play(c.aNUpGroundAnim);
                attackFrames = c.nUpGFrames;
            }
            else
            {
                c.animator.Play(c.aNUpAerialAnim);
                attackFrames = c.nUpAFrames;
            }
        }
        else if (dirInput.y < 0)
        {
            if (c.isGrounded)
            {
                c.animator.Play(c.aNDownGroundAnim);
                attackFrames = c.nDownGFrames;
            }
            else
            {
                c.animator.Play(c.aNDownAerialAnim);
                attackFrames = c.nDownAFrames;
            }
        }

        //determine damage based on light/heavy
        if(isHeavy > 0)
        {
            
        }
        else
        {
            
        }
    }

    public override void Continue(CharController c)
    {
        if (currentFrame > attackFrames)
        {
            c.interuptible = true;
            c.EnterState(IdleState.Instance);
        }

        currentFrame++;
    }

    public override void Exit(CharController c)
    {

    }
}
