using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ItemToCart : MonoBehaviour
{
    public Text nameItem;
    public Text price;
    public Text number;
    public int id;
    public void RemoveItem()
    {

        FanstoreView fanstoreView = ViewsManager.Instance.currentView as FanstoreView;
        fanstoreView.RemoveItem(Convert.ToInt32( price.text.Remove(price.text.Length-8)));

        FanstoreManager.inst.itemsInCart.Remove(FanstoreDatabase.ins.fanstoreItem.itemList[id]);
        Destroy(this.gameObject);
    }
}
