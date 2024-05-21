using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FightLogic;

namespace FightLogic
{
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

                if (attacker.StateType != StateType.None && defender.StateType != StateType.None)
                {
                    //Dash is invincible to attacks, nothing happens
                    if (defender.StateType == StateType.Dash)
                    {
                        attackSuccess = false;
                        return;
                    }
                    else if (defender.StateType == StateType.RegularParry)
                    {
                        attackSuccess = false;
                    }
                    else if (defender.StateType == StateType.NormalParry 
                        && attacker.StateType == StateType.NormalAttack)
                    {
                        attackSuccess = false;
                    }
                    else if (defender.StateType == StateType.SpecialParry
                        && attacker.StateType == StateType.SpecialAttack)
                    {
                        attackSuccess = false;
                    }
                    //P2 enters block stun if guarding
                    else if (defender.StateType == StateType.Guard)
                    {
                        defender.blockStunState.CurrentBlockStunFrame = attacker.StateType == StateType.NormalAttack ? 30 : 5;

                        int staminaVal = attacker.CurrAttackValue;

                        //add additional stamina penalty if incorrect guard height
                        if (attacker.AttackHeight != defender.GuardHeight)
                        {
                            if (defender.playerData.Stamina > 0)
                            {
                                staminaVal++;
                            }
                        }

                        //if defender has enough stamina, pass it to attacker
                        if (defender.playerData.Stamina >= staminaVal)
                        {
                            defender.playerData.Stamina -= staminaVal;
                            attacker.playerData.Stamina += staminaVal;
                        }
                        //if not enough, pass what is left to attacker and guard break defender
                        else
                        {
                            attacker.playerData.Stamina += defender.playerData.Stamina;
                            defender.playerData.Stamina = 0;

                            defender.hitStunState.CurrentHitStunFrame = 30;
                            defender.EnterState(StateType.HitStun);
                            defender.playerData.Stamina = 10;

                            attackSuccess = false;

                            return;
                        }

                        defender.EnterState(StateType.BlockStun);

                        attackSuccess = false;

                        //allow for the player to attack cancel
                        if (attacker.StateType == StateType.NormalAttack)
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

                        if (defender.StateType == StateType.NormalAttack ||
                            defender.StateType == StateType.SpecialAttack)
                        {
                            defender.playerData.Health -= 2;
                            Debug.Log("COUNTER!");
                        }
                        defender.playerData.Health -= attacker.CurrAttackValue;

                        CinemachineShake.Instance.ShakeCamera(5, 0.1f);

                        defender.EnterState(StateType.HitStun);

                        //allow for the player to attack cancel
                        if (attacker.StateType == StateType.NormalAttack)
                        {
                            attacker.canAttack = true;
                        }
                    }
                }
            }
        }
    }
}
