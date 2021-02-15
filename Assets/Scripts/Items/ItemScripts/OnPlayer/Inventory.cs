using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<Item> itemsInInventory = new List<Item>();


    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] PlayerHealth playerHealth;

    private void Start()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PrintItemsInInventory();
    }


    void PrintItemsInInventory()
    {
        Debug.Log("Number of items : " + itemsInInventory.Count);
        for(int i = 0; i<itemsInInventory.Count; i++)
        {
            Debug.Log("Item : " + itemsInInventory[i].itemName + ", cost: " + itemsInInventory[i].cost + ", item rarity : " + itemsInInventory[i].itemRarity);
        }
    }

    public void AddItemStatsToPlayer(int damage, int health)
    {
        playerCombat.baseDamage += damage;
        playerHealth.maxHealth += health;
    }

    public void RemoveItemStatsFromPlayer(int damage, int health)
    {
        playerCombat.baseDamage -= damage;
        playerHealth.maxHealth -= health;
    }

    public void RemoveItemFromInventory(Item item)
    {
        itemsInInventory.Remove(item);
    }

    public void EquipItem(Item newItem)
    {
        itemsInInventory.Add(newItem);
        Debug.Log("Picked up " + newItem.itemName);
    }

}
