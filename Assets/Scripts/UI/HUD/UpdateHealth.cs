using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealth : MonoBehaviour
{
    Slider healthSlider;

    public bool isPlayer1;
    // Update is called once per frame

    private void Start()
    {
        healthSlider = gameObject.GetComponent<Slider>();

        if (isPlayer1)
        {
            CharManager.player1.OnHealthChanged += UpdateHealthBar;
        }
        else
        {
            CharManager.player2.OnHealthChanged += UpdateHealthBar;
        }
    }

    public void UpdateHealthBar()
    {
        healthSlider.value = isPlayer1 ? CharManager.player1.Health : CharManager.player2.Health;
    }
}
