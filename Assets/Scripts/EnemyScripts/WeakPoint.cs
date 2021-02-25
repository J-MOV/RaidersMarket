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

    [SerializeField] ParticleSystem popParticleSystem;

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
            FindObjectOfType<DungeonManager>().OnEnemyWeakpointDestroyed();
            
            GameObject particle = Instantiate(popParticleSystem.gameObject, transform.position, Quaternion.identity);
            Destroy(particle, 2f);
            Destroy(gameObject);
        }
    }

}
