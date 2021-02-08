using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(PlayerCombat))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStats;

    PlayerHealth playerHealth;
    PlayerCombat playerCombat;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerCombat = GetComponent<PlayerCombat>();

        AssignStatsToPlayer();
    }


    void AssignStatsToPlayer()
    {
        if(playerStats == null)
        {
            Debug.LogWarning("No player stats were found!");
            return;
        }

        //Player health
        playerHealth.startingHealth = playerStats.startingHealth;
        playerHealth.maxHealth = playerStats.maxHealth;

        //Player combat
        playerCombat.baseDamage = playerStats.baseDamage;

        Debug.Log("Assigned stats to player");
    }
}
