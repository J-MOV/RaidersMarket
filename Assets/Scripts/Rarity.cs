using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Rarity
{
    
    public string title;
    public Color32 color;

    public Rarity(string title, Color32 color) {

        this.title = title;
        this.color = color;
    }
}
