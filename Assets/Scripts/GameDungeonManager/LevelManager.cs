using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public int maxEnemiesRaw = 30; //without adding 1 enemy per level
    public float enemyMultiplier = 1.5f;

    GameManager gameManager;

    [SerializeField] EnemySO easyEnemy, normalEnemy, hardEnemy, insaneEnemy;

    int enemyCount;

    public Button beginRaidButton;
    public Canvas menuButtons;
    public Button levelSelectButton;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        maxEnemiesRaw = maxEnemiesRaw + level;
        if (!PlayerPrefs.HasKey("currentLevel"))
            PlayerPrefs.SetInt("currentLevel", 1);

        level = PlayerPrefs.GetInt("currentLevel");

    }

    public void StartNextRaid()
    {
        if (!PlayerPrefs.HasKey("currentLevel"))
            PlayerPrefs.SetInt("currentLevel", 1);

        level = PlayerPrefs.GetInt("currentLevel");

        StartLevel(level);
    }

    public void StartLevel(int _level)
    {
        level = _level;
        menuButtons.gameObject.SetActive(false);
        beginRaidButton.gameObject.SetActive(false);
        levelSelectButton.gameObject.SetActive(false);

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
