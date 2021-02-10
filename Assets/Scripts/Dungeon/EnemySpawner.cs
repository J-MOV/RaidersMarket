using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform tempSpawnPoint;

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, tempSpawnPoint.position, Quaternion.identity);
    }
}
