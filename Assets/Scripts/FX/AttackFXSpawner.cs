using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AttackFXSpawner : MonoBehaviour
{
    [NonSerialized]
    public static AttackFXSpawner Instance;

    [SerializeField]
    private GameObject hitFXPrefab;

    [SerializeField]
    private GameObject blockFXPrefab;

    private ObjectPool<GameObject> hitFXPool;

    private ObjectPool<GameObject> blockFXPool;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        hitFXPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(hitFXPrefab);
        }, hitFX =>
        {
            hitFX.gameObject.SetActive(true);
        }, hitFix =>
        {
            hitFix.gameObject.SetActive(false);
        }, hitFix =>
        {
            Destroy(hitFix.gameObject);
        }, false, 3, 6);


        blockFXPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(blockFXPrefab);
        }, blockFX =>
        {
            blockFX.gameObject.SetActive(true);
        }, blockFX =>
        {
            blockFX.gameObject.SetActive(false);
        }, blockFX =>
        {
            Destroy(blockFX.gameObject);
        }, false, 3, 6);
    }

    public void SpawnHitFX(Vector3 position)
    {
        GameObject hitFX = hitFXPool.Get();
        hitFX.transform.position = position;
    }

    public void SpawnBlockFX(Vector3 position)
    {
        GameObject blockFX = blockFXPool.Get();
        blockFX.transform.position = position;
    }

    public void KillHitFX(GameObject hitFX)
    {
        hitFXPool.Release(hitFX);
    }

    public void KillBlockFX(GameObject blockFX)
    {
        blockFXPool.Release(blockFX);
    }
}
