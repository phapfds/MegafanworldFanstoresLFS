using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using FirebaseWebGL.Scripts.FirebaseBridge;
//using Newtonsoft.Json;

public class FanroomDatabase : MonoBehaviour
{
    public static FanroomDatabase ins;
    public FanstoreItemList fanroomItem = new FanstoreItemList();
    private void Awake()
    {
        ins = this;
    }

    public void GetData(GameObject gameObject, string callback, string fallback)
    {
        FirebaseDatabase.GetFanroomData(UserInfoManager.Instance.userInfo.userID, gameObject.name, callback, fallback);
    }

    public void GetData(string userID, GameObject gameObject, string callback, string fallback)
    {
        FirebaseDatabase.GetFanroomData(userID, gameObject.name, callback, fallback);
    }

    public void SaveData()
    {
        string db = JsonUtility.ToJson(fanroomItem);
        FirebaseDatabase.PostFanroomData(UserInfoManager.Instance.userInfo.userID, db, gameObject.name, null, null);
    }
    public void ClearData()
    {
        fanroomItem.itemList.Clear();
    }
    public void DebugLogData()
    {
        foreach (Item item in fanroomItem.itemList)
        {
            Debug.Log(JsonUtility.ToJson(item));
        };
    }
    public bool SearchItem(int id)
    {
        int id_ = 0;
        foreach (Item item in fanroomItem.itemList)
        {
            if (item.id == id)
            {
                id_ = id;
            }
        }
        return id_ == id;
    }

    public void ReadUserIDsListInvited(Action<string> Callback)
    {
        //FBDatabase.ReadUserIDsInvitedToFanroom(UserInfoManager.Instance.userInfo.email, (data) =>
        //{
        //    Callback(data);
        //});
    }
}
