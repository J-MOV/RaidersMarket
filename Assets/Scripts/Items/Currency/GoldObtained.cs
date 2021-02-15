using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldObtained : MonoBehaviour
{
    [HideInInspector] public int goldPlayerHas;

    [SerializeField] int randomizedGoldMax;
    [SerializeField] int randomizedGoldMin;

    CurrencyManager manager;
    

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<CurrencyManager>();

        goldPlayerHas = PlayerPrefs.GetInt("amountOfGoldPlayerHas");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            FinishedDungeon();
    }

    public void FinishedDungeon()
    {
        goldPlayerHas += Random.Range(randomizedGoldMin, randomizedGoldMax);

        PlayerPrefs.SetInt("amountOfGoldPlayerHas", goldPlayerHas);

        Debug.Log("The player got this: " + goldPlayerHas + " gold!");
    }
}
