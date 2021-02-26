using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Item", menuName = "Items/Stat Buff Item")]
public class StatBuffItem : BaseItem
{
    public int attackValue;
    public int defenseValue;
}
