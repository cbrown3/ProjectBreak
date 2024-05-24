using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFX : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        AttackFXSpawner.Instance.KillHitFX(gameObject);
    }
}
