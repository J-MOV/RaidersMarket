﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Enemy enemyStats;
    //TODO: Reference the player health
    // Start is called before the first frame update
    void Start()
    {
        //TODO: Store reference to player health
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AttackThePlayer()
    {
        //TODO: Make the player lose HP per X second
        yield return new WaitForSeconds(enemyStats.AttackTimer);
    }
}
