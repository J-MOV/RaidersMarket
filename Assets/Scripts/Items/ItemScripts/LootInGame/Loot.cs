using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour, ILootInterface, IItemInterface
{
    public ItemStatsSO itemStats;
    ItemRarity itemRarity;

    [SerializeField] GameObject common, uncommon, rare, legendary;


    void Start()
    {
        itemRarity = itemStats.itemRarity;

        PlayGlowParticle(itemRarity);
    }

    void PlayGlowParticle(ItemRarity rarity)
    {
        if (itemRarity == ItemRarity.common)
        {
            Debug.Log("COMMON");
            common.SetActive(true);
        }
        else if (itemRarity == ItemRarity.uncommon)
        {
            Debug.Log("UNCOMMON");
            uncommon.SetActive(true);
        }
        else if (itemRarity == ItemRarity.rare)
        {
            Debug.Log("RARE");
            rare.SetActive(true);
        }
        else if (itemRarity == ItemRarity.legendary)
        {
            Debug.Log("LEGENDARY");
            legendary.SetActive(true);
        }
    }

    public void LootItem()
    {
        FindObjectOfType<Inventory>().EquipItem(this);
    }

    public void AddItemEffect()
    {
        Debug.Log(itemStats.itemName+" affecting player");
    }

    public void RemoveItemEffect()
    {
        Debug.Log(itemStats.itemName + " no longer affecting player");
    }
}
