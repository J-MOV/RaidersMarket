using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuff : Loot
{
    public int defenseAmount = 10;

    public override void AddItemEffect()
    {
        FindObjectOfType<PlayerHealth>().AlterHealth(defenseAmount);
    }

    public override void RemoveItemEffect()
    {
        FindObjectOfType<PlayerHealth>().AlterHealth(-defenseAmount);
    }
}
