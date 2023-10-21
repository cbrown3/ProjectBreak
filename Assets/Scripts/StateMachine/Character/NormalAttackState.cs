using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NormalAttackState : IState<CharController>
{
    public NormalAttackState()
    {
        stopAttack = false;
    }

    InputAction heavyNormal;

    Vector2 dirInput;

    static Vector2 pastDirInput;

    static bool isAttackCancel; 

    int currentFrame;

    int attackFrames;

    public float isHeavy;

    bool inputComplete;

    bool stopAttack;

    public override void Enter(CharController c)
    {
        heavyNormal = c.playerInput.actions.FindAction("Heavy Normal");

        //determine attack type
        isHeavy = heavyNormal.ReadValue<float>();

        if (heavyNormal.phase == InputActionPhase.Waiting && isHeavy == default)
        {
            return;
        }

        currentFrame = 0;

        c.canAttack = false;

        c.canDash = true;

        //determine directional input
        dirInput = c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>();

        if (isAttackCancel && pastDirInput == dirInput)
        {
            isAttackCancel = false;
            c.canAttack = true;
            c.EnterState(c.idleState);
        }

        c.interuptible = false;

        if (isHeavy > 0)
        {
            inputComplete = false;
            c.animator.speed = 0;
            stopAttack = true;
        }
        else if(!stopAttack)
        {
            inputComplete = true;
            c.animator.speed = 1;
        }

        //determine which attack animation to play, length of attack, and attack height
        if (dirInput == Vector2.zero)
        {
            c.AttackHeight = 1;
            c.animator.Play(c.aNNeutralGroundAnim);
            attackFrames = c.nNeutralGFrames;
            c.NormalAttackGlow();
        }
        else if (dirInput.x > 0)
        {
            c.AttackHeight = 1;
            c.animator.Play(c.aNSideGroundAnim);
            attackFrames = c.nSideGFrames;
            c.NormalAttackGlow();
        }
        else if (dirInput.x < 0)
        {
            c.AttackHeight = 1;
            c.animator.Play(c.aNSideGroundAnim);
            attackFrames = c.nSideGFrames;
            c.NormalAttackGlow();
        }
        else if (dirInput.y > 0)
        {
            c.AttackHeight = 2;
            c.animator.Play(c.aNUpGroundAnim);
            attackFrames = c.nUpGFrames;
            c.NormalAttackGlow();
        }
        else if (dirInput.y < 0)
        {
            c.AttackHeight = 0;
            c.animator.Play(c.aNDownGroundAnim);
            attackFrames = c.nDownGFrames;
            c.NormalAttackGlow();
        }
    }

    public override void Continue(CharController c)
    {
        c.rigid.velocity = Vector2.zero;

        //check if the move has been held long enough, if so complete the heavy attack, set heavy damage
        if (!inputComplete && heavyNormal.phase == InputActionPhase.Performed)
        {
            c.animator.speed = 1;
            inputComplete = true;

            Debug.Log("Heavy Attack!");
            c.CurrAttackValue = 2;
        }
        //if not begin a light attack
        else if (!inputComplete && !heavyNormal.triggered && heavyNormal.phase == InputActionPhase.Waiting)
        {
            c.animator.speed = 1;
            inputComplete = true;

            Debug.Log("Light Attack!");
            c.CurrAttackValue = 1;
        }

        if (inputComplete && c.colliders.GetComponentsInChildren<Transform>().GetLength(0) == 2)
        {
            c.canAttack = true;
            
            pastDirInput = dirInput;

            isAttackCancel = true;
        }
        else
        {
            c.canAttack = false;

            pastDirInput = Vector2.zero;

            isAttackCancel = false;
        }
        
        if (currentFrame > attackFrames)
        {
            c.ResetGlow();
            c.interuptible = true;

            c.EnterState(c.idleState);
        }

        if (inputComplete)
        {
            currentFrame++;
        }
    }

    public override void Exit(CharController c)
    {
        c.CurrAttackValue = 0;
        c.AttackHeight = -1;
    }
}
