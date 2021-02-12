using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootManager : MonoBehaviour
{
    public Loot[] lootTypes; //Defense Buff, attack buff etc.
    public ItemStatsSO[] commonLootStats, uncommonLootStats, rareLootStats, legendaryLootStats;
    [SerializeField] Transform lootDropPosition;

    public TextMeshProUGUI uncommonText, commonText, rareText, legendaryText;

    public List<Loot> collectedLoot;

    public float lootChanceInPercentage;

    public float commonDropRate, uncommonDropRate, rareDropRate, legendaryDropRate; //In %


    int uncommonLootCount, commonLootCount, rareLootCount, legendaryLootCount; //Colleced in this dungeon


    [SerializeField] Transform dungeonTransform;

    private void Start()
    {
        uncommonLootCount = 0;
        commonLootCount = 0;
        rareLootCount = 0;
        legendaryLootCount = 0;
    }


    public void DropRandomLoot()
    {
        float rnd = Random.Range(0, 100);

        if (rnd < lootChanceInPercentage) {


            Vector3 randomOffsetPosition = new Vector3(Random.Range(-2f, 2f), Random.Range(-0.5f, 0.5f), Random.Range(-2f, 2f));

            
            
            float rnd2 = Random.Range(0, 100);
           
             Loot newLoot = Instantiate(lootTypes[Random.Range(0, lootTypes.Length)], lootDropPosition.position + randomOffsetPosition, Quaternion.identity);


            if (rnd2 <= legendaryDropRate)
            {
                //legendary
                newLoot.itemStats = legendaryLootStats[Random.Range(0, legendaryLootStats.Length)];
                Debug.Log("Dropped legendary item with drop rate of " + legendaryDropRate+"%");
            }
            else if (rnd2 <= rareDropRate)
            {
                //rare
                newLoot.itemStats = rareLootStats[Random.Range(0, rareLootStats.Length)];
                Debug.Log("Dropped rare item with drop rate of " + rareDropRate + "%");
            }
            else if (rnd2<= uncommonDropRate)
            {
                //uncommon
                newLoot.itemStats = uncommonLootStats[Random.Range(0, uncommonLootStats.Length)];
                Debug.Log("Dropped uncommon item with drop rate of " + uncommonDropRate + "%");
            }
            else
            {
                //common
                newLoot.itemStats = commonLootStats[Random.Range(0, commonLootStats.Length)];
                Debug.Log("Dropped common item with drop rate of " + commonDropRate + "%");
            }

                //Add loot to temporary loot list

                if (newLoot.itemStats.itemRarity == ItemRarity.uncommon)
                    uncommonLootCount++;
                else if (newLoot.itemStats.itemRarity == ItemRarity.common)
                    commonLootCount++;
                else if (newLoot.itemStats.itemRarity == ItemRarity.rare)
                    rareLootCount++;
                else if (newLoot.itemStats.itemRarity == ItemRarity.legendary)
                    legendaryLootCount++;

            }
        }

    public void SendLootToInventory()
    {
            foreach (Loot loot in collectedLoot) //Add all loot collected in one dungeon to inventory
            {
                FindObjectOfType<Inventory>().EquipItem(loot);
            }
     }

    public void UpdateLootCollectedText()
    {
        uncommonText.text = uncommonLootCount.ToString();
        commonText.text = commonLootCount.ToString();
        rareText.text = rareLootCount.ToString();
        legendaryText.text = legendaryLootCount.ToString();
    }
}

