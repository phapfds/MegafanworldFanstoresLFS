using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryBuyItem
{
    public List<HistoryItem> items = new List<HistoryItem>();

}

[System.Serializable]
public class HistoryItem
{
    public string itemName;
    public ItemType itemType;
    public int id;
    public string buytime;
    public int cost;
    public int number;
    public HistoryItem(string itemName, ItemType itemType, int id, string buytime, int cost, int number)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.id = id;
        this.buytime = buytime;
        this.cost = cost;
        this.number = number;
    }

}
