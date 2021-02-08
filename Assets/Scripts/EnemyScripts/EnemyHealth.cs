using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemyStats;
    public int enemyHealth;

    public Slider healthSlider;

    private void Awake()
    {
        healthSlider = GameObject.FindGameObjectWithTag("EnemyHealthBar").GetComponent<Slider>();

        healthSlider.maxValue = enemyStats.enemyHealth;
        healthSlider.value = healthSlider.maxValue;

        enemyHealth = enemyStats.enemyHealth;
    }
    
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        healthSlider.value = enemyHealth;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
