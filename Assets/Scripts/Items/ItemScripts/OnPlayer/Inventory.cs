using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Loot> itemsInInventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PrintItemsInInventory();
    }


    void PrintItemsInInventory()
    {
        Debug.Log("Number of items : " + itemsInInventory.Count);
        for(int i = 0; i<itemsInInventory.Count; i++)
        {
            Debug.Log("Item : " + itemsInInventory[i].itemStats.itemName + ", cost: " + itemsInInventory[i].itemStats.itemCost + ", item rarity : " + itemsInInventory[i].itemStats.itemRarity);
        }
    }

    public void RemoveItemFromInventory(Loot loot)
    {
        itemsInInventory.Remove(loot);
    }

    public void EquipItem(Loot newLoot)
    {
        itemsInInventory.Add(newLoot);
        Debug.Log(newLoot.itemStats.itemRarity + " item looted with name: " + newLoot.itemStats.itemName);

    }
}
