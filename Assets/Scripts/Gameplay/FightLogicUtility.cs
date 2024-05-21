using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class FightLogicUtility : MonoBehaviour
    {
        static public bool IsPlayerOnRightSide(bool player1)
        {
            //determine if player 1 is to the right of player 2 (true) or not (false)
            bool isplayer1OnRight = CharManager.player1.transform.position.x - CharManager.player2.transform.position.x > 0 ? true : false;

            //return the value if looking for player 1, return the opposite if player 2
            return player1 ? isplayer1OnRight : !isplayer1OnRight;
        }

        static public Vector3 PlayerPositionOffset(Vector3 basePosition, float offsetX, float offsetY)
        {
            if(basePosition == null)
            {
                Debug.LogWarning("PlayerPositionOffset: Base position null");
                return Vector3.zero;
            }

            return new Vector3(basePosition.x + offsetX, basePosition.y + offsetY, basePosition.z);
        }
    }
}
