/**
 * This is the structure of a unique and spawned item that
 * an owner owns. Info about the item, such as name, descrption, 3D asset and 
 * more can be found in it's IndexedItem
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item 
{
    public int id;
    public int item;
    public double pattern;
    public int owner;
    public int for_sale;
    public int equipped;
    public int price;
    public DateTime created;
    public int creator;
}
