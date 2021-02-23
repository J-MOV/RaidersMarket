using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemySO enemyStats;
    [SerializeField] private PlayerHealth playerHealth;

    float attackTimer = 0f;
    float attackCooldown;

    void Start()
    {
        //TODO: Store reference to player health
        attackCooldown = enemyStats.attackCooldown;
        attackTimer = attackCooldown;

        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        if (!GameManager.isPlaying) return;

        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            //Attack
            Attack();
        }
    }

    void Attack()
    {
        attackTimer = attackCooldown;
        playerHealth.TakeDamage(enemyStats.damage);
    }

    
}
