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
        staminaSlider.value = isPlayer1 ? CharManager.player1.Stamina : CharManager.player2.Stamina;
    }
}
