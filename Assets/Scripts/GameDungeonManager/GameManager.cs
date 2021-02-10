using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int dungeonLevel;
    public int setEnemyCount;
    public float lootChanceInPercentage;
    public static bool isPlaying;
    int amountOfEnemies;
    int enemiesDefeated;

    public List<ItemStatsSO> randomLootDrops;
    public GameObject lootPrefab;
    [SerializeField] Transform lootDropPosition;

    public List<Loot> collectedLoot;

    int uncommonLoot, commonLoot, rareLoot, legendaryLoot; //Colleced in this dungeon
    public TextMeshProUGUI uncommonText, commonText, rareText, legendaryText;

    public Text dungeonProgressText;
    public GameObject endScreenPanel;

    public Text dungeonCompleteText;

    [SerializeField] ParticleSystem fireWorks;
    [SerializeField] Transform firework1Pos, firework2Pos;

    EnemySpawner enemySpawner;
    DungeonManager dungeonManager;
    PlayerHealth playerHealth;

    public GameObject playerHealthSlider, enemyHealthSlider;

    [Space]
    public EnemySO easyEnemy, normalEnemy, hardEnemy, insaneEnemy;
    [Space]

    public EnemySO enemyVariantToSpawn;

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
        dungeonManager.EnemyDead += DropRandomLoot;
        playerHealth.PlayerDead += StopDungeon;

        endScreenPanel.SetActive(false);
    }

    public void BeginDungeon(int level ,int enemyCount)
    {
        isPlaying = true;
        dungeonLevel = level;
        uncommonLoot = 0;
        commonLoot = 0;
        rareLoot = 0;
        legendaryLoot = 0;

        timeSinceLastEnemy = timeBetweenEnemies;

        enemiesDefeated = 0;
        amountOfEnemies = enemyCount;
    }

    private void Update()
    {
        if (isPlaying)
        {
            playerHealthSlider.SetActive(true);
            enemyHealthSlider.SetActive(true);

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
            playerHealthSlider.SetActive(false);
            enemyHealthSlider.SetActive(false);
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

    void DropRandomLoot()
    {
        int rnd = Random.Range(0, 100);
        if (rnd < lootChanceInPercentage) {

            Vector3 randomOffsetPosition = new Vector3(Random.Range(-2f, 2f),Random.Range(-0.5f, 0.5f),Random.Range(-2f, 2f));

           GameObject newLoot = Instantiate(lootPrefab, lootDropPosition.position + randomOffsetPosition, Quaternion.identity);
            newLoot.GetComponent<Loot>().itemStats = randomLootDrops[Random.Range(0, randomLootDrops.Count)];

            //Add loot to temporary loot list
            collectedLoot.Add(newLoot.GetComponent<Loot>());
            if (newLoot.GetComponent<Loot>().itemStats.itemRarity == ItemRarity.uncommon)
                uncommonLoot++;
            else if (newLoot.GetComponent<Loot>().itemStats.itemRarity == ItemRarity.common)
                commonLoot++;
            else if (newLoot.GetComponent<Loot>().itemStats.itemRarity == ItemRarity.rare)
                rareLoot++;
            else if (newLoot.GetComponent<Loot>().itemStats.itemRarity == ItemRarity.legendary)
                legendaryLoot++;
        }
    }

    IEnumerator waitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void EndDungeon(bool completed)
    {
        isPlaying = false;
        endScreenPanel.SetActive(true);


        uncommonText.text = uncommonLoot.ToString();
        commonText.text = commonLoot.ToString();
        rareText.text = rareText.ToString();
        legendaryText.text = legendaryLoot.ToString();

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

            foreach (Loot loot in collectedLoot) //Add all loot collected in one dungeon to inventory
            {
                FindObjectOfType<Inventory>().EquipItem(loot);
            }

        }
        else
        {
            //If dungeon was NOT completed
            Debug.Log("Player failed dungeon " + dungeonLevel + " and killed " + enemiesDefeated + " out of " + amountOfEnemies);
            dungeonProgressText.text = "Dungeon " + dungeonLevel + " Failed!";
            dungeonCompleteText.text = "Dungeon " + dungeonLevel + " Failed!";
        }
    }
}

