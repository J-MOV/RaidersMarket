using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAndSellItems : MonoBehaviour
{

    [SerializeField] int costOfItem;

    CurrencyManager manager;

    void Start()
    {
        manager = FindObjectOfType<CurrencyManager>();

        Debug.Log(manager);
    }


    public void Buy()
    {
        if(costOfItem >= manager.amountOfGoldPlayerHas)
        {
            manager.amountOfGoldPlayerHas -= costOfItem;
            //TODO: Create a reference to the player so that the item belongs to the player.
        }
    }

    public void PutUpOnMarketPlace()
    {
        //TODO: Remove the item from the player and put it up on the marketplace.
    }

    public void Sell()
    {
        manager.amountOfGoldPlayerHas += costOfItem;
    }
}
