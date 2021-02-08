using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth, maxHealth;
    public int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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
