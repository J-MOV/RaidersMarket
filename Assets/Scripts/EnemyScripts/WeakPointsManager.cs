﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WeakPointsManager : MonoBehaviour
{
    public int amountOfWeakPoints = 4;
    [SerializeField] private EnemyHealth enemyHealthScript;

    public WeakPoint weakPointPrefab;

    public Transform spawnAreaForWeakPoints;
    public float spawnAreaWidth, spawnAreaHeight;

    private void Start()
    {
        amountOfWeakPoints = enemyHealthScript.enemyStats.weakpoints;
        spawnAreaForWeakPoints = GameObject.FindGameObjectWithTag("WeakPointArea").transform;

        SpawnWeakPoints(spawnAreaForWeakPoints);
    }

    
    public void SpawnWeakPoints(Transform weakPointsSpawnArea)
    {
        for (int i = 0; i<amountOfWeakPoints; i++)
        {

            //Get random position in 2d area
            //float randomX = UnityEngine.Random.Range(weakPointsSpawnArea.rectTransform.rect.xMin, weakPointsSpawnArea.rectTransform.rect.xMax);
            //float randomY = UnityEngine.Random.Range(weakPointsSpawnArea.rectTransform.rect.yMin, weakPointsSpawnArea.rectTransform.rect.yMax);

            //Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

            spawnAreaWidth = weakPointsSpawnArea.GetComponent<WeakPointSpawner>().width;
            spawnAreaHeight = weakPointsSpawnArea.GetComponent<WeakPointSpawner>().height;
            Debug.Log(spawnAreaWidth);
            Debug.Log(spawnAreaHeight);

            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnAreaWidth, spawnAreaWidth), UnityEngine.Random.Range(-spawnAreaHeight, spawnAreaHeight));

            WeakPoint newWeakPoint = Instantiate(weakPointPrefab, weakPointsSpawnArea.position + spawnPosition, Quaternion.identity,weakPointsSpawnArea);

            //Assign values to weakpoint
            newWeakPoint.enemyHealthScript = enemyHealthScript;

            //reference to help round up int
            float floatEnemyHealth = enemyHealthScript.enemyStats.enemyHealth;
            float totalDamage = floatEnemyHealth / amountOfWeakPoints;

            //convert rounded up float to int
            newWeakPoint.totalDamage = Convert.ToInt32(Mathf.Ceil(totalDamage));
        }
    }
}
