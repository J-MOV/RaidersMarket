using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemyStats;
    public int enemyHealth;

    int enemiesKilled;

    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem damageParticle;

    private void Start()
    {
        enemiesKilled = PlayerPrefs.GetInt("enemiesKilled");

        if (!enemyStats)
        {
            Debug.LogWarning("No Enemy Stats were found!");
        }
        else 
        { 
            enemyHealth = enemyStats.enemyHealth;
        }

        
    }
    
    public void TakeDamage(int damage)
    {
       
        Debug.Log("Enemy took damage");
        enemyHealth -= damage;

        ParticleSystem _damageParticle = Instantiate(damageParticle, transform.position, Quaternion.identity);
        Destroy(_damageParticle.gameObject, 2f);

        if (enemyHealth <= 0)
        {
            enemiesKilled++;
            PlayerPrefs.SetInt("enemiesKilled", enemiesKilled);
            AnalyticsResult results = Analytics.CustomEvent("Enemies Killed");
            Debug.Log(results);

            
            ParticleSystem _deathParticle = Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(_deathParticle.gameObject, 2f);
            transform.position = new Vector3(0, -50, 0);
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

}
