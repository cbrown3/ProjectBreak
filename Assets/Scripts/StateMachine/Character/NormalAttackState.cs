using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NormalAttackState : IState<CharController>
{
    public NormalAttackState()
    {
        stopAttack = false;
        attackString = new List<Vector2>();
    }

    InputAction heavyNormal;

    Vector2 dirInput;

    List<Vector2> attackString;

    int currentActiveFrame;

    int attackFrames;

    float isHeavy;

    bool inputComplete;

    bool stopAttack;

    public override void Enter(CharController c)
    {
        //determine directional input
        dirInput = c.playerInput.actions.FindAction("DirectionalInput").ReadValue<Vector2>();

        if (!c.canAttackCancel || attackString.Contains(dirInput))
        {
            Debug.Log("Cannot attack cancel");
            Exit(c);
        }
        else
        {
            attackString.Add(dirInput);
            Debug.Log(attackString.Count + " HIT COMBO!");
        }

        heavyNormal = c.playerInput.actions.FindAction("Heavy Normal");

        //determine attack type
        isHeavy = heavyNormal.ReadValue<float>();

        if (heavyNormal.phase == InputActionPhase.Waiting && isHeavy == default)
        {
            return;
        }

        currentActiveFrame = 0;

        c.canAttack = false;

        c.canAttackCancel = false;

        c.canDash = true;

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
            c.AttackHeight = CharController.Height.Mid;
            c.animator.Play(c.aNNeutralGroundAnim);
            attackFrames = c.nNeutralGFrames;
            c.NormalAttackGlow();
        }
        else if (dirInput.x > 0)
        {
            c.AttackHeight = CharController.Height.Mid;
            c.animator.Play(c.aNSideGroundAnim);
            attackFrames = c.nSideGFrames;
            c.NormalAttackGlow();
        }
        else if (dirInput.x < 0)
        {
            c.AttackHeight = CharController.Height.Mid;
            c.animator.Play(c.aNSideGroundAnim);
            attackFrames = c.nSideGFrames;
            c.NormalAttackGlow();
        }
        else if (dirInput.y > 0)
        {
            c.AttackHeight = CharController.Height.High;
            c.animator.Play(c.aNUpGroundAnim);
            attackFrames = c.nUpGFrames;
            c.NormalAttackGlow();
        }
        else if (dirInput.y < 0)
        {
            c.AttackHeight = CharController.Height.Low;
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

        if (inputComplete && c.canAttack)
        {
            attackString.Add(dirInput);

            c.canAttackCancel = true;
        }
        else if (inputComplete)
        {
            attackString.Clear();
        }
        
        if (currentActiveFrame > attackFrames)
        {
            c.ResetGlow();
            c.interuptible = true;
            c.canAttackCancel = false;

            Debug.Log("End of attack.");
            c.EnterState(c.idleState);

            return;
        }

        if (inputComplete)
        {
            currentActiveFrame++;
        }
    }

    public override void Exit(CharController c)
    {
        c.CurrAttackValue = 0;
        c.AttackHeight = CharController.Height.None;
    }
}
