using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFront {
    public Item item;
    public int amount;
}

public class ItemListing {
    public Item item;
    public string seller;
}

public class ItemListings {
    public ItemListing[] items;
    public string lowestSoldFor;
    public string lastSoldFor;
}

public class MarketManager : MonoBehaviour
{
    public InspectManager inspectManager;

    public Transform marketFrontScrollContent;
    public GameObject marketFrontEntryPrefab;

    public Transform listingsContent;
    public GameObject listingPrefab;


    public InventoryNavigation navigation;
    public OnlineConnection connection;

    public ItemRenderer renderer;

    ItemFront[] itemFrontData;

    public Text lastSold;
    public Text lowestSold;

    public Text itemName;
    public Text itemRarity;
    public Text listingsTitle;

    public int activeItemListing = -1;


    public void OnListings(string data) {
        ItemListings listings = JsonConvert.DeserializeObject<ItemListings>(data);
        IndexedItem origin = connection.GetIndexedItem(listings.items[0].item.item);

        lowestSold.text = listings.lowestSoldFor;
        lastSold.text = listings.lastSoldFor;
        itemName.text = origin.name;
        itemRarity.text = origin.rarity.title.ToUpper();
        itemRarity.color = origin.rarity.color;
        listingsTitle.text = "Listings for " + origin.name + ", sorted by cheapest";

        ClearTransform(listingsContent);

        RawImage firstThumb = null;

        for(int i = 0; i < listings.items.Length; i++) {

            ItemListing listing = listings.items[i];
            
            Transform listingObject = Instantiate(listingPrefab, listingsContent).transform;

            RawImage thumbnail = listingObject.Find("Thumbnail").GetComponent<RawImage>();
            if (i == 0) firstThumb = thumbnail;

            if(!origin.pattern && i > 0) {
                thumbnail.texture = firstThumb.texture;
            } else {
                StartCoroutine(renderer.RenderItem(listing.item, thumbnail));
            }

            

            listingObject.Find("Seller").GetComponent<Text>().text = listing.seller;
            listingObject.Find("ListingPrice").GetComponent<Text>().text = listing.item.price.ToString();
            listingObject.GetComponent<Button>().onClick.AddListener(() => {
                inspectManager.Inspect(listing.item);
            });
        }
    }


    public void OnMarketFront(string data) {

        ClearTransform(marketFrontScrollContent);

        itemFrontData = JsonConvert.DeserializeObject<ItemFront[]>(data);
        foreach(ItemFront frontEntry in itemFrontData) {
            IndexedItem origin = connection.GetIndexedItem(frontEntry.item.item);
            Transform entry = Instantiate(marketFrontEntryPrefab, marketFrontScrollContent).transform;

            RawImage thumbnail = entry.Find("Thumbnail").GetComponent<RawImage>();
            StartCoroutine(renderer.RenderItem(frontEntry.item, thumbnail));

            entry.Find("ItemTitle").GetComponent<Text>().text = origin.name;
            entry.Find("ItemBorder").GetComponent<Image>().color = origin.rarity.color;
            entry.Find("AmountListed").GetComponent<Text>().text = frontEntry.amount + " listed";
            entry.Find("CheapestPriceListed").GetComponent<Text>().text = frontEntry.item.price.ToString();

            entry.GetComponent<Button>().onClick.AddListener(() => {
                OpenMarketListings(origin.id);
            });
        }

        if(itemFrontData.Length > 0) {
            if (activeItemListing == -1 || !IsItemIndexListed(activeItemListing)) activeItemListing = itemFrontData[0].item.item;
            OpenMarketListings(activeItemListing);
        }
       
    }

    public bool IsItemIndexListed(int indexItemId) {
        foreach(ItemFront frontEntry in itemFrontData) {
            if (frontEntry.item.item == indexItemId) return true;
        }
        return false;
    }


    public void OpenMarketListings(int item) {
        activeItemListing = item;
        connection.Send("get_listings", item.ToString());
    }

    void ClearTransform(Transform parent) {
        while(parent.childCount > 0) {
            DestroyImmediate(parent.GetChild(0).gameObject);
        }
    }

    public void UpdateMarketFront() {
        connection.Send("get_market_front");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
