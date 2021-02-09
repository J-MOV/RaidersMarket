using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemyStats;
    public int enemyHealth;

    public Slider healthSlider;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem damageParticle;

    private void Start()
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

        ParticleSystem _damageParticle = Instantiate(damageParticle, transform.position, Quaternion.identity);
        Destroy(_damageParticle.gameObject, 2f);

        if (enemyHealth <= 0)
        {
            FindObjectOfType<DungeonManager>().OnEnemyDeath();
            ParticleSystem _deathParticle = Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(_deathParticle.gameObject, 2f);
            Destroy(gameObject);
        }
    }

}
