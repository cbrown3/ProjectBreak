using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class GrabCollider : MonoBehaviour
    {
        public bool isPlayer1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(gameObject.name + " grab collided with " + collision.name);

            //If this is colliding with a Character...
            if (collision.gameObject.layer == 7)
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
                    Type defCurrStateType = defenderState.GetType();

                    //If defender is in parry state...
                    if (defCurrStateType == typeof(ParryState))
                    {
                        //check the type of parry and type of attack
                        ParryState.ParryType defParryType = ((ParryState)defenderState).currParryType;
                        Type attCurrStateType = attackerState.GetType();

                        //respond accordingly depending on parry type
                        if (defParryType == ParryState.ParryType.GrabParry)
                        {
                            Debug.Log("Grab Parry Successful!");

                            Debug.Log("TODO: SMALL PUSHBACK");
                            attacker.EnterState(attacker.idleState);
                            defender.EnterState(defender.idleState);
                        }
                        else
                        {
                            Debug.Log("Parry Unsuccessful!");
                        }
                    }
                    //defender enters thrown state otherwise
                    else
                    {
                        defender.playerData.Health -= attacker.CurrGrabValue;
                        defender.EnterState(defender.thrownState);
                    }
                }
            }
        }
    }
}