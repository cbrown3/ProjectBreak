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

                IState<CharController> attackerState = attacker.stateMachine.GetCurrentState();
                IState<CharController> defenderState = defender.stateMachine.GetCurrentState();

                if (attackerState != null && defenderState != null)
                {
                    Type attCurrStateType = attackerState.GetType();

                    //check the type of parry and type of attack
                    ParryState.ParryType defParryType = ((ParryState)defenderState).currParryType;

                    //respond accordingly depending on parry type
                    if (defParryType == ParryState.ParryType.RegularParry)
                    {
                        Debug.Log("Regular Parry Successful!");

                        attacker.pushbackState.forceAmount = 2;
                        attacker.EnterState(attacker.pushbackState);
                        defender.pushbackState.forceAmount = 2;
                        defender.EnterState(defender.pushbackState);
                    }
                    else if (defParryType == ParryState.ParryType.NormalParry &&
                        attCurrStateType == typeof(NormalAttackState))
                    {
                        Debug.Log("Normal Parry Successful!");
                        Debug.Log("TODO: DEFENDER PLUS FRAMES");
                    }
                    else if (defParryType == ParryState.ParryType.SpecialParry &&
                        attCurrStateType == typeof(SpecialAttackState))
                    {
                        Debug.Log("Special Parry Successful!");
                        Debug.Log("TODO: DEFENDER PLUS FRAMES");
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