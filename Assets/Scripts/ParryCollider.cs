using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class ParryCollider : MonoBehaviour
    {
        public bool isPlayer1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(gameObject.name + " parry collided with " + collision.name);

            //If this is colliding with an Attack Collider...
            if (collision.gameObject.layer == 9)
            {
                CharController attacker = null;
                CharController defender = null;

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
                    //respond accordingly depending on state
                    if (defender.StateType == StateType.RegularParry)
                    {
                        Debug.Log("Regular Parry Successful!");

                        attacker.pushbackState.forceAmount = 2;
                        attacker.EnterState(StateType.Pushback);
                        defender.pushbackState.forceAmount = 2;
                        defender.EnterState(StateType.Pushback);
                    }
                    else if (defender.StateType == StateType.NormalParry &&
                        attacker.StateType == StateType.NormalAttack)
                    {
                        Debug.Log("Normal Parry Successful!");
                        attacker.hitStunState.CurrentHitStunFrame = 10;
                        attacker.EnterState(StateType.HitStun);
                        defender.EnterState(StateType.Idle);
                    }
                    else if (defender.StateType == StateType.SpecialParry &&
                        attacker.StateType == StateType.SpecialAttack)
                    {
                        Debug.Log("Special Parry Successful!");
                        attacker.hitStunState.CurrentHitStunFrame = 10;
                        attacker.EnterState(StateType.HitStun);
                        defender.EnterState(StateType.Idle);
                    }
                    else
                    {
                        Debug.Log("Parry Unsuccessful!");
                    }
                }
            }
        }
    }
}