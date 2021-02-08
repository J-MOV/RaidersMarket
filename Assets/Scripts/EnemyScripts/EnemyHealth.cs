using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemyStats;
    public int enemyHealth;

    private void Awake()
    {
        enemyHealth = enemyStats.enemyHealth;
    }
    
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
