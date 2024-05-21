using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Player Data")]
public class PlayerData : ScriptableObject
{
    public const int MAX_HEALTH = 10;
    public const int MAX_STAMINA = 10;
    
    [Range(0, MAX_HEALTH)]
    [SerializeField]
    private int health;

    [Range(0, MAX_STAMINA)]
    [SerializeField]
    private int stamina;

    public event Action<int> OnHealthChanged;

    public event Action<int> OnStaminaChanged;

    public int Health
    {
        get 
        {
            return health;
        }
        set
        {
            health = value;
            OnHealthChanged?.Invoke(health);
        }
    }

    public int Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            stamina = value;
            OnStaminaChanged?.Invoke(stamina);
        }
    }
}
