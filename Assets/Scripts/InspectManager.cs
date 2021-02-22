/**
 * This script manages the inspect function for all items, in inventory and on the market.
 * This includes the sell menu, and equip options.
 */

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectManager : MonoBehaviour
{

    public Transform inspectWindow;
    public RotateInspectedItem rotateManager;
    public OnlineConnection connection;
    public ItemRenderer renderer;

    public Text itemName;
    public Text itemRarity;
    public Text itemDescription;
    public Text itemType;

    public GameObject OwnedItemButtons;
    public GameObject ListedItemButtons;
    public GameObject OwnListingButtons;

    public int inspectedItemId;

    public Transform createListingWindow;

    void Start()
    {
        
    }

    public void CloseInspectWindow() {
        inspectWindow.gameObject.SetActive(false);
        rotateManager.inspecting = false;
    }

    public void Inspect(Item item) {

        inspectedItemId = item.id;

        foreach(GameObject buttons in new GameObject[] {OwnedItemButtons, ListedItemButtons, OwnListingButtons }) {
            buttons.SetActive(false);
        }

        IndexedItem origin = connection.GetIndexedItem(item.item);

        renderer.InitiateRenderItem(item);

        itemName.text = origin.name;
        itemRarity.text = origin.rarity.title.ToUpper();
        itemRarity.color = origin.rarity.color;

        itemDescription.text = origin.description;
        itemType.text = char.ToUpper(origin.type[0]) + origin.type.Substring(1);

        if(item.owner == connection.user.id && item.for_sale == 0) {
            OwnedItemButtons.SetActive(true);
            Transform equipButton = OwnedItemButtons.transform.Find("EquipButton");
            // Set the text of the equip button
            equipButton.GetComponentInChildren<Text>().text = item.equipped == 1 ? "UNEQUIP" : "EQUIP";

            Button sellButton = OwnedItemButtons.transform.Find("SellButton").GetComponent<Button>();

            sellButton.onClick.RemoveAllListeners();
            sellButton.onClick.AddListener(() => {
                CloseInspectWindow();
                OpenCreateListingWindow(item);
            });

        } else if (item.owner == connection.user.id && item.for_sale == 1) {
            OwnListingButtons.SetActive(true);

            Button removeListingButton = OwnListingButtons.transform.Find("RemoveListingButton").GetComponent<Button>();

            removeListingButton.onClick.RemoveAllListeners();
            removeListingButton.onClick.AddListener(() => {
                connection.Send("remove_listing", item.id.ToString());
                CloseInspectWindow();
            });
        } else {;
            ListedItemButtons.SetActive(true);
            Button buyButton = ListedItemButtons.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.RemoveAllListeners();

            Text listedPrice = ListedItemButtons.transform.Find("ListingPrice").GetComponentInChildren<Text>();

            listedPrice.text = item.price.ToString();

            buyButton.interactable = item.price <= connection.user.gold;
            buyButton.onClick.AddListener(() => {
                connection.Send("buy", item.id.ToString());
                CloseInspectWindow();
            });
        }



            rotateManager.inspecting = true;
        inspectWindow.gameObject.SetActive(true);
    }

    public void OpenCreateListingWindow(Item item) {

        IndexedItem origin = connection.GetIndexedItem(item.item);

        createListingWindow.Find("ItemName").GetComponent<Text>().text = origin.name;

        Text rarityText = createListingWindow.Find("Rarity").GetComponent<Text>();
        rarityText.text = origin.rarity.title;
        rarityText.color = origin.rarity.color;

        InputField priceInput = createListingWindow.Find("PriceInput").GetComponent<InputField>();

        priceInput.text = 10.ToString();

        priceInput.onValidateInput = (string input, int index, char character) => {
            Debug.Log("Validate input");
            if (!char.IsNumber(character)) return '\0';
            return character;
        };

        Button createListingsButton =
        createListingWindow.Find("CreateListingButton").GetComponent<Button>();

        createListingsButton.onClick.RemoveAllListeners();
        createListingsButton.onClick.AddListener(() => {
            connection.Send("create_listing", JsonConvert.SerializeObject(new Dictionary<string, int> { { "item", item.id }, { "price", Int32.Parse(priceInput.text) } }));
            CancelListing();
        });

        createListingWindow.gameObject.SetActive(true);
    }

    public void CancelListing() {
        createListingWindow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
