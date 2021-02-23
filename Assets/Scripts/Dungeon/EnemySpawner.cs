using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform tempSpawnPoint;

    public Slider enemyHealthbar;
    EnemyHealth _enemy1, _enemy2;
    int totalHealth;

    private void Start()
    {
        FindObjectOfType<DungeonManager>().onWeakPointDestroyed += UpdateHealthBar;
    }

    public void SpawnEnemy(EnemySO _enemyStats)
    {
        totalHealth = 0;
        GameObject enemy = Instantiate(enemyPrefab, tempSpawnPoint.position, Quaternion.identity);
        enemy.GetComponent<EnemyHealth>().enemyStats = _enemyStats;
        enemy.GetComponent<EnemyBehaviour>().enemyStats = _enemyStats;
        _enemy1 = enemy.GetComponent<EnemyHealth>();

        GameObject enemy2 = Instantiate(enemyPrefab, tempSpawnPoint.position + new Vector3(-4f, 0f, 0f), Quaternion.identity);
        enemy2.GetComponent<EnemyHealth>().enemyStats = _enemyStats;
        enemy2.GetComponent<EnemyBehaviour>().enemyStats = _enemyStats;
        _enemy2 = enemy2.GetComponent<EnemyHealth>();

        totalHealth = _enemy1.GetComponent<EnemyHealth>().enemyStats.enemyHealth + _enemy2.GetComponent<EnemyHealth>().enemyStats.enemyHealth;
        enemyHealthbar.maxValue = totalHealth;
        enemyHealthbar.value = totalHealth;
    }


    void UpdateHealthBar()
    {
        totalHealth = _enemy1.GetComponent<EnemyHealth>().enemyHealth + _enemy2.GetComponent<EnemyHealth>().enemyHealth;
        
        enemyHealthbar.value = totalHealth;

        if (totalHealth <= 0)
        {
            FindObjectOfType<DungeonManager>().OnEnemyDeath();
            _enemy1.GetComponent<EnemyHealth>().DestroyObject();
            _enemy2.GetComponent<EnemyHealth>().DestroyObject();
        }
    }

}
