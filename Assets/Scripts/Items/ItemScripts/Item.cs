using UnityEngine;

[System.Serializable]
public class Item
{
    public ItemStatsSO itemStats;

    public string itemName = "Item";
    public int cost = 0;
    public ItemRarity itemRarity;

    Inventory inventory;

    public Item(ItemStatsSO _itemStats, Inventory _inventory)
    {
        itemStats = _itemStats;
        inventory = _inventory;

        itemName = itemStats.itemName;
        cost = itemStats.itemCost;
        itemRarity = itemStats.itemRarity;
    }

    public virtual void AddStatsToPlayer()
    {
        inventory.AddItemStatsToPlayer(itemStats.damageValue, itemStats.defenseValue);
    }

    public virtual void RemoveStatsFromPlayer()
    {
        inventory.RemoveItemStatsFromPlayer(itemStats.damageValue, itemStats.defenseValue);
    }
}
