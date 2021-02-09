using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    [HideInInspector] public EnemyHealth enemyHealthScript;

    public int amountOfClicks;
    int currentClickCount = 0;

    int damagePerClick;
    [HideInInspector] public int totalDamage;


    private void Start()
    {
        damagePerClick = totalDamage / amountOfClicks;
    }

    public void DealDamage()
    {
        currentClickCount++;

        if(currentClickCount >= amountOfClicks)
        {
            Debug.Log("WeakPoint destroyed!");
            
            enemyHealthScript.TakeDamage(totalDamage);
            Destroy(gameObject);
        }
    }

}
