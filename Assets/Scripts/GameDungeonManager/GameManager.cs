using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int dungeonLevel;
    public int setEnemyCount;
    public static bool isPlaying;
    int amountOfEnemies;
    int enemiesDefeated;

    public OnlineRaidManager onlineRaid;


    public Text dungeonProgressText;
    public GameObject endScreenPanel;

    [SerializeField] GameObject topDungeonProgress;

    public Text dungeonCompleteText;

    [SerializeField] ParticleSystem fireWorks;
    [SerializeField] Transform firework1Pos, firework2Pos;

    EnemySpawner enemySpawner;
    DungeonManager dungeonManager;
    PlayerHealth playerHealth;
    GoldObtained goldManager;

    public GameObject playerHealthSlider, enemyHealthSlider;

    [Space]
    public EnemySO easyEnemy, normalEnemy, hardEnemy, insaneEnemy;
    [Space]

    public EnemySO enemyVariantToSpawn;

    public float timeBetweenEnemies = 4f;
    float timeSinceLastEnemy;

    bool spawnEnemy;
    bool isFightingEnemy;

    bool canPan = true;

    DungeonCamera gameCamera;

    public GameObject lootPrefab;

    [SerializeField] Transform lootDropPosition;

    int commonLoot, uncommonLoot, rareLoot, legendaryLoot, mythicLoot;

    [SerializeField] TextMeshProUGUI commonLootText, unCommonLootText, rareLootText, 
        legendaryLootText, mythicLootText;

       

    private void Start()
    {


        enemySpawner = FindObjectOfType<EnemySpawner>();
        dungeonManager = FindObjectOfType<DungeonManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        gameCamera = FindObjectOfType<DungeonCamera>();

        dungeonManager.EnemyDead += () => enemiesDefeated++;
        dungeonManager.EnemyDead += () => isFightingEnemy = false;
        dungeonManager.EnemyDead += SpawnNextEnemy;
        //dungeonManager.EnemyDead += DropLoot;

        playerHealth.PlayerDead += StopDungeon;

        dungeonProgressText.gameObject.SetActive(false);

        goldManager = FindObjectOfType<GoldObtained>();
    }

    public void BeginDungeon(int level ,int enemyCount)
    {
        isPlaying = true;
        dungeonLevel = level;


        dungeonProgressText.gameObject.SetActive(true);

        gameCamera.EnterRaid();

        timeSinceLastEnemy = timeBetweenEnemies;

        enemiesDefeated = 0;
        amountOfEnemies = enemyCount;

    }

    public bool isEnding() {
        return enemiesDefeated == amountOfEnemies;
    }

    private void Update()
    {
        if (isPlaying)
        {
            playerHealthSlider.SetActive(true);
            enemyHealthSlider.SetActive(true);

            if(enemiesDefeated < amountOfEnemies) dungeonProgressText.text = (enemiesDefeated + 1) + "/" + amountOfEnemies;
            //Do stuff when playing

            
            //Spawn enemies

            if (timeSinceLastEnemy < timeBetweenEnemies && !isFightingEnemy)
            {
                timeSinceLastEnemy += Time.deltaTime;
                if (canPan && isPlaying)
                {
                    //gameCamera.PanNext();
                    canPan = false;
                }
            }

            if (timeSinceLastEnemy >= timeBetweenEnemies)
                spawnEnemy = true;

            SpawnNextEnemy();
        }
        else
        {
            //Do stuff when not playing
            playerHealthSlider.SetActive(false);
            enemyHealthSlider.SetActive(false);
        }
    }

    void StopDungeon()
    {
        EndDungeon(false);
        gameCamera.ExitRaid();
    }

    void SpawnNextEnemy()
    {
        if (spawnEnemy)
        {
            isFightingEnemy = true;
            timeSinceLastEnemy = 0;
            spawnEnemy = false;
            enemySpawner.SpawnEnemy(enemyVariantToSpawn);
            canPan = true;
        }
    }

    public void DropLoot(ItemRarity rarity)
    {


        Loot newLoot = Instantiate(lootPrefab, lootDropPosition.position, Quaternion.identity).GetComponent<Loot>();

        newLoot.itemStatsSO.itemRarity = rarity;
        newLoot.SetRarityColor(rarity);

    }

    bool ShouldDropLoot()
    {
        //somehow determine if loot should drop or not
        return true;
    }


    public void EndDungeon(bool completed)
    {
        isPlaying = false;
        dungeonProgressText.gameObject.SetActive(false);

        onlineRaid.EndRaid(completed);


        WeakPoint[] weakPoints = FindObjectsOfType<WeakPoint>();

        WeakPointsManager[] enemies = FindObjectsOfType<WeakPointsManager>();

        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }


        for (int i = 0; i < weakPoints.Length; i++)
        {
            Destroy(weakPoints[i].gameObject);
        }

        return;
        endScreenPanel.SetActive(true);

        UpdateLootText();

        if (completed)
        {
            //If dungeon was completed
            Debug.Log("Player completed dungeon " + dungeonLevel + " with " + amountOfEnemies + " enemies");
            dungeonProgressText.text = "Dungeon "+ dungeonLevel + " Complete!";
            dungeonCompleteText.text = "Dungeon " + dungeonLevel + " Complete!";

            ParticleSystem _fireWorks1 = Instantiate(fireWorks, firework1Pos.position, Quaternion.identity);
            Destroy(_fireWorks1.gameObject, 3f);

            ParticleSystem _fireWorks2 = Instantiate(fireWorks, firework2Pos.position, Quaternion.identity);
            Destroy(_fireWorks2.gameObject, 3f);

            PlayerPrefs.SetInt("currentLevel", dungeonLevel + 1);


            Analytics.CustomEvent("LevelCompleted");
        }
        else
        {
            //If dungeon was NOT completed
            Debug.Log("Player failed dungeon " + dungeonLevel + " and killed " + enemiesDefeated + " out of " + amountOfEnemies);
            dungeonProgressText.text = "Dungeon " + dungeonLevel + " Failed!";
            dungeonCompleteText.text = "Dungeon " + dungeonLevel + " Failed!";
            Analytics.CustomEvent("LevelLost");
        }

    }

    void UpdateLootText()
    {
        commonLootText.text = commonLoot.ToString();
        unCommonLootText.text = uncommonLoot.ToString();
        rareLootText.text = rareLoot.ToString();
        legendaryLootText.text = legendaryLoot.ToString();
        mythicLootText.text = mythicLoot.ToString();

        //reset loot count for next dungeon
        commonLoot = 0;
        uncommonLoot = 0;
        rareLoot = 0;
        legendaryLoot = 0;
        mythicLoot = 0;

    }
}

