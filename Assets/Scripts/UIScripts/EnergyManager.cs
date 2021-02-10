using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    int maxEnergy = 100;

    [SerializeField] int currentEnergy;

    [SerializeField] int lostEnergy;

    bool hasStartedGameForFirstTime = false;
    
    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = PlayerPrefs.GetInt("currentEnergy");

        hasStartedGameForFirstTime = PlayerPrefs.GetInt("hasStartedGameForFirstTime") == 1;
        Debug.Log(hasStartedGameForFirstTime);

        if (!hasStartedGameForFirstTime)
        {
            currentEnergy = maxEnergy;
            hasStartedGameForFirstTime = true;
            PlayerPrefs.SetInt("currentEnergy", currentEnergy);
            PlayerPrefs.SetInt("hasStartedGameForFirstTime", hasStartedGameForFirstTime ? 1 : 0);
        }
        else
            Debug.Log("This player has started the game once!");        
    }

    public void RemoveEnergy()
    {
        currentEnergy -= lostEnergy;
        PlayerPrefs.SetInt("currentEnergy",currentEnergy);
    }
}
