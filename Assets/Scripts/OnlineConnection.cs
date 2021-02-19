

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NativeWebSocket;
using System.Threading.Tasks;
using Newtonsoft.Json;


public class OnlineConnection : MonoBehaviour
{

    public ItemRenderer renderer;

    public RawImage itemSlotTextReee;

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
    

    public Text goldAmountText;

    public Text loggedInStatusText;
    public Image loggedInStatusSymbol;

    

    async void Start()
    {

        
        token = PlayerPrefs.GetString("token");

        ws = new WebSocket("ws://localhost:8080");

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
            }

            
        };



        ws.OnClose += (e) => {
            SetVisualStatus("Offline", new Color32(240, 2, 34, 255));
            StartCoroutine(Reconnect());
        };

        await ws.Connect();
    }

    IEnumerator Reconnect() {
        yield return new WaitForSeconds(1);
        ws.Connect();
    }

    public void OnNewLogin(string new_token) {
        // Save new login authentication token
        PlayerPrefs.SetString("token", new_token);
        Login();
    }

    void OnInventory(string json) {
        
        inventory = JsonConvert.DeserializeObject<Item[]>(json);
        Debug.Log("Downloaded Inventory");

        LoadInventory();

    }

    void LoadInventory() {

        StartCoroutine(renderer.RenderItem(items[0].model, itemSlotTextReee));


        Debug.Log("Loaded inventory");
    }

    IndexedItem GetIndexedItem(int id) {
        for(int i = 0; i < items.Length; i++) {
            if (items[i].id == id) return items[i];
        }
        return null;
    }

    void Login() {
        Send("login", token);
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
  

            items[i] = item;
            
        }

        Debug.Log("Loaded Indexed items");
    }

    public async void OnLogin(string data) {

        SetVisualStatus("Logged in", new Color32(14, 238, 140, 255));

        // Parse user info
        user = JsonUtility.FromJson<User>(data);

        goldAmountText.text = user.gold.ToString();
    }


    

    void SetVisualStatus(string status, Color32 color) {
        loggedInStatusText.text = status;
        loggedInStatusSymbol.color = color;
    }

    void Send(string identifier, string data) {
        SocketPackage package = new SocketPackage();
        package.identifier = identifier;
        package.data = data;
        string json = JsonUtility.ToJson(package);
        ws.SendText(json);
    }

    void Update()
    {

        #if !UNITY_WEBGL || UNITY_EDITOR
            ws.DispatchMessageQueue();
        #endif
    }
}


public class SocketPackage {
    public string identifier;
    public string data;
};