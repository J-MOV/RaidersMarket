using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Item Stats")]
public class ItemStatsSO : ScriptableObject
{
    public string itemName = "Item";

    public string itemDescription = "This is an item description";

    public int itemCost = 20;
    public ItemRarity itemRarity;
   
}
public enum ItemRarity 
{ 
    common, uncommon, rare, legendary
}