/**
 * Indexed items is the core info of an item.
 * Not an actual spawned item that a user owns.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IndexedItem {

    public int id;
    public string name;
    public string description;
    public string type;
    public Rarity rarity;
    public GameObject model;
    public bool pattern;
    public float hp;
}

[Serializable]
public class SerializedIndexedItem {
    public int id;
    public string name;
    public string description;
    public string type;
    public string model;
    public int rarity;
    public float loot;
    public int pattern;
    public float hp;
}