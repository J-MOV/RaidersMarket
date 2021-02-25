﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public float enemyMultiplier = 1.5f;

    GameManager gameManager;

    [SerializeField] EnemySO easyEnemy, normalEnemy, hardEnemy, insaneEnemy;

    int enemyCount;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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

        float enemyCountInFloat = level * enemyMultiplier;
        enemyCount = (int)Mathf.Ceil(enemyCountInFloat);

        gameManager.enemyVariantToSpawn = normalEnemy;

        gameManager.BeginDungeon(_level, enemyCount);
    }
}
