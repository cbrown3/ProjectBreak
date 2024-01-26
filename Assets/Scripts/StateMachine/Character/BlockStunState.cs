using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class BlockStunState : IState<CharController>
    {
        public BlockStunState()
        {
            //stateType = StateType.HitStun;
        }

        public int CurrentBlockStunFrame { get => currentBlockStunFrame; set => currentBlockStunFrame = value; }

        int currentBlockStunFrame;

        public override void Enter(CharController c)
        {
            c.animator.Play(c.aHitStunAnim);

            c.canAttack = false;

            c.interuptible = false;

            c.canDash = false;
        }

        public override void Continue(CharController c)
        {
            currentBlockStunFrame--;

            if (currentBlockStunFrame == 0)
            {
                c.EnterState(c.idleState);

                return;
            }

            //Get opponent's current state, using type
            Type oppCurrState = c.isPlayer1 ? CharManager.player2.stateMachine.GetCurrentState().GetType() :
                CharManager.player1.stateMachine.GetCurrentState().GetType();

            //if the opponent's state is not attacking or idle, end hitstun and return to idle
            if ((oppCurrState != typeof(SpecialAttackState) && oppCurrState != typeof(NormalAttackState) &&
                oppCurrState != typeof(IdleState)))
            {
                currentBlockStunFrame = 0;
                c.EnterState(c.idleState);

                return;
            }
        }

        public override void Exit(CharController c)
        {

        }
    }
}