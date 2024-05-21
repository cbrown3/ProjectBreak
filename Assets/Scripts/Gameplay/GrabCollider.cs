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

                if (attacker.StateType != StateType.None && defender.StateType != StateType.None)
                {
                    //respond accordingly depending on state
                    if (defender.StateType == StateType.GrabParry)
                    {
                        Debug.Log("Grab Parry Successful!");

                        Debug.Log("TODO: SMALL PUSHBACK");
                        attacker.EnterState(StateType.Idle);
                        defender.EnterState(StateType.Idle);
                    }
                    else
                    {
                        if (defender.StateType == StateType.NormalParry || defender.StateType == StateType.RegularParry ||
                        defender.StateType == StateType.SpecialParry)
                        {
                            Debug.Log("Parry Unsuccessful!");
                        }

                        defender.playerData.Health -= attacker.CurrGrabValue;
                        attacker.EnterState(StateType.Throw);

                        //put the defender exactly in front of the attacker
                        float offsetX = FightLogicUtility.IsPlayerOnRightSide(attacker.isPlayer1) ? -0.65f : 0.65f;

                        defender.transform.position = FightLogicUtility.PlayerPositionOffset(attacker.transform.position, offsetX, 0);

                        defender.EnterState(StateType.Thrown);
                    }
                }
            }
        }
    }
}