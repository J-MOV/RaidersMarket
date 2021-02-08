using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Enemy enemyStats;
    private void Start()
    {
        Debug.Log(enemyStats.enemyHealth);
    }
    public void TakeDamage(int damage)
    {
        enemyStats.enemyHealth -= damage;
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        if(enemyStats.enemyHealth <= 100)
        {
            //TODO: Create particle effect, play sound.
            yield return new WaitForSeconds(enemyStats.waitUntilDeath);
            Destroy(gameObject);
        }
    }
}
