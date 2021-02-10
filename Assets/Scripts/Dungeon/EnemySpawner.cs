using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform tempSpawnPoint;

    public void SpawnEnemy(EnemySO _enemyStats)
    {
        GameObject enemy = Instantiate(enemyPrefab, tempSpawnPoint.position, Quaternion.identity);
        enemy.GetComponent<EnemyHealth>().enemyStats = _enemyStats;
        enemy.GetComponent<EnemyBehaviour>().enemyStats = _enemyStats;
    }
}
