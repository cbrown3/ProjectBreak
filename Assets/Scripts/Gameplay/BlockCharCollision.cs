using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightLogic
{
    public class BlockCharCollision : MonoBehaviour
    {
        public BoxCollider2D characterCollider;
        public BoxCollider2D guardCollider;
        public PolygonCollider2D characterBlockerCollider;
        // Start is called before the first frame update
        void Start()
        {
            Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
            Physics2D.IgnoreCollision(guardCollider, characterBlockerCollider, true);
        }

    }
}