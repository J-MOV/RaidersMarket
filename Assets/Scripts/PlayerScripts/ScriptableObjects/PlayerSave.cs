using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    public List<Item> itemInventory;
    public PlayerSave(Inventory inventory)
    {
        itemInventory = inventory.itemsInInventory;
    }
}
