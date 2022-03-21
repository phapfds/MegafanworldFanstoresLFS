using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirebaseWebGL.Scripts.FirebaseBridge;
public class FanroomFriendManager : MonoBehaviour
{
    [Header("Variable")]
    public static FanroomFriendManager inst;
    public static string userID;
    public FanstoreItemList fanroomItem = new FanstoreItemList();
    public string nameOwnerRoom;
    [Header("Objects reference")]
    public Dictionary<ItemType, int> numberItems = new Dictionary<ItemType, int>(); //
    [Header("ARTS REFERENCE")]
    public List<IdItem> idItems;
    public Dictionary<int, GameObject> itemDics = new Dictionary<int, GameObject>();
    [Header("STATUS POST MESSAGE")]
    public StatusPostManager statusPostManager;
    private void Awake()
    {
        inst = this;
        foreach (IdItem idItem in idItems)
        {
            itemDics.Add(idItem.id, idItem.item);
        }
#if UNITY_EDITOR
        foreach (GameObject gameObject_ in itemDics.Values)
        {
            gameObject_.SetActive(true);
        }
#endif

        ViewsManager.Instance.ChangeView(ViewType.FanroomFriendView);
        FirebaseDatabase.GetRawData("Users/" + userID + "/StatusFriend", gameObject.name, "LoadStatusData", null);
        FanroomDatabase.ins.GetData(userID, gameObject, "LoadFanroomData_", "FallBack");
        FirebaseDatabase.GetRawData("Users/" + userID + "/UserInfo/name", gameObject.name, "Name", null);
        URLParameters.Instance.RegisterOnceOnDone((x) =>
        {
            if (URLParameters.Instance.SearchParameters.ContainsKey("email"))
            {
                ChatManager.inst.DisableChatSystem();
            }
        });
    }
    public void Name(string data)
    {
        nameOwnerRoom = data;
    }
    public void LoadStatusData(string data)
    {
        if (data != null && data != "" && data != null)
            statusPostManager.statusPost = JsonUtility.FromJson<StatusPost>(data);
    }
    public void LoadFanroomData_(string data)
    {
        if (data != null && data != "" && data != "null")
        {
            fanroomItem = JsonUtility.FromJson<FanstoreItemList>(data);
            foreach (Item item in fanroomItem.itemList)
            {
                itemDics[item.id].SetActive(true);
            }
        }
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

    public void Fallback(string data)
    {
        Debug.Log(data);
    }
}
