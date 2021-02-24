using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHistory {
    public MarketTransaction[] transactions;
    public string creator;
    public DateTime created;
}
public class MarketTransaction {
    public string seller, buyer;
    public int item, price, id;
    public DateTime date;
}
