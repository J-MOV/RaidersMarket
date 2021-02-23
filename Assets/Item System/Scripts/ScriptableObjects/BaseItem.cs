using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    public string itemName;
    [TextArea(5,10)]
    public string itemDescription;

    [Space]

    public int itemCost;

    public ItemRarity itemRarity;
    public ItemType itemType;
}

public enum ItemRarity { Common, Uncommon, Rare, Legendary, Mythic}

public enum ItemType { Skin, StatBuff}