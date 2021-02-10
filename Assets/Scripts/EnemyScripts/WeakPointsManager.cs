using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WeakPointsManager : MonoBehaviour
{
    public int amountOfWeakPoints = 4;
    [SerializeField] private EnemyHealth enemyHealthScript;

    public WeakPoint weakPointPrefab;

    public Image spawnAreaForWeakPoints;

    private void Start()
    {
        amountOfWeakPoints = enemyHealthScript.enemyStats.weakpoints;
        spawnAreaForWeakPoints = GameObject.FindGameObjectWithTag("WeakPointArea").GetComponent<Image>();

        SpawnWeakPoints(spawnAreaForWeakPoints);
    }


    public void SpawnWeakPoints(Image weakPointsSpawnArea)
    {
        for (int i = 0; i<amountOfWeakPoints; i++)
        {
            
            //Get random position in 2d area
            float randomX = UnityEngine.Random.Range(weakPointsSpawnArea.rectTransform.rect.xMin, weakPointsSpawnArea.rectTransform.rect.xMax);
            float randomY = UnityEngine.Random.Range(weakPointsSpawnArea.rectTransform.rect.yMin, weakPointsSpawnArea.rectTransform.rect.yMax);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

           
            WeakPoint newWeakPoint = Instantiate(weakPointPrefab, weakPointsSpawnArea.transform.position + spawnPosition, Quaternion.identity);
            newWeakPoint.transform.SetParent(weakPointsSpawnArea.transform);

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
