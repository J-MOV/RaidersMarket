using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public delegate void OnPlayerDeath();
    public event OnPlayerDeath PlayerDead;

    public int startingHealth, maxHealth;
    public int currentHealth;

    public Slider healthSlider;

    DungeonManager dungeonManager;

    [SerializeField] ParticleSystem takeDamageParticle;
    [SerializeField] ParticleSystem deathParticle;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = startingHealth;
        currentHealth = startingHealth;

        dungeonManager = FindObjectOfType<DungeonManager>();
        dungeonManager.EnemyDead += HealPlayer;
    }

    private void Update()
    {
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Audioclip?, particle effect?

        ParticleSystem _damageParticle = Instantiate(takeDamageParticle, transform.position, Quaternion.identity);
        Destroy(_damageParticle.gameObject, 2f);

        if(currentHealth <= 0)
        {
            //Die
            PlayerDeath();
        }
    }

    void HealPlayer()
    {
        currentHealth = maxHealth;
    }

    void PlayerDeath()
    {
        Debug.Log("Player died");
        ParticleSystem _deathParticle = Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(_deathParticle.gameObject, 2f);

        PlayerDead?.Invoke();
    }
}
