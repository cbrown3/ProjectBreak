using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FightLogic;

public class UpdateStamina : MonoBehaviour
{
    Slider staminaSlider;

    public PlayerData playerData;
    // Update is called once per frame

    private void Start()
    {
        staminaSlider = gameObject.GetComponent<Slider>();

        playerData.OnStaminaChanged += UpdateStaminaBar;
    }

    public void UpdateStaminaBar(int stamina)
    {
        staminaSlider.value = stamina;
    }
}
