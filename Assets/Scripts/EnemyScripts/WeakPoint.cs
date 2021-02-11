using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    [HideInInspector] public EnemyHealth enemyHealthScript;
    PlayerCombat playerCombatScript;

    public int weakpointHealth;
    int currentWeakPointDamageDealt = 0;

    public int damagePerClick;
    [HideInInspector] public int totalDamage;

    private void Start()
    {
        playerCombatScript = FindObjectOfType<PlayerCombat>();

        damagePerClick = playerCombatScript.baseDamage;
    }

    public void DealDamage()
    {
        currentWeakPointDamageDealt += damagePerClick;

        if(currentWeakPointDamageDealt >= weakpointHealth)
        { 
            enemyHealthScript.TakeDamage(totalDamage);
            Destroy(gameObject);
        }
    }

}
