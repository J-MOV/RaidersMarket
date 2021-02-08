using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemySO enemyStats;
    [SerializeField] private PlayerHealth playerHealth;

    float attackTimer = 0f;
    float attackCooldown;

    void Start()
    {
        //TODO: Store reference to player health
        attackCooldown = enemyStats.attackCooldown;
    }

    private void Update()
    {
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
        playerHealth.currentHealth -= enemyStats.damage;
    }

    
}
