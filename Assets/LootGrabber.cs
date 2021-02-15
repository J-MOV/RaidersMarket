using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGrabber : MonoBehaviour
{

    Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if(other.transform.tag == "Loot")
        {
            Debug.Log("Trigger with loot");
            Loot loot = other.GetComponentInParent<Loot>();
            loot.LootItem();

            Debug.LogWarning("Loot picked up, but not working :)");
        }
    }
}
