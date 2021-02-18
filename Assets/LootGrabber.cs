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
        if(other.CompareTag("Loot"))
        {
            Loot loot = other.GetComponentInParent<Loot>();
            loot.LootItem();
            Destroy(other.gameObject);

        }
    }
}
