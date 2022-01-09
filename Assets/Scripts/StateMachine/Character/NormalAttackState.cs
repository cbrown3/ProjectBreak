using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackState : IState<CharController>
{
    public NormalAttackState() { }

    Vector2 dirInput;

    int currentFrame;

    int attackFrames;

    int hitStunFrameDuration;

    public float isHeavy;

    bool inputComplete;

    bool isAerial;

    public override void Enter(CharController c)
    {
        currentFrame = 0;

        c.canAttack = false;

        //determine directional input
        dirInput = c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>();

        c.maxAerialSpeed = Mathf.Abs(c.rigid.velocity.x) + c.aerialDrift;

        if (c.isGrounded)
        {
            isAerial = false;
        }
        else
        {
            isAerial = true;
        }

        //determine attack type
        isHeavy = c.playerInput.actions.FindAction("Heavy Normal").ReadValue<float>();

        c.interuptible = false;

        if (isHeavy > 0)
        {
            inputComplete = false;
            c.animator.speed = 0;
        }
        else
        {
            inputComplete = true;
            c.animator.speed = 1;
        }

        //determine which attack animation to play, and length of attack
        if (dirInput == Vector2.zero)
        {
            if (c.isGrounded)
            {
                c.animator.Play(c.aNNeutralGroundAnim);
                attackFrames = c.nNeutralGFrames;
                c.NormalAttackGlow();
                hitStunFrameDuration = 15;
            }
            else
            {
                c.animator.Play(c.aNNeutralAerialAnim);
                attackFrames = c.nNeutralAFrames;
                c.NormalAttackGlow();
                hitStunFrameDuration = 15;
            }
        }
        else if (dirInput.x > 0)
        {
            c.GetComponent<SpriteRenderer>().flipX = false;

            if (c.isGrounded)
            {
                c.animator.Play(c.aNSideGroundAnim);
                attackFrames = c.nSideGFrames;
                c.NormalAttackGlow();
                hitStunFrameDuration = 15;
            }
            else
            {
                c.animator.Play(c.aNSideAerialAnim);
                attackFrames = c.nSideAFrames;
                c.NormalAttackGlow();
                hitStunFrameDuration = 15;
            }
        }
        else if (dirInput.x < 0)
        {
            c.GetComponent<SpriteRenderer>().flipX = true;

            if (c.isGrounded)
            {
                c.animator.Play(c.aNSideGroundAnim);
                attackFrames = c.nSideGFrames;
                c.NormalAttackGlow();
                hitStunFrameDuration = 15;
            }
            else
            {
                c.animator.Play(c.aNSideAerialAnim);
                attackFrames = c.nSideAFrames;
                c.NormalAttackGlow();
                hitStunFrameDuration = 15;
            }
        }
        else if (dirInput.y > 0)
        {
            if (c.isGrounded)
            {
                c.animator.Play(c.aNUpGroundAnim);
                attackFrames = c.nUpGFrames;
                c.NormalAttackGlow();
                hitStunFrameDuration = 15;
            }
            else
            {
                c.animator.Play(c.aNUpAerialAnim);
                attackFrames = c.nUpAFrames;
                c.NormalAttackGlow();
                hitStunFrameDuration = 15;
            }
        }
        else if (dirInput.y < 0)
        {
            if (c.isGrounded)
            {
                c.animator.Play(c.aNDownGroundAnim);
                attackFrames = c.nDownGFrames;
                c.NormalAttackGlow();
                hitStunFrameDuration = 15;
            }
            else
            {
                c.animator.Play(c.aNDownAerialAnim);
                attackFrames = c.nDownAFrames;
                c.NormalAttackGlow();
                hitStunFrameDuration = 15;
            }
        }

        if (c.isPlayer1)
        {
            CharManager.P1HitStunLength = hitStunFrameDuration;
        }
        else
        {
            CharManager.P2HitStunLength = hitStunFrameDuration;
        }

        //flip hitboxes appropriately
        if(c.GetComponent<SpriteRenderer>().flipX)
        {
            c.colliders.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            c.colliders.transform.rotation = Quaternion.identity;
        }
    }

    public override void Continue(CharController c)
    {
        if(!c.isGrounded)
        {
            c.moveInput = c.playerInput.actions.FindAction("Move").ReadValue<float>();

            c.rigid.AddForce(new Vector2(c.aerialDrift * c.moveInput, 0), ForceMode2D.Impulse);

            if (c.rigid.velocity.x > c.maxAerialSpeed)
            {
                c.rigid.velocity = new Vector2(c.maxAerialSpeed, c.rigid.velocity.y);
            }
            else if (c.rigid.velocity.x < -c.maxAerialSpeed)
            {
                c.rigid.velocity = new Vector2(-c.maxAerialSpeed, c.rigid.velocity.y);
            }
            /*else
            {
                c.rigid.velocity = new Vector2(c.aerialDrift * c.moveInput, c.rigid.velocity.y);
            }*/
        }
        else if(isAerial)
        {
            c.ResetGlow();
            c.interuptible = true;
            c.EnterState(c.idleState);
        }
        else
        {
            c.rigid.velocity = Vector2.zero;
        }

        //check if the move has been held long enough, if so complete the heavy attack, set heavy damage
        if(!inputComplete && c.playerInput.actions.FindAction("Heavy Normal").phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            c.animator.speed = 1;
            inputComplete = true;

            Debug.Log("Heavy Attack!");
        }
        //if not begin a light attack
        else if(!inputComplete && !c.playerInput.actions.FindAction("Heavy Normal").triggered &&
            c.playerInput.actions.FindAction("Heavy Normal").phase == UnityEngine.InputSystem.InputActionPhase.Waiting)
        {
            c.animator.speed = 1;
            inputComplete = true;

            Debug.Log("Light Attack!");
        }

        if (currentFrame > attackFrames)
        {
            c.ResetGlow();
            c.interuptible = true;

            if(c.isGrounded)
            {
                c.EnterState(c.idleState);
            }
            else
            {
                c.EnterState(c.fallState);
            }
        }

        if(inputComplete)
        {
            currentFrame++;
        }
    }

    public override void Exit(CharController c)
    {

    }
}
