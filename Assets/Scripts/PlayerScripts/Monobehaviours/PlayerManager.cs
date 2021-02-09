using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStats;

    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();

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


        Debug.Log("Assigned stats to player");
    }
}
