using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public bool isPlayer1;

    private bool attackSuccess;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " attack collided with " + collision.name);
        
        //If this is colliding with a Character...
        if (collision.gameObject.layer == 7)
        {
            CharController attacker = null;
            CharController defender = null;
            IState<CharController> attackerState = null;
            IState<CharController> defenderState = null;

            //set the attacker and defender correctly
            if (isPlayer1 && collision == CharManager.player2.playerCollider)
            {
                attacker = CharManager.player1;
                defender = CharManager.player2;
            }
            else if(!isPlayer1 && collision == CharManager.player1.playerCollider)
            {
                attacker = CharManager.player2;
                defender = CharManager.player1;
            }

            attackerState = attacker.stateMachine.GetCurrentState();
            defenderState = defender.stateMachine.GetCurrentState();

            if (attackerState != null && defenderState != null)
            {
                Type defCurrStateType = defenderState.GetType();

                //Dash is invincible to attacks, nothing happens
                if (defCurrStateType == typeof(DashState))
                {
                    attackSuccess = false;
                    return;
                }
                //If defender is in parry state...
                else if (defCurrStateType == typeof(ParryState))
                {
                    //check the type of parry and type of attack
                    ParryState.ParryType defParryType = ((ParryState)defenderState).currParryType;
                    Type attCurrStateType = attackerState.GetType();

                    //respond accordingly depending on parry type
                    if (defParryType == ParryState.ParryType.RegularParry)
                    {
                        Debug.Log("Regular Parry Successful!");

                        Debug.Log("TODO: SMALL PUSHBACK");
                        attacker.EnterState(attacker.idleState);
                        defender.EnterState(defender.idleState);

                        attackSuccess = false;
                    }
                    else if (defParryType == ParryState.ParryType.NormalParry &&
                        attCurrStateType.Name == "NormalAttackState")
                    {
                        Debug.Log("Normal Parry Successful!");
                        Debug.Log("TODO: DEFENDER PLUS FRAMES");

                        attackSuccess = false;
                    }
                    else if (defParryType == ParryState.ParryType.SpecialParry &&
                        attCurrStateType.Name == "SpecialAttackState")
                    {
                        Debug.Log("Special Parry Successful!");
                        Debug.Log("TODO: DEFENDER PLUS FRAMES");

                        attackSuccess = false;
                    }
                    else
                    {
                        Debug.Log("Parry Unsuccessful!");
                        attackSuccess = true;
                    }
                }
                //P2 enters block stun if guarding
                else if (defCurrStateType == typeof(GuardState))
                {
                    bool isNormalAttack = attackerState.GetType() == typeof(NormalAttackState);

                    defender.blockStunState.CurrentBlockStunFrame = isNormalAttack ? 30 : 5;

                    if (attacker.AttackHeight != defender.GuardHeight)
                    {
                        defender.Stamina--;
                    }

                    defender.Stamina -= attacker.CurrAttackValue;

                    defender.EnterState(defender.blockStunState);

                    attackSuccess = false;
                }
                //P2 enters hit stun otherwise
                else
                {
                    attackSuccess = true;
                }

                if (attackSuccess)
                {
                    //TODO: DETERMINE HITSTUN AMOUNT
                    defender.hitStunState.CurrentHitStunFrame = 12;

                    if (defCurrStateType == typeof(NormalAttackState) ||
                        defCurrStateType == typeof(SpecialAttackState))
                    {
                        defender.Health -= 2;
                        Debug.Log("COUNTER!");
                    }
                    defender.Health -= attacker.CurrAttackValue;
                    defender.EnterState(defender.hitStunState);
                }
            }
        }
    }
}
