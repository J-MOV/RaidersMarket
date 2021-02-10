using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform tempSpawnPoint;

    public void SpawnEnemy(GameObject enemyType)
    {
        Instantiate(enemyType, tempSpawnPoint.position, Quaternion.identity);
    }
}
