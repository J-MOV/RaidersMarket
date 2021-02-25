/**
 *  This script manages the navigation links in the Inventory & Market
 *  window. You can use the public functions in this script to open this
 *  view from anywhere in the game
 */

using Newtonsoft.Json;
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

public class PublicUser {
    public string username;
    public int gold, id, lvl;
}

public class InventoryNavigation : MonoBehaviour
{

    public List<Navigation> navigationLinks;
    public GameObject inventoryAndNavigation;
    public MarketManager market;
    public OnlineRaidManager raidManager;
    public OnlineConnection connection;

    public Transform leaderboardContent;
    public Transform leaderboardEntry;

    public RotateInspectedItem rotation;

    const float COUNT_DOWN_TIME = 1f; // Seconds
    private bool countingDown = false;
    private float countdown = 0;

    public Button startRaidButton;

    public Transform playerInspectView;

    public void Update() {
        if (countingDown) {
            countdown -= Time.deltaTime;
            if (countdown <= 0) {
                countingDown = false;
                raidManager.StartRaid();
            }
            startRaidButton.GetComponentInChildren<Text>().text = countingDown ? "STARTING IN " + countdown.ToString("0.00")  + "s" : "STARTING!";
        }
    }

    public void OpenInventory() {
        OpenTab("inventory");
    }

    public void OpenMarket() {
        OpenTab("market");
        
    }

    public void StartRaidButtonClick() {
        if (!countingDown) {
            StartRaidCountdown();
        } else {
            CancelRaidCountdown();
        }
    }

    public void StartRaidCountdown() {
        countingDown = true;
        countdown = COUNT_DOWN_TIME;
    }

    public void CancelRaidCountdown() {
        countingDown = false;
        raidManager.UpdateRaidButtonText();
    }


    void Start() {
        foreach (Navigation nav in navigationLinks) {
            Button button = nav.link.GetComponent<Button>();
            button.onClick.AddListener(() => {
                OpenTab(nav.title);   
            });
        }
    }

    public void LoadLeaderboard() {
        connection.Send("leaderboard");
    }

    public void InspectPlayer() {

    }

    public void ClosePlayerInspect() {

    }

    public void OnLeaderboard(string json) {
        PublicUser[] users = JsonConvert.DeserializeObject<PublicUser[]>(json);
        
        while(leaderboardContent.childCount > 0) {
            DestroyImmediate(leaderboardContent.GetChild(0).gameObject);
        }

        foreach(PublicUser user in users) {
            Transform entry = Instantiate(leaderboardEntry, leaderboardContent);
            entry.Find("Username").GetComponent<Text>().text = user.username;
            entry.Find("Gold").GetComponent<Text>().text = user.gold.ToString();
            entry.Find("Level").GetComponent<Text>().text = "Lvl " + user.lvl;
            if (user == users[0]) entry.Find("Crown").gameObject.SetActive(true);
        }
    }

    public void OpenTab(string title) {
        if (countingDown) return;
        rotation.inMainMenu = false;
        foreach (Navigation nav in navigationLinks) {
            if(nav.title == title) {

                if(title == "market") market.UpdateMarketFront();
                if (title == "leaderboard") LoadLeaderboard();

                DisableAllWindows();
                nav.window.gameObject.SetActive(true);
                SetNavigationLinkColor(nav, new Color32(0, 0, 0, 119));
                Open();
                return;
            }            
        }
    }

  
    public void Open() {
        rotation.inMainMenu = false;
        inventoryAndNavigation.SetActive(true);
    }

    public void Close() {
        inventoryAndNavigation.SetActive(false);
        rotation.inMainMenu = true;

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
