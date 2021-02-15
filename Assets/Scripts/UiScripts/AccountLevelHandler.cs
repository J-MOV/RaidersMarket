using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AccountLevelHandler
{
    private static int accountLevel;

    private static int eXP;

    public static List<int> mileStones;

    public static void AddExperience(int ammount)
    {
        eXP += ammount;

        foreach(var level in mileStones)
        {
            if(eXP >= level)
            {
                eXP -= level;
                LevelUp();
            }
        }
    }

    /// <summary>
    /// Returns the current Account Experience
    /// </summary>
    public static double Experience()
    {
        return eXP;
    }

    /// <summary>
    /// Returns the current Account Level
    /// </summary>
    public static int Level()
    {
        return accountLevel;
    }

    static void LevelUp()
    {
        accountLevel++;
    }

}
