using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCollected : MonoBehaviour
{
    public List<BaseItem> loot = new List<BaseItem>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PrintInventoryList();
    }

    void PrintInventoryList()
    {
        for (int i = 0; i < loot.Count; i++)
        {
            int itemNumber = i + 1;
            Debug.Log("Item " + itemNumber.ToString() 
                + " name : " + loot[i].itemName
                + ", Item cost : " + loot[i].itemCost
                + ", Item rarity : " + loot[i].itemRarity);

        }
    }

    public void ClearLootCollected()
    {
        loot.Clear();
    }
}
