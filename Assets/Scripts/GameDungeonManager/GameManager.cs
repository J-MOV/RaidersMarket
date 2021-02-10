using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int setEnemyCount;
    public static bool isPlaying;
    int amountOfEnemies;

    int enemiesDefeated;

    public Text dungeonProgressText;
    public GameObject endScreenPanel;

    public Text dungeonCompleteText;

    [SerializeField] ParticleSystem fireWorks;
    [SerializeField] Transform firework1Pos, firework2Pos;

    EnemySpawner enemySpawner;
    DungeonManager dungeonManager;
    PlayerHealth playerHealth;

    [Space]
    public EnemySO easyEnemy, normalEnemy, hardEnemy, insaneEnemy;
    [Space]

    [SerializeField] EnemySO enemyVariantToSpawn;

    public float timeBetweenEnemies = 4f;
    float timeSinceLastEnemy;

    bool spawnEnemy;
    bool isFightingEnemy;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        dungeonManager = FindObjectOfType<DungeonManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        dungeonManager.EnemyDead += () => enemiesDefeated++;
        dungeonManager.EnemyDead += () => isFightingEnemy = false;
        dungeonManager.EnemyDead += SpawnNextEnemy;
        playerHealth.PlayerDead += StopDungeon;

        endScreenPanel.SetActive(false);

        BeginDungeon(setEnemyCount);

    }

    public void BeginDungeon(int enemyCount)
    {
        isPlaying = true;

        timeSinceLastEnemy = timeBetweenEnemies;

        enemiesDefeated = 0;
        amountOfEnemies = enemyCount;
    }

    private void Update()
    {
        if (isPlaying)
        {
            dungeonProgressText.text = enemiesDefeated + "/" + amountOfEnemies;
            //Do stuff when playing

            if (enemiesDefeated == amountOfEnemies)
                EndDungeon(true);

            //Spawn enemies

            if (timeSinceLastEnemy < timeBetweenEnemies && !isFightingEnemy)
                timeSinceLastEnemy += Time.deltaTime;

            if (timeSinceLastEnemy >= timeBetweenEnemies)
                spawnEnemy = true;

            SpawnNextEnemy();
        }
        else
        {
            //Do stuff when not playing
        }
    }

    void StopDungeon()
    {
        EndDungeon(false);
    }

    void SpawnNextEnemy()
    {
        if (spawnEnemy)
        {
            isFightingEnemy = true;
            timeSinceLastEnemy = 0;
            spawnEnemy = false;
            enemySpawner.SpawnEnemy(enemyVariantToSpawn);
        }
    }

    public void EndDungeon(bool completed)
    {
        isPlaying = false;
        endScreenPanel.SetActive(true);
        

        if (completed)
        {
            //If dungeon was completed
            Debug.Log("Player completed dungeon with " + amountOfEnemies + " enemies");
            dungeonProgressText.text = "Dungeon Complete!";
            dungeonCompleteText.text = "Dungeon Complete!";

            ParticleSystem _fireWorks1 = Instantiate(fireWorks, firework1Pos.position, Quaternion.identity);
            Destroy(_fireWorks1.gameObject, 3f);

            ParticleSystem _fireWorks2 = Instantiate(fireWorks, firework2Pos.position, Quaternion.identity);
            Destroy(_fireWorks2.gameObject, 3f);

        }
        else
        {
            //If dungeon was NOT completed
            Debug.Log("Player failed dungeon and killed " + enemiesDefeated + " out of " + amountOfEnemies);
            dungeonProgressText.text = "Dungeon Failed!";
            dungeonCompleteText.text = "Dungeon Failed!";
        }
    }
}

