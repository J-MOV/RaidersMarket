using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGrabber : MonoBehaviour
{
    LootCollected inGameInventory;

    private void Start()
    {
        inGameInventory = FindObjectOfType<LootCollected>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Loot>())
        other.GetComponent<Loot>().AddToInventory(inGameInventory);
    }
}
