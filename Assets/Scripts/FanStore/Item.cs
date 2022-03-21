using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Hat,
    Cup,
    Lamp,
    Bembel,
    WaterBottle,
    Shirt,
    WallPainting,
    AutographCard,
    Shoes,
    Ball,
    Table,
    Couches,
    Sofa,
    Workdesk,
    Carpet,
    Shelf,
    Chair
}

[System.Serializable]
public class Item
{
    public int id;
    public ItemType itemType;
    public string nameItem;
    public string description;
    public int price;
    public int number;
    //public Texture image;
    public Item(int id, ItemType itemType, string nameItem, string description, int price, int number)
    {
        this.id = id;
        this.itemType = itemType;
        this.nameItem = nameItem;
        this.description = description;
        this.price = price;
        this.number = number;
    }
    public Item()
    {

    }
}
