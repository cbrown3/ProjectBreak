using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStamina : MonoBehaviour
{
    Slider staminaSlider;

    public bool isPlayer1;
    // Update is called once per frame

    private void Start()
    {
        staminaSlider = gameObject.GetComponent<Slider>();

        if (isPlayer1)
        {
            CharManager.player1.OnStaminaChanged += UpdateStaminaBar;
        }
        else
        {
            CharManager.player2.OnStaminaChanged += UpdateStaminaBar;
        }
    }

    public void UpdateStaminaBar()
    {
        if(isPlayer1)
        {
            if(CharManager.player1.Stamina > staminaSlider.maxValue)
            {
                CharManager.player1.Stamina = (int)staminaSlider.maxValue;
            }

            staminaSlider.value = CharManager.player1.Stamina;
        }
        else
        {
            if (CharManager.player2.Stamina > staminaSlider.maxValue)
            {
                CharManager.player2.Stamina = (int)staminaSlider.maxValue;
            }

            staminaSlider.value = CharManager.player2.Stamina;
        }
    }
}
