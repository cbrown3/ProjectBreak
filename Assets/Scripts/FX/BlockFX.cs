using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFX : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        AttackFXSpawner.Instance.KillBlockFX(gameObject);
    }
}
