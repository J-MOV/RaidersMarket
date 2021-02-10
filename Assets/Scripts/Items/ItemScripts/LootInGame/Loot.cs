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
            common.SetActive(true);
        }
        else if (itemRarity == ItemRarity.uncommon)
        {
            uncommon.SetActive(true);
        }
        else if (itemRarity == ItemRarity.rare)
        {
            rare.SetActive(true);
        }
        else if (itemRarity == ItemRarity.legendary)
        {
            legendary.SetActive(true);
        }
    }

    public void LootItem()
    {
        FindObjectOfType<Inventory>().EquipItem(this);
    }

    public virtual void AddItemEffect()
    {
        Debug.Log(itemStats.itemName+" affecting player");
    }

    public virtual void RemoveItemEffect()
    {
        Debug.Log(itemStats.itemName + " no longer affecting player");
    }
}
