using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AccountLevelHandler
{
    private static int accountLevel;

    private static int eXP;

    public static List<int> mileStones;

    /// <summary>
    /// Initializes the List of levels and their goals
    /// </summary>
    /// <param name="levelGoals"></param>
    public static void Initzialize(List<int> levelGoals)
    {
        mileStones = levelGoals;
    }

    public static void AddExperience(int ammount)
    {
        eXP += ammount;

        if(eXP >= mileStones[accountLevel + 1])
        {
            eXP -= mileStones[accountLevel + 1];
            LevelUp();
        }

        foreach(var level in mileStones)
        {
            if(eXP >= level)
            {

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
