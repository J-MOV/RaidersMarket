/**
 * This script manages the websocket connection
 * between the client and server.
 * 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using NativeWebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class UsernameAvailability{
    public string username;
    public bool available;
}

public class OnlineConnection : MonoBehaviour
{

    public ItemRenderer renderer;
    public InspectManager im;
    public OnlineRaidManager onlineRaid;

    public Rarity[] rarities = {
        new Rarity("Common", new Color32(199, 199, 199, 255)),
        new Rarity("Rare", new Color32(43, 114, 255, 255)),
        new Rarity("Epic", new Color32(255, 43, 128, 255)),
        new Rarity("Legendary", new Color32(255, 188, 43, 255)),
        new Rarity("Mythical", new Color32(255, 51, 51, 255))};

    WebSocket ws;

    string token;
    public User user;
    public Item[] inventory;
    public IndexedItem[] items;

    public Transform playerContainer;
    

    public Text goldAmountText;

    public Text loggedInStatusText;
    public Image loggedInStatusSymbol;

    public GameObject inventorySlotPrefab;
    public Transform inventorySpace;

    public MarketManager market;

    public Transform chooseUsernamePopup;

    public InventoryNavigation navigation;

    float animateGoldChangeTime = 2;
    int previousGoldAmount = 0;

    public Text playerDmg;
    public Text playerHp;
    public Text highestLvlClearedText;


    async void Start() {
        
        previousGoldAmount = PlayerPrefs.GetInt("gold");
        token = PlayerPrefs.GetString("token");

        ws = new WebSocket("wss://dungeon.ygstr.com");

         ws.OnOpen += () => {
             SetVisualStatus("Logging in...", new Color32(255, 173, 69, 255));
             Login();
         };

        ws.OnMessage += (bytes) => {
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            SocketPackage package = JsonUtility.FromJson<SocketPackage>(message);

        switch (package.identifier) {

            case "items_index":
                OnItemsIndex(package.data);
                break;

            case "new_login":
                OnNewLogin(package.data);
                break;

            case "logged_in":
                OnLogin(package.data);
                break;

            case "inventory":
                OnInventory(package.data);
                break;

            case "market_front":
                market.OnMarketFront(package.data);
                break;
            case "listings":
                market.OnListings(package.data);
                break;
            case "choose_username":
                ChooseUsername();
                break;
            case "username_availability":
                OnUsernameAvailability(package.data);
                break;
            case "start_raid":
                onlineRaid.OnRaidGranted();
                break;
            case "loot_rarity":
                onlineRaid.OnLootDrop(Int32.Parse(package.data));
                break;
            case "post_raid_info":
                onlineRaid.OnRaidEnd(package.data);
                break;
            case "history":
                im.OnHistory(package.data);
                break;
            case "leaderboard":
                navigation.OnLeaderboard(package.data);
                break;
            case "player_model":
                navigation.OnInspectPlayer(package.data);
                break;
            }


        };


        ws.OnClose += (e) => {
            Debug.Log("Connection closed");
            SetVisualStatus("Offline", new Color32(240, 2, 34, 255));
            StartCoroutine(Reconnect());
        };

        await ws.Connect();
    }

    // Try to reconnect if it loses conection to the server
    IEnumerator Reconnect() {
        yield return new WaitForSeconds(1);
        ws.Connect();
    }

    public void OnUsernameAvailability(string json) {
        InputField usernameInput = chooseUsernamePopup.Find("UsernameInput").GetComponent<InputField>();

        UsernameAvailability availability = JsonConvert.DeserializeObject<UsernameAvailability>(json);
        if(availability.username == usernameInput.text) {
            Button submitButton = chooseUsernamePopup.Find("SubmitUsernameButton").GetComponent<Button>();
            
            submitButton.interactable = availability.available;

            submitButton.transform.Find("Text").GetComponent<Text>().text = availability.available ? "READY!" : "Username taken!";
        }
    }

    public void ChooseUsername() {

        Button submitButton = chooseUsernamePopup.Find("SubmitUsernameButton").GetComponent<Button>();
        submitButton.interactable = false;

        chooseUsernamePopup.gameObject.SetActive(true);

        InputField usernameInput = chooseUsernamePopup.Find("UsernameInput").GetComponent<InputField>();

        usernameInput.onValidateInput += (string input, int index, char character) => {
            if (!char.IsLetter(character)) return '\0';
            return character;
        };

        submitButton.onClick.AddListener(() => {
            Send("set_username", usernameInput.text);
            chooseUsernamePopup.gameObject.SetActive(false);
        });

        usernameInput.onValueChanged.AddListener((string input) => {
            submitButton.interactable = false;
            Text buttonText = submitButton.transform.Find("Text").GetComponent<Text>();
            if (input.Length < 3) buttonText.text = "Too short";
            else {
                buttonText.text = "Checking...";
                Send("check_username_availability", input);
            }
        });
    }

    // When the users account is not found or the client has no token
    public void OnNewLogin(string new_token) {
        // Save new login authentication token
        PlayerPrefs.SetString("token", new_token);
        token = new_token;
        Login();
    }

    // When the inventory is sent to the client
    void OnInventory(string json) {
        
        inventory = JsonConvert.DeserializeObject<Item[]>(json);
        Debug.Log("Downloaded Inventory");

        

        LoadInventory();
    }

    public void DressCharacter(List<Item> items) {

        while(playerContainer.childCount > 1) {
            DestroyImmediate(playerContainer.GetChild(playerContainer.childCount-1).gameObject);
        }

        foreach(Item item in items) {
            renderer.InitiateFinishedItem(item, playerContainer);
        }

        Debug.Log("Dressed character");
    }

    public void DressMyself() {
        List<Item> items = new List<Item>();

        foreach (Item item in inventory) {
            if (item.equipped == 1) {
                IndexedItem origin = GetIndexedItem(item.item);

                user.hp += origin.hp;
                user.dmg += origin.dmg;

                items.Add(item);
            }
        }

        playerDmg.text = user.dmg.ToString();
        playerHp.text = user.hp.ToString();

        DressCharacter(items);
    }

    public void ToggleEquip() {
        Debug.Log("Called toggle equip");
        Send("equip", im.inspectedItemId.ToString());
        im.CloseInspectWindow();
    }

    void LoadInventory() {

        // Clear inventory space
        while (inventorySpace.childCount > 0) {
            DestroyImmediate(inventorySpace.GetChild(0).gameObject);
        }
        
        // Loop through all items in inventory and render them
        for(int i = 0; i < inventory.Length; i++) {
            // Create a new empty item slot and place it in the inventory space
            GameObject itemSlot = Instantiate(inventorySlotPrefab, inventorySpace);
            // Get the origin indexed item (info about this item, such as rarity and prefab assset)
            Item item = inventory[i];
            IndexedItem origin = GetIndexedItem(item.item);

            itemSlot.GetComponent<Button>().onClick.AddListener(() => {
                im.Inspect(item);
            });


            // Set color of item border
            itemSlot.transform.Find("Border").GetComponent<Image>().color = origin.rarity.color;

            StartCoroutine(renderer.RenderItem(inventory[i], itemSlot.GetComponentInChildren<RawImage>()));
        }


        Debug.Log("Loaded inventory");

        DressMyself();
    }

    public IndexedItem GetIndexedItem(int id) {
        for(int i = 0; i < items.Length; i++) {
            if (items[i].id == id) return items[i];
        }
        return null;
    }

    void Login() {
        Send("login");
    }

    void OnItemsIndex(string json) {
        
        SerializedIndexedItem[] itemArray = JsonConvert.DeserializeObject<SerializedIndexedItem[]>(json);
        
        items = new IndexedItem[itemArray.Length];
        
        for(int i = 0; i < itemArray.Length; i++) {
            SerializedIndexedItem jsonItem = itemArray[i];
            IndexedItem item = new IndexedItem();
            item.id = jsonItem.id;
            item.name = jsonItem.name;
            item.description = jsonItem.description;
            item.type = jsonItem.type;
            item.rarity = rarities[jsonItem.rarity];
            item.model = Resources.Load<GameObject>("Items/" + jsonItem.model);
            item.hp = jsonItem.hp;
            item.dmg = jsonItem.dmg;


            item.pattern = jsonItem.pattern == 1;

            items[i] = item;          
        }

        Debug.Log("Loaded Indexed items");
    }

    public async void OnLogin(string data) {

        // Parse user info
        user = JsonUtility.FromJson<User>(data);

        if(user.gold == previousGoldAmount) goldAmountText.text = user.gold.ToString();
        
        highestLvlClearedText.text = "Lvl " + user.lvl;

        SetVisualStatus("Logged in as " + user.username, new Color32(14, 238, 140, 255));

        onlineRaid.UpdateRaidButtonText();
      
    }

    void SetVisualStatus(string status, Color32 color) {
        loggedInStatusText.text = status;
        loggedInStatusSymbol.color = color;
    }

    public void Send(string identifier) {
        Send(identifier, "");
    }

    public void Send(string identifier, string data) {
        SocketPackage package = new SocketPackage();
        package.identifier = identifier;
        package.data = data;
        package.token = token;
        string json = JsonUtility.ToJson(package);
        ws.SendText(json);
    }

    void Update()
    {

        #if !UNITY_WEBGL || UNITY_EDITOR
            ws.DispatchMessageQueue();
        #endif

        
        if(user != null && (previousGoldAmount != user.gold)) {
            
            if (previousGoldAmount > user.gold) previousGoldAmount--;
            else previousGoldAmount++;

            goldAmountText.text = previousGoldAmount.ToString();
            if(previousGoldAmount == user.gold) {
                PlayerPrefs.SetInt("gold", previousGoldAmount);
            }
        }
    }
}


public class SocketPackage {
    public string identifier;
    public string data;
    public string token;
};