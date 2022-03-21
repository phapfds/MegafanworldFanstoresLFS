using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FanstoreManager : MonoBehaviour
{
    public static FanstoreManager inst;
    public Action<int> ShowItemDetail;
    public List<Item> itemsInCart = new List<Item>();
    private void Awake()
    {
        inst = this;
        ViewsManager.Instance.ChangeView(ViewType.FanstoreView);
    }
}
