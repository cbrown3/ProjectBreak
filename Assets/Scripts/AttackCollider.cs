using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public bool isPlayer1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " attack collided with " + collision.name);
        
        //If this is colliding with a Character...
        if (collision.gameObject.layer == 7)
        {
            IState<CharController> p1State = CharManager.player1.stateMachine.GetCurrentState();
            IState<CharController> p2State = CharManager.player2.stateMachine.GetCurrentState();

            //...and it is P1's attack colliding with P2
            if (isPlayer1 && collision == CharManager.player2.playerCollider)
            {
                Type currStateType = p2State.GetType();

                //Dash is invincible to attacks, nothing happens
                if (currStateType == typeof(DashState))
                {
                    return;
                }

                //P2 enters block stun if guarding
                if (currStateType == typeof(GuardState))
                {
                    bool isNormalAttack = p1State.GetType() == typeof(NormalAttackState);
                    CharManager.player2.blockStunState.CurrentBlockStunFrame = isNormalAttack ? 30 : 5;

                    if(CharManager.player1.AttackHeight != CharManager.player2.GuardHeight)
                    {
                        CharManager.player2.Stamina--;
                    }

                    CharManager.player2.Stamina -= CharManager.player1.CurrAttackValue;
                    
                    CharManager.player2.EnterState("BlockStun");
                }
                //P2 enters hit stun otherwise
                else
                {
                    CharManager.player2.hitStunState.CurrentHitStunFrame = 12;

                    if(currStateType == typeof(NormalAttackState) ||
                        currStateType == typeof(SpecialAttackState))
                    {
                        CharManager.player2.Health -= 2;
                        Debug.Log("COUNTER!");
                    }
                    CharManager.player2.Health -= CharManager.player1.CurrAttackValue;
                    CharManager.player2.EnterState("HitStun");
                }
            }
            //...and it is Player 2's attack colliding with Player 1
            else if (!isPlayer1 && collision == CharManager.player1.playerCollider)
            {
                Type currStateType = p1State.GetType();

                //Dash is invincible to attacks, nothing happens
                if (currStateType == typeof(DashState))
                {
                    return;
                }

                //P1 enters block stun if guarding
                if (currStateType == typeof(GuardState))
                {
                    bool isNormalAttack = p2State.GetType() == typeof(NormalAttackState);
                    CharManager.player1.blockStunState.CurrentBlockStunFrame = isNormalAttack ? 30 : 5;

                    if (CharManager.player2.AttackHeight != CharManager.player1.GuardHeight)
                    {
                        CharManager.player1.Stamina--;
                    }


                    CharManager.player1.Stamina -= CharManager.player2.CurrAttackValue;
                    CharManager.player1.EnterState("BlockStun");
                }
                //P1 enters hit stun otherwise
                else
                {
                    CharManager.player1.hitStunState.CurrentHitStunFrame = 12;

                    if (currStateType == typeof(NormalAttackState) ||
                        currStateType == typeof(SpecialAttackState))
                    {
                        CharManager.player1.Health -= 2;
                        Debug.Log("COUNTER!");
                    }
                    CharManager.player1.Health -= CharManager.player2.CurrAttackValue;
                    CharManager.player1.EnterState("HitStun");
                }
            }
        }
    }
}
