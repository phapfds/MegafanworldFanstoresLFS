using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryTradingDatabase : MonoBehaviour
{
    public static HistoryTradingDatabase ins;
    public HistoryBuyItem historyItem = new HistoryBuyItem();
    private void Awake()
    {
        ins = this;
        GetDataLocal();
    }

    private void Start()
    {
        //DeleteDataLocal();
        //DebugLogData();
    }
    public HistoryBuyItem GetDataLocal()
    {
        string json = PlayerPrefs.GetString("HistoryItemDatabase");
        if (json != null && json != "")
            historyItem = JsonUtility.FromJson<HistoryBuyItem>(json);
        //Debug.Log(json);
        return historyItem;
    }
    public void SaveDataLocal()
    {
        string db = JsonUtility.ToJson(historyItem);
        //Debug.Log(db);
        PlayerPrefs.SetString("HistoryItemDatabase", db);
    }
    public void DeleteDataLocal()
    {
        PlayerPrefs.DeleteKey("HistoryItemDatabase");
        historyItem.items.Clear();
    }
    public void DebugLogData()
    {
        foreach (HistoryItem item in historyItem.items)
        {
            Debug.Log(JsonUtility.ToJson(item));
        };
    }
}
