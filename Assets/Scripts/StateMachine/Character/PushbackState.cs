using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightLogic
{
    public class PushbackState : IState<CharController>
    {

        public PushbackState()
        {

        }

        public int forceAmount;
        int frameCount;

        public override void Enter(CharController c)
        {
            frameCount = 0;

            c.rigid.velocity = Vector2.zero;

            c.canDash = false;

            c.canAttack = false;

            c.interuptible = false;

            //c.animator.Play(c.aDashAnim);
        }

        public override void Continue(CharController c)
        {

            //End of pushback
            if (frameCount > c.pushbackFrameLength)
            {
                forceAmount = 0;
                c.EnterState(c.idleState);
                return;
            }
            //Pushback
            else
            {
                int direction = c.GetComponent<SpriteRenderer>().flipX ? 1 : -1;
                c.rigid.AddForce(Vector2.right * direction * forceAmount, ForceMode2D.Impulse);
            }

            frameCount++;
        }

        public override void Exit(CharController c)
        {

        }
    }
}