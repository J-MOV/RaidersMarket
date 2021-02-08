using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemyStats;
    int enemyHealth;
    private void Start()
    {
        enemyHealth = enemyStats.enemyHealth;
        TakeDamage(enemyStats.damage);
        Debug.Log(enemyStats.enemyHealth);
    }
    
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            StartCoroutine(Dead());
        }
    }
    
    IEnumerator Dead()
    {
        //TODO: Create particle effect, play sound.
        yield return new WaitForSeconds(enemyStats.waitUntilDeath);
        Destroy(gameObject);
    }
}
