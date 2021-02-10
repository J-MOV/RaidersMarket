
public class Item
{
    public string itemName = "Item";
    public int cost = 0;
    public ItemRarity itemRarity;

    public Item(ItemStatsSO _itemStats)
    {
        itemName = _itemStats.itemName;
        cost = _itemStats.itemCost;
        itemRarity = _itemStats.itemRarity;
    }
}
