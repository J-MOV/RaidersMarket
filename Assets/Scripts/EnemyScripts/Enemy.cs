using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyStats", menuName = "EnemyStats")]
public class Enemy : ScriptableObject
{
    public int enemyHealth = 100;
    public int damage = 20;
    public float waitUntilDeath = 2f;
    public float AttackTimer = 3f;
}
