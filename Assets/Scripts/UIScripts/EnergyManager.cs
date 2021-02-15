using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    int maxEnergy = 100;

    [SerializeField] int currentEnergy;

    [SerializeField] int lostEnergy;

    MainMenuButtons menuManager;

    bool hasStartedGameForFirstTime = false;
    
    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = PlayerPrefs.GetInt("currentEnergy");

        menuManager = FindObjectOfType<MainMenuButtons>();

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
        if(currentEnergy < lostEnergy)
        {
            Debug.Log("Sorry! You need more energy to play!");
        }
        else
        {
            currentEnergy -= lostEnergy;
            PlayerPrefs.SetInt("currentEnergy", currentEnergy);
            menuManager.StartGame();
        }
    }
}
