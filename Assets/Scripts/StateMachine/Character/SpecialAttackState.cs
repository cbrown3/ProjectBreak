using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class SpecialAttackState : IState<CharController>
    {
        public SpecialAttackState()
        {

        }
        public override void Enter(CharController c)
        {
            c.canAttack = false;
            c.canDash = true;
        }

        public override void Continue(CharController c)
        {

        }

        public override void Exit(CharController c)
        {

        }
    }
}