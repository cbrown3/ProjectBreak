using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class ThrownState : IState<CharController>
    {
        public ThrownState()
        {

        }

        public int CurrentHitStunFrame { get => currentHitStunFrame; set => currentHitStunFrame = value; }

        int currentHitStunFrame;

        public override void Enter(CharController c)
        {
            c.StateType = StateType.Thrown;

            c.animator.Play(c.aHitStunAnim);

            c.canAttack = false;

            c.interuptible = false;

            c.canDash = false;
        }

        public override void Continue(CharController c)
        {
            currentHitStunFrame--;

            if (currentHitStunFrame == 0)
            {
                c.EnterState(StateType.Idle);

                return;
            }

            //Get opponent's current state, using type
            StateType oppCurrState = c.isPlayer1 ? CharManager.player2.StateType : CharManager.player1.StateType;

            //if the opponent's state is not attacking or idle, end hitstun and return to idle
            if (oppCurrState != StateType.SpecialAttack && oppCurrState != StateType.NormalAttack &&
                oppCurrState != StateType.Idle)
            {
                currentHitStunFrame = 0;
                c.EnterState(StateType.Idle);

                return;
            }
        }

        public override void Exit(CharController c)
        {

        }
    }
}