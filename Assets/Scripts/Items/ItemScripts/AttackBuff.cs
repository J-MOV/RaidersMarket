using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuff : Loot
{
    public int attackBuffAmount = 10;

    public override void AddItemEffect()
    {
        FindObjectOfType<PlayerCombat>().baseDamage += attackBuffAmount;
        Debug.Log("base damage += " + attackBuffAmount);
    }

    public override void RemoveItemEffect()
    {
        FindObjectOfType<PlayerCombat>().baseDamage -= attackBuffAmount;
    }
}
