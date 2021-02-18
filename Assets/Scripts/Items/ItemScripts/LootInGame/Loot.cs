using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Item item;
    public ItemStatsSO itemStats;

    [SerializeField] GameObject common, uncommon, rare, legendary;

    void Start()
    {
        item = new Item(itemStats, FindObjectOfType<Inventory>());
        PlayGlowParticle();

        item.AddStatsToPlayer();
    }

    void PlayGlowParticle()
    {
        if (itemStats.itemRarity == ItemRarity.common)
        {
            common.SetActive(true);
        }
        else if (itemStats.itemRarity == ItemRarity.uncommon)
        {
            uncommon.SetActive(true);
        }
        else if (itemStats.itemRarity == ItemRarity.rare)
        {
            rare.SetActive(true);
        }
        else if (itemStats.itemRarity == ItemRarity.legendary)
        {
            legendary.SetActive(true);
        }
    }

    public void LootItem()
    {
        FindObjectOfType<Inventory>().EquipItem(item);
    }

    public virtual void AddItemEffect()
    {
        item.AddStatsToPlayer();
        Debug.Log("Added " + item.itemName + " to equiped items");
    }

    public virtual void RemoveItemEffect()
    {
        item.RemoveStatsFromPlayer();
        Debug.Log("Removed " + item.itemName + " from equiped items");
    }

}
