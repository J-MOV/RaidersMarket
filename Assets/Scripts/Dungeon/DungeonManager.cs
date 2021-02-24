using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public delegate void EnemyDeath();
    public event EnemyDeath EnemyDead;
    public OnlineRaidManager onlineRaid;

    public void OnEnemyDeath()
    {
        onlineRaid.KilledEnemy();
        if (EnemyDead != null)
            EnemyDead.Invoke();
    }

    

    public delegate void WeakPointDestroyed();
    public event WeakPointDestroyed onWeakPointDestroyed;

    public void OnEnemyWeakpointDestroyed()
    {
        if (onWeakPointDestroyed != null)
            onWeakPointDestroyed.Invoke();
    }
}
