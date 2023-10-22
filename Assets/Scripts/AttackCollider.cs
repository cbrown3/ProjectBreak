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
            attackSuccess = false;

            //set the attacker and defender correctly
            if (isPlayer1 && collision == CharManager.player2.playerCollider)
            {
                attacker = CharManager.player1;
                defender = CharManager.player2;
            }
            else if (!isPlayer1 && collision == CharManager.player1.playerCollider)
            {
                attacker = CharManager.player2;
                defender = CharManager.player1;
            }
            else
            {
                Debug.Log(gameObject.name + " missed!");
                return;
            }

            attackerState = attacker.stateMachine.GetCurrentState();
            defenderState = defender.stateMachine.GetCurrentState();

            if (attackerState != null && defenderState != null)
            {
                Type defCurrStateType = defenderState.GetType();
                Type attCurrStateType = attackerState.GetType();

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
                        attCurrStateType == typeof(NormalAttackState))
                    {
                        Debug.Log("Normal Parry Successful!");
                        Debug.Log("TODO: DEFENDER PLUS FRAMES");

                        attackSuccess = false;
                    }
                    else if (defParryType == ParryState.ParryType.SpecialParry &&
                        attCurrStateType == typeof(SpecialAttackState))
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

                    int staminaVal = attacker.CurrAttackValue;

                    //add additional stamina penalty if incorrect guard height
                    if (attacker.AttackHeight != defender.GuardHeight)
                    {
                        if(defender.Stamina > 0)
                        {
                            staminaVal++;
                        }
                    }

                    //if defender has enough stamina, pass it to attacker
                    if (defender.Stamina >= staminaVal)
                    {
                        defender.Stamina -= staminaVal;
                        attacker.Stamina += staminaVal;
                    }
                    //if not enough, pass what is left to attacker and guard break defender
                    else
                    {
                        //TODO: GUARD BREAK
                        attacker.Stamina += defender.Stamina;
                        defender.Stamina = 0;
                    }

                    defender.EnterState(defender.blockStunState);

                    attackSuccess = false;

                    //allow for the player to attack cancel
                    if (isNormalAttack)
                    {
                        attacker.canAttack = true;
                    }
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

                    Debug.Log("Attack landed!");

                    if (defCurrStateType == typeof(NormalAttackState) ||
                        defCurrStateType == typeof(SpecialAttackState))
                    {
                        defender.Health -= 2;
                        Debug.Log("COUNTER!");
                    }
                    defender.Health -= attacker.CurrAttackValue;
                    defender.EnterState(defender.hitStunState);

                    //allow for the player to attack cancel
                    if (attCurrStateType == typeof(NormalAttackState))
                    {
                        attacker.canAttack = true;
                    }
                }
            }
        }
    }
}
