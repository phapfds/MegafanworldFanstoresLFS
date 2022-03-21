using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using FirebaseWebGL.Scripts.FirebaseBridge;
public class FanroomView : Views
{
    public Text credits;
    [SerializeField] List<Button> buttons;
    [SerializeField] GameObject joystick;
    private static bool isCategoryOpen = false;
    [Header("CATEGORY REF")]
    public Image category;
    public Image hideButton;
    public Image showButton;
    public Transform contentCategory;
    [SerializeField] Image tradingHistory;
    [SerializeField] ScrollRect scrollRectTradingHistory;
    public Transform contentHistory;
    public HistoryItemObject itemHistoryPrefab;
    public Text numberprefab;
    [SerializeField] Transform openPos;
    [SerializeField] Transform hidePos;
    [Header("EMAIL INVITE")]
    [SerializeField] GameObject emailInvite;
    [SerializeField] InputField inputFieldEmail;
    [SerializeField] InviteNotice noticePrefab;
    [SerializeField] Transform noticeContent;
    private List<string> usersidInvited = new List<string>();
    [SerializeField] GameObject redNotice;
    [SerializeField] GameObject viewport;
    [Header("POST STATUS")]
    [SerializeField] GameObject postStatusMessage;
    [SerializeField] GameObject postStatus;
    [SerializeField] StatusUI prefabStatusUI;
    [SerializeField] Transform contentPostMessage;
    [SerializeField] ScrollRect scrollRect;
    [Header("CHOOSE CATEGORY")]
    [SerializeField] GameObject itemCategory;
    [SerializeField] GameObject furnitureCategory;
    [SerializeField] GameObject galleryCategory;
    [SerializeField] GameObject itemOnIcon;
    [SerializeField] GameObject itemOffIcon;
    [SerializeField] GameObject funitureOnIcon;
    [SerializeField] GameObject funirureOffIcon;
    [SerializeField] GameObject galleryOnIcon;
    [SerializeField] GameObject galleryOffIcon;
    [Header("CHOOSE TEXTURE")]
    [SerializeField] GameObject chooseArt;
    [SerializeField] GameObject contentChooseArt;
    [Header("MOVE OBJECT")]
    [SerializeField] GameObject doneOrCancelMoveObjectUI;
    [Header("REMOVE OBJECT")]
    [SerializeField] List<string> iconItemName = new List<string>();
    public override void SetUp()
    {
        base.SetUp();
        //ButtonInteract(true);
        credits.text = UserInfoManager.Instance.userInfo.coinsNum.ToString() + " credits";
        StartCoroutine(InventoryOpen());

        FirebaseDatabase.GetRawData("SocialNetwork/InviteToFanroom/" + UserInfoManager.Instance.userInfo.email.Replace(".", "%2E"), gameObject.name, "DisplayInfo", null);
        StartCoroutine(NoticeOpen());
        StartCoroutine(PostMessageOpen());
        itemCategory.SetActive(true);
        galleryCategory.SetActive(true);
        furnitureCategory.SetActive(true);
    }
    public void DisplayInfo(string data)
    {
        if (data != null & data != "null")
            usersidInvited = data.Split(',').ToList();
    }
    public void ChangeToMoveItemsView()
    {
        FanroomManager.inst.ChangeToMoveItemsView();
    }
    IEnumerator InventoryOpen()
    {
        yield return new WaitForEndOfFrame();
        #region OLD VER
        /*      foreach (ItemType itemType in FanroomManager.inst.numberItems.Keys)
              {
                  GameObject gb = null;
                  switch (itemType)
                  {
                      case ItemType.Bembel:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Bembel"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.bembel;
                          break;
                      case ItemType.Cup:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Cup"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.cup;
                          break;
                      case ItemType.Hat:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Hat"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.hat;
                          break;
                      case ItemType.Lamp:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Lamp"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.lamp;
                          break;
                      case ItemType.Shirt:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Shirt"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.shirt;
                          break;
                      case ItemType.WaterBottle:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/WaterBottle"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.bottle;
                          break;
                      //case ItemType.WallPainting:
                      //  gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/WallPainting_6"));
                      //    break;
                      case ItemType.AutographCard:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/AutographCard"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.autographCard;
                          break;
                      case ItemType.Ball:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Ball"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.ball;
                          break;
                      case ItemType.Shoes:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Shoes"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.shoes;
                          break;
                      case ItemType.Couches:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Couch"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.workDesk;
                          break;
                      case ItemType.Table:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Table"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.table;
                          break;
                      case ItemType.Sofa:
                          gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Sofa"));
                          gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.couche;
                          break;
                      default:
                          break;
                  }
                  if (itemType == ItemType.Couches || itemType == ItemType.Table || itemType == ItemType.Sofa)
                  {
                      gb.transform.SetParent(furnitureCategory.transform);

                  }
                  else if (itemType == ItemType.WallPainting)
                  {
                      //gb.transform.SetParent(galleryCategory.transform);
                  }
                  else
                  {
                      gb.transform.SetParent(contentCategory);
                  }
                  if (gb != null)
                  {
                      if (FanroomManager.inst.numberItems[itemType] > 1)
                      {
                          var number = Instantiate<Text>(numberprefab, gb.transform);
                          number.text = FanroomManager.inst.numberItems[itemType].ToString();
                      }
                  }
              }

              foreach (Item item in FanroomDatabase.ins.fanroomItem.itemList)
              {
                  if (item.itemType == ItemType.WallPainting)
                  {
                      if (!iconArtName.Contains("WallPainting_" + item.id.ToString()))
                      {
                          GameObject gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/WallPainting_" + item.id.ToString()));
                          gb.transform.SetParent(galleryCategory.transform);
                          iconArtName.Add("WallPainting_" + item.id.ToString());
                      };
                      //GameObject gb_chooseArt = Instantiate<GameObject>(gb, contentChooseArt.transform);
                      //gb.GetComponent<Button>().enabled = false;
                  }
              }*/
        #endregion
        foreach (Item item in FanroomDatabase.ins.fanroomItem.itemList)
        {
            if (!iconItemName.Contains(item.id.ToString()))
            {
                GameObject gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/" + item.id.ToString()));
                switch (item.itemType)
                {
                    case ItemType.WallPainting:
                        gb.transform.SetParent(galleryCategory.transform);
                        break;
                    case ItemType.Couches:
                        gb.transform.SetParent(furnitureCategory.transform);
                        break;
                    case ItemType.Sofa:
                        gb.transform.SetParent(furnitureCategory.transform);
                        break;
                    case ItemType.Table:
                        gb.transform.SetParent(furnitureCategory.transform);
                        break;
                    case ItemType.Carpet:
                        gb.transform.SetParent(furnitureCategory.transform);
                        break;
                    case ItemType.Shelf:
                        gb.transform.SetParent(furnitureCategory.transform);
                        break;
                    case ItemType.Chair:
                        gb.transform.SetParent(furnitureCategory.transform);
                        break;
                    default:
                        gb.transform.SetParent(contentCategory);
                        break;
                }
                Debug.Log(item.nameItem.ToString());
                gb.GetComponent<ItemIcon>().gameObject_ = FanroomManager.inst.itemDics[item.id];
                iconItemName.Add(item.id.ToString());
            }
        }
    }
    IEnumerator PostMessageOpen()
    {
        yield return new WaitForSeconds(1);
        //Debug.Log(FanroomManager.inst.statusPostManager.statusPost.status.Count);
        foreach (Status st in FanroomManager.inst.statusPostManager.statusPost.status)
        {
            var x = Instantiate<GameObject>(prefabStatusUI.gameObject, contentPostMessage);
            x.GetComponent<StatusUI>()._name.text = st.name;
            x.GetComponent<StatusUI>()._text.text = st.text;
        }
        yield return new WaitForEndOfFrame();
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
    public void ShowHideStatus(bool show)
    {
        postStatus.SetActive(show);
    }
    IEnumerator NoticeOpen()
    {
        yield return new WaitForSecondsRealtime(1);
        if (usersidInvited.Count > 0)
        {
            redNotice.SetActive(true);
            foreach (string id in usersidInvited)
            {
                InviteNotice notice = Instantiate<InviteNotice>(noticePrefab, noticeContent);
                notice.name += id;
                notice.userID = id;
            };
        }
        if (FanroomManager.inst.statusPostManager.statusPost.status.Count > 0)
        {
            redNotice.SetActive(true);
            Instantiate<GameObject>(postStatusMessage, noticeContent).SetActive(true);
        }

    }
    public override void OnStart()
    {
        base.OnStart();
    }
    public void ShowNotice(Image notice)
    {
        notice.gameObject.SetActive(true);
        //ButtonInteract(false);
    }
    public void OffNotice(Image notice)
    {
        notice.gameObject.SetActive(false);
        //ButtonInteract(true);
    }
    public void AnswerYes(bool setUpCharacter)
    {
        if (setUpCharacter)
            SetupManager.SetUpView(ViewType.SetupView);
        else
            SetupManager.SetUpView(ViewType.SelectCityView);
        ViewsManager.Instance.LoadSceneByName("Setup");
    }
    public void IAPView()
    {
        ViewsManager.Instance.ChangeView(ViewType.FanroomView, ViewType.IAPView);
    }
    public void LeaderBoardView()
    {
        ViewsManager.Instance.ChangeView(ViewType.FanroomView, ViewType.LeaderBoardView);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenHideCategory()
    {
        if (isCategoryOpen)
        {
            category.transform.DOMoveX(hidePos.position.x, 0.1f);
            hideButton.rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
            showButton.gameObject.SetActive(true);
            hideButton.gameObject.SetActive(false);
            isCategoryOpen = false;
        }
        else
        {
            category.transform.DOMoveX(openPos.position.x, 0.1f);
            showButton.gameObject.SetActive(false);
            hideButton.gameObject.SetActive(true);
            isCategoryOpen = true;
        }
    }
    public void GoToFanstore(string nameScene)
    {
        ViewsManager.Instance.LoadSceneByName(nameScene);
    }
    public void OpenHideHistory(bool open)
    {
        tradingHistory.gameObject.SetActive(open);
        if (open)
        {
            foreach (HistoryItem item in HistoryTradingDatabase.ins.historyItem.items)
            {
                var gb = Instantiate<HistoryItemObject>(itemHistoryPrefab, contentHistory);
                gb.itemName.text = item.itemName.ToUpper();
                gb.buytime.text = item.buytime;
                gb.cost.text = item.cost.ToString();
                gb.buytime.text = item.buytime;
                gb.gameObject.SetActive(true);
            }
            StartCoroutine(ScrollToBottom());
        }
    }
    public void OpenEmailInvite()
    {
        emailInvite.gameObject.SetActive(!emailInvite.activeSelf);
    }
    public void OpenHideNoticeInvite(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void SendInvite()
    {
        if (inputFieldEmail.text.Contains("@"))
        {
            OpenEmailInvite();

            //FirebaseDatabase.GetRawData("SocialNetwork/InviteToFanroom/" + inputFieldEmail.text.Replace(".", "%2E"), gameObject.name, "WriteUserID", null);
            FirebaseDatabase.PostJSON("SocialNetwork/InviteToFanroom/" + inputFieldEmail.text.Replace(".", "%2E"), UserInfoManager.Instance.userInfo.userID, gameObject.name, "ResetEmailUI", null);
        }
    }
    public void WriteUserID(string data)
    {
        Debug.Log(data);
        if (data != null && data != "null")
        {
            data += "," + UserInfoManager.Instance.userInfo.userID;
        }
        else
        {
            data = UserInfoManager.Instance.userInfo.userID;
        }
        FirebaseDatabase.PostJSON("SocialNetwork/InviteToFanroom/" + inputFieldEmail.text.Replace(".", "%2E"), data, gameObject.name, "ResetEmailUI", null);
    }
    public void ResetEmailUI(string data)
    {
        inputFieldEmail.text = "";

    }
    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRectTradingHistory.normalizedPosition = new Vector2(0, 0);
    }
    public void ChooseCategory(string categoryType)
    {
        switch (categoryType)
        {
            case "Item":
                itemOffIcon.SetActive(false);
                funirureOffIcon.SetActive(true);
                galleryOffIcon.SetActive(true);
                break;
            case "Gallery":
                itemOffIcon.SetActive(true);
                funirureOffIcon.SetActive(true);
                galleryOffIcon.SetActive(false);
                break;
            case "Furniture":
                itemOffIcon.SetActive(true);
                funirureOffIcon.SetActive(false);
                galleryOffIcon.SetActive(true);
                break;
        }
        ChangeIcon();
    }
    public void ChangeIcon()
    {
        funitureOnIcon.SetActive(!funirureOffIcon.activeSelf);
        itemOnIcon.SetActive(!itemOffIcon.activeSelf);
        galleryOnIcon.SetActive(!galleryOffIcon.activeSelf);

        itemCategory.SetActive(itemOnIcon.activeSelf);
        furnitureCategory.SetActive(funitureOnIcon.activeSelf);
        galleryCategory.SetActive(galleryOnIcon.activeSelf);

    }
    public void OpenChooseArt()
    {
        chooseArt.SetActive(!chooseArt.activeSelf);
    }
    private void OnDisable()
    {
        for (int i = 0; i < contentCategory.childCount; i++)
        {
            Destroy(contentCategory.GetChild(i).gameObject);
        }

        for (int i = 0; i < noticeContent.childCount; i++)
        {
            usersidInvited.Clear();
            Destroy(noticeContent.GetChild(i).gameObject);
        }

        for (int i = 0; i < contentPostMessage.childCount; i++)
        {
            Destroy(contentPostMessage.GetChild(i).gameObject);
        }

        for (int i = 0; i < furnitureCategory.transform.childCount; i++)
        {
            Destroy(furnitureCategory.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < galleryCategory.transform.childCount; i++)
        {
            Destroy(galleryCategory.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < contentChooseArt.transform.childCount; i++)
        {
            Destroy(contentChooseArt.transform.GetChild(i).gameObject);
        }
        redNotice.gameObject.SetActive(false);
        viewport.SetActive(false);

        iconItemName.Clear();
    }
    public void OpenUIMoveObject()
    {
        doneOrCancelMoveObjectUI.SetActive(true);
    }
    public void DoneMoveObject()
    {
        MoveObjectManage.ins.FreeIt();
        doneOrCancelMoveObjectUI.SetActive(false);
    }
    public void CancelMoveObject()
    {
        MoveObjectManage.ins.CancelMove();
        doneOrCancelMoveObjectUI.SetActive(false);
    }
}
