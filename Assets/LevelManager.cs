using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public int maxEnemiesRaw = 30; //without adding 1 enemy per level
    public float enemyMultiplier = 1.5f;

    GameManager gameManager;

    [SerializeField] EnemySO easyEnemy, normalEnemy, hardEnemy, insaneEnemy;

    int enemyCount;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        maxEnemiesRaw = maxEnemiesRaw + level;
        level = PlayerPrefs.GetInt("currentLevel");

        StartLevel(level);
    }

    public void StartLevel(int _level)
    {
        level = _level;

        float enemyCountInFloat = level * enemyMultiplier;
        enemyCount = (int)Mathf.Ceil(enemyCountInFloat);

        if(enemyCount < maxEnemiesRaw * 0.5f && enemyCount > 5)
        {
            gameManager.enemyVariantToSpawn = normalEnemy;
        }
        else if(enemyCount >= maxEnemiesRaw * 0.5f && enemyCount < maxEnemiesRaw)
        {
            gameManager.enemyVariantToSpawn = hardEnemy;
        }
        else if (enemyCount > maxEnemiesRaw)
        {
            enemyCount = maxEnemiesRaw;
            gameManager.enemyVariantToSpawn = hardEnemy;
        }

        gameManager.BeginDungeon(_level, enemyCount);
    }
}
