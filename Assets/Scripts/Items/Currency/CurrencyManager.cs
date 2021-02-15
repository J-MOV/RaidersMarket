using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [HideInInspector] public int amountOfGoldPlayerHas;
    [SerializeField] int startingGold;

    bool hasStartedGame;

    private void Start()
    {
        hasStartedGame = PlayerPrefs.GetInt("hasStartedGameForFirstTime") == 1;

        amountOfGoldPlayerHas = PlayerPrefs.GetInt("amountOfGoldPlayerHas");

        if (!hasStartedGame)
        {
            amountOfGoldPlayerHas += startingGold;
            PlayerPrefs.SetInt("amountOfGoldPlayerHas", amountOfGoldPlayerHas);
        }

        Debug.Log(amountOfGoldPlayerHas);
    }

}
