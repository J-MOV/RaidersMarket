using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] PlayerStatsSO playerStats;

    public int baseDamage; //per click


    private void Start()
    {
        baseDamage = playerStats.baseDamage;
    }
}
