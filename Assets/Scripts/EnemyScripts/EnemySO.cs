using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyStats", menuName = "EnemyStats")]
public class EnemySO : ScriptableObject
{
    public int enemyHealth = 100;
    public int damage = 20;

    public float attackCooldown = 3f;

    public int weakpoints = 1;
}
