using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergyManager : MonoBehaviour
{
    int maxEnergy = 100;

    [SerializeField] int currentEnergy;

    [SerializeField] int chargedEnergy;

    [SerializeField] int lostEnergy;

    bool hasStartedGameForFirstTime = false;
    
    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("RechargeEnergy", 5, 5);

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RemoveAllEnergy();
        }
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
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("ViktorScene");
    }

    private void RechargeEnergy()
    {
        if(currentEnergy >= maxEnergy) 
        {
            Debug.Log("You have full energy!");
        }
        else
        {
            currentEnergy += chargedEnergy;
            PlayerPrefs.SetInt("currentEnergy", currentEnergy);
        }
    }

    private void RemoveAllEnergy()
    {
        //THIS IS DEVELOPER THINGY NOT FOR IN GAME
        currentEnergy = 0;
        PlayerPrefs.SetInt("currentEnergy", currentEnergy);
        Debug.Log("No Energy!");
    }
}
