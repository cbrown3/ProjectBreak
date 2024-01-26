using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FightLogic;

public class UpdateHealth : MonoBehaviour
{
    Slider healthSlider;

    public PlayerData playerData;
    // Update is called once per frame

    private void Start()
    {
        healthSlider = gameObject.GetComponent<Slider>();

        playerData.OnHealthChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar(int health)
    {
        healthSlider.value = health;
    }
}
