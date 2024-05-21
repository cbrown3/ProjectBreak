using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject CameraManager;

    public static GameplayManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraShake()
    {
        CameraManager.GetComponent<Animator>().Play("CameraShake");
    }
}
