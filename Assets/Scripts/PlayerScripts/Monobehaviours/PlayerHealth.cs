using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth, maxHealth;
    public int currentHealth;

    public Slider healthSlider;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = startingHealth;
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        //Audioclip?, particle effect?

        if(currentHealth <= 0)
        {
            //Die
            PlayerDeath();
        }
    }


    void PlayerDeath()
    {
        Debug.Log("Player died");
    }
}
