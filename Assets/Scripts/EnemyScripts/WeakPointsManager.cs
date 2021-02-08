using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeakPointsManager : MonoBehaviour
{
    public int amountOfWeakPoints = 4;
    [SerializeField] private EnemyHealth enemyHealthScript;

    public WeakPoint weakPointPrefab;

    public Image spawnAreaForWeakPoints;

    private void Start()
    {
        
        spawnAreaForWeakPoints = GameObject.FindGameObjectWithTag("WeakPointArea").GetComponent<Image>();

        SpawnWeakPoints(spawnAreaForWeakPoints);
    }


    public void SpawnWeakPoints(Image weakPointsSpawnArea)
    {
        for (int i = 0; i<amountOfWeakPoints; i++)
        {
            //Get random position in 2d area
            float randomX = Random.Range(-weakPointsSpawnArea.GetComponent<RectTransform>().rect.width, weakPointsSpawnArea.GetComponent<RectTransform>().rect.width);
            float randomY = Random.Range(-weakPointsSpawnArea.GetComponent<RectTransform>().rect.height, weakPointsSpawnArea.GetComponent<RectTransform>().rect.height);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

           
            WeakPoint newWeakPoint = Instantiate(weakPointPrefab, weakPointsSpawnArea.transform.position + spawnPosition, Quaternion.identity);
            newWeakPoint.transform.SetParent(weakPointsSpawnArea.transform);

            //Assign values to weakpoint
            newWeakPoint.enemyHealthScript = enemyHealthScript;
            newWeakPoint.totalDamage = enemyHealthScript.enemyHealth / amountOfWeakPoints;
        }
    }
}
