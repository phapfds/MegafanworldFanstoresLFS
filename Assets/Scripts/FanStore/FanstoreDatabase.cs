using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanstoreDatabase : MonoBehaviour
{

    public static FanstoreDatabase ins;
    public FanstoreItemList fanstoreItem = new FanstoreItemList();
    [SerializeField] List<Item> itemInit;
    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        //DeleteDataLocal();
        if (GetDataLocal().itemList.Count < 32)
        {
            InitData();
        };
    }
    #region "Local"
    public FanstoreItemList InitData()
    {
        Debug.Log("Init fanstore data");
        DeleteDataLocal();
        foreach (Item item in itemInit)
        {
            fanstoreItem.itemList.Add(item);
        }
        SaveDataLocal();
        return fanstoreItem;
    }
    public FanstoreItemList GetDataLocal()
    {
        string json = PlayerPrefs.GetString("FanStoreDatabase");
        if (json != null && json != "")
            fanstoreItem = JsonUtility.FromJson<FanstoreItemList>(json);
        //Debug.Log(json);
        return fanstoreItem;
    }
    public void SaveDataLocal()
    {
        string db = JsonUtility.ToJson(fanstoreItem);
        PlayerPrefs.SetString("FanStoreDatabase", db);
    }
    public void DeleteDataLocal()
    {
        PlayerPrefs.DeleteKey("FanStoreDatabase");
        PlayerPrefs.DeleteAll();
        fanstoreItem.itemList.Clear();
    }
    public void DebugLogData()
    {
        foreach (Item item in fanstoreItem.itemList)
        {
            Debug.Log(JsonUtility.ToJson(item));
        };
    }
    public int SearchItem(int id)
    {
        int id_ = 0;
        foreach (Item item in fanstoreItem.itemList)
        {
            if (item.id == id)
            {
                id_ = id;
            }
        }
        return id_;
    }
    #endregion
}
