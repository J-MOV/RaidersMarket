using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New PlayerStats", menuName ="PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public int startingHealth = 0;
    public int maxHealth = 0;

    public int baseDamage = 0;
}
