using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharCollision : MonoBehaviour
{
    public BoxCollider2D characterCollider;
    public BoxCollider2D guardCollider;
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
        //Physics2D.IgnoreCollision(guardCollider, characterBlockerCollider, true);
    }

}
