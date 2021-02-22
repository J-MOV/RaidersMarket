/**
 *  This script manages the navigation links in the Inventory & Market
 *  window. You can use the public functions in this script to open this
 *  view from anywhere in the game
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class Navigation {
    public string title;
    public Transform link;
    public Transform window;
}

public class InventoryNavigation : MonoBehaviour
{

    public List<Navigation> navigationLinks;
    public GameObject inventoryAndMarket;
    public MarketManager market;

    public void OpenInventory() {
        OpenTab("inventory");
    }

    public void OpenMarket() {
        OpenTab("market");
        
    }


    void Start() {
        foreach (Navigation nav in navigationLinks) {
            Button button = nav.link.GetComponent<Button>();
            button.onClick.AddListener(() => {
                OpenTab(nav.title);   
            });
        }
    }

    public void OpenTab(string title) {
        foreach (Navigation nav in navigationLinks) {
            if(nav.title == title) {

                if(title == "market") market.UpdateMarketFront();

                DisableAllWindows();
                nav.window.gameObject.SetActive(true);
                SetNavigationLinkColor(nav, new Color32(0, 0, 0, 119));
                inventoryAndMarket.SetActive(true);
                return;
            }            
        }
    }

  
    public void Open() {
        inventoryAndMarket.SetActive(true);
    }

    public void Close() {
        inventoryAndMarket.SetActive(false);
    }

    void DisableAllWindows() {
        foreach (Navigation nav in navigationLinks) {
            nav.window.gameObject.SetActive(false);
            SetNavigationLinkColor(nav, new Color32(0, 0, 0, 0));
        }
    }

    void SetNavigationLinkColor(Navigation nav, Color32 color) {
        nav.link.GetComponent<Image>().color = color;
    }
}
