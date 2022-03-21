using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirebaseWebGL.Scripts.FirebaseBridge;

[System.Serializable]
public class IdItem
{
    public int id;
    public GameObject item;
}


[System.Serializable]
public class FloorMatRef
{
    public Texture mainTexture;
    public Texture normalMap;
    public Texture metallicMap;
}

public class FanroomManager : MonoBehaviour
{
    public static FanroomManager inst;
    public Dictionary<ItemType, int> numberItems = new Dictionary<ItemType, int>(); //
    [Header("ARTS REFERENCE")]
    #region "References"
    public List<IdItem> idItems;
    public Dictionary<int, GameObject> itemDics = new Dictionary<int, GameObject>();
    #endregion
    [Header("POST MESSAGE")]
    public StatusPostManager statusPostManager;
    [Header("ARRANGE ITEMS")]
    [SerializeField] GameObject firstPersionCam;
    [SerializeField] GameObject upCam;
    [SerializeField] public List<DragObject> dragObjects;
    [SerializeField] GameObject ceil;
    public GameObject floor;
    [Header("Boundary")]
    public Transform left;
    public Transform right;
    public Transform top;
    public Transform bottom;
    [Header("FloorMaterial")]
    public List<Material> materialsFloor;
    public Renderer rendererFloor;
    public void Awake()
    {
        #region INSTANTIATE
        inst = this;
        ViewsManager.Instance.ChangeView(ViewType.FanroomView);
        foreach (IdItem idItem in idItems)
        {
            itemDics.Add(idItem.id, idItem.item);
        }
        #endregion
        #region EDITOR
#if UNITY_EDITOR
        EditorTest();
#endif
        #endregion
        #region FLOOR MAT
        try
        {
            int floorMatKey = PlayerPrefs.GetInt("FloorMatKey", 0);
            ChooseMatFloor(floorMatKey);
        }
        catch
        {
            ChooseMatFloor(0);
        }
        #endregion
        #region OLD VERS
        //foreach (Item item in FanroomDatabase.ins.fanroomItem.itemList)
        //{
        //    if (numberItems.ContainsKey(item.itemType))
        //        numberItems[item.itemType] += 1;
        //    else
        //        numberItems.Add(item.itemType, 1);
        //}
        //foreach (ItemType itemType in numberItems.Keys)
        //{
        //    switch (itemType)
        //    {
        //        case ItemType.WallPainting:
        //            art_0.SetActive(FanroomDatabase.ins.SearchItem(6));
        //            art_1.SetActive(FanroomDatabase.ins.SearchItem(7));
        //            art_2.SetActive(FanroomDatabase.ins.SearchItem(8));
        //            art_3.SetActive(FanroomDatabase.ins.SearchItem(10));
        //            art_4.SetActive(FanroomDatabase.ins.SearchItem(11));
        //            art_5.SetActive(FanroomDatabase.ins.SearchItem(18));
        //            art_6.SetActive(FanroomDatabase.ins.SearchItem(19));
        //            art_7.SetActive(FanroomDatabase.ins.SearchItem(20));

        //            break;
        //        case ItemType.AutographCard:
        //            autographCard.SetActive(true);
        //            break;
        //        case ItemType.Shoes:
        //            shoes.gameObject.SetActive(shoes != null);
        //            break;
        //        case ItemType.Ball:
        //            ball.gameObject.SetActive(ball != null);
        //            break;
        //        case ItemType.Couches:
        //            couche.SetActive(couche != null);
        //            break;
        //        case ItemType.Table:
        //            table.SetActive(table != null);
        //            break;
        //        case ItemType.Sofa:
        //            workDesk.SetActive(workDesk != null);
        //            break;
        //        case ItemType.Hat:
        //            hat.SetActive(hat != null);
        //            break;
        //        case ItemType.Lamp:
        //            lamp.SetActive(lamp != null);
        //            break;
        //        case ItemType.Cup:
        //            cup.SetActive(cup != null);
        //            break;
        //        case ItemType.Bembel:
        //            bembel.SetActive(bembel != null);
        //            break;
        //        case ItemType.WaterBottle:
        //            bottle.SetActive(bottle != null);
        //            break;
        //        case ItemType.Shirt:
        //            shirt.SetActive(shirt != null);
        //            break;
        //        default:

        //            break;
        //    }
        //}
        #endregion
        #region ENABLE ITEMS
        foreach (Item item in FanroomDatabase.ins.fanroomItem.itemList)
        {
            itemDics[item.id].SetActive(true);
        }
        #endregion
        #region FRSTATUS
        FirebaseDatabase.GetRawData("Users/" + UserInfoManager.Instance.userInfo.userID + "/StatusFriend", gameObject.name, "LoadStatusData", null);
        #endregion
    }
    public void ChooseMatFloor(int index)
    {
        rendererFloor.material = materialsFloor[index];
    }
    public void ChangeToMoveItemsView()
    {
        ViewsManager.Instance.ChangeView(ViewType.MoveItemView);
        firstPersionCam.SetActive(false);
        ceil.SetActive(false);
        upCam.SetActive(true);
        foreach (DragObject dr in dragObjects)
        {
            if (dr != null)
                dr.canDrag = true;
        }
    }
    public void SavePosition()
    {
        foreach (DragObject dr in dragObjects)
        {
            if (dr != null)
            {
                PlayerPrefs.SetString(UserInfoManager.Instance.userInfo.userID + dr.name, dr.transform.position.ToString());
                PlayerPrefs.SetString(UserInfoManager.Instance.userInfo.userID + dr.name + "rotation", dr.transform.eulerAngles.ToString());
                foreach (CheckContainer checkContainer1 in dr.gameObject.GetComponentsInChildren<CheckContainer>())
                {
                    if (checkContainer1 != null)
                        checkContainer1.SavePosition();
                }
            }
        }
    }
    public void ChangeToFanroomView()
    {
        ViewsManager.Instance.ChangeView(ViewType.FanroomView);
        firstPersionCam.SetActive(true);
        upCam.SetActive(false);
        ceil.SetActive(true);
        foreach (DragObject dr in dragObjects)
        {
            if (dr != null)
                dr.canDrag = false;
        }
    }
    public void LoadStatusData(string data)
    {
        if (data != null && data != "" && data != null)
            statusPostManager.statusPost = JsonUtility.FromJson<StatusPost>(data);
    }
    #region EDITOR
#if UNITY_EDITOR
    public void EditorTest()
    {
        firstPersionCam.SetActive(false);
        ceil.SetActive(false);
        upCam.SetActive(true);
        foreach(GameObject gameObject_ in itemDics.Values)
        {
            gameObject_.SetActive(true);
        }
 
        foreach (DragObject dr in dragObjects)
        {
            if (dr != null)
            {
                dr.gameObject.GetComponent<DragObject>().enabled = true;
                dr.canDrag = true;
            }
        }
    }
#endif
    #endregion
    public void SelectedObjectToRotate(string name)
    {
        foreach (DragObject dragObject in dragObjects)
        {
            if (dragObject != null)
            {
                dragObject.isSelectedRotate = dragObject.name == name;
            }
        }
    }

}
