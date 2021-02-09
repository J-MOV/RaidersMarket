﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] enemyPositions;

    public Transform tempSpawnPoint;

    int currEnemy;

    private void Start()
    {
        currEnemy = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SpawnEnemy(tempSpawnPoint);
    }


    public void SpawnEnemy(Transform spawnPosition)
    {
        Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
        currEnemy++;
    }
}