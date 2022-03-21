using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using FirebaseWebGL.Scripts.FirebaseBridge;
public class FanroomFriendView : Views
{
    [Header("UI REFERENCE")]
    [SerializeField] List<Button> buttons;
    [SerializeField] GameObject joystick;
    public Button backButton;
    public Button postMessage;
    [Header("CATEGORY")]
    public Image category;
    private static bool isCategoryOpen = true;
    public Image hideButton;
    public Image showButton;
    public Transform contentCategory;
    public Text numberprefab;
    [SerializeField] Transform openPos;
    [SerializeField] Transform hidePos;

    [Header("POST MESSAGE")]
    [SerializeField] GameObject status;
    [SerializeField] InputField statusText;
    public Button sendStatus;
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

    [Header("Welcome")]
    [SerializeField] GameObject welcomePanel;
    [SerializeField] Text welcomeText;
    [Header("REMOVE OBJECT")]
    [SerializeField] List<string> iconItemName = new List<string>();
    public override void SetUp()
    {
        base.SetUp();
        StartCoroutine(InventoryOpen());
        StartCoroutine(PostMessageOpen());
        StartCoroutine(NameAssign());
    }
    IEnumerator NameAssign()
    {
        yield return new WaitForSeconds(1);
        welcomeText.text = "Welcome to "+ FanroomFriendManager.inst.nameOwnerRoom + "'s room!";
    }
    IEnumerator InventoryOpen()
    {
        yield return new WaitForSeconds(1);
        #region OLD VERSION
        //foreach (ItemType itemType in FanroomFriendManager.inst.numberItems.Keys)
        //{
        //    GameObject gb = null;
        //    switch (itemType)
        //    {
        //        case ItemType.Bembel:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Bembel"));
        //            break;
        //        case ItemType.Cup:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Cup"));
        //            break;
        //        case ItemType.Hat:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Hat"));
        //            break;
        //        case ItemType.Lamp:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Lamp"));
        //            break;
        //        case ItemType.Shirt:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Shirt"));
        //            break;
        //        case ItemType.WaterBottle:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/WaterBottle"));
        //            break;
        //        //case ItemType.WallPainting:
        //        //    gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/WallPainting"));
        //        //    break;
        //        case ItemType.AutographCard:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/AutographCard"));
        //            break;
        //        case ItemType.Ball:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Ball"));
        //            break;
        //        case ItemType.Shoes:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Shoes"));
        //            break;
        //        case ItemType.Couches:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Couch"));
        //            break;
        //        case ItemType.Table:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Table"));
        //            break;
        //        case ItemType.Sofa:
        //            gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/Sofa"));
        //            break;
        //        default:
        //            break;
        //    }
        //    if (itemType == ItemType.Couches || itemType == ItemType.Table || itemType == ItemType.Sofa)
        //    {
        //        gb.transform.SetParent(furnitureCategory.transform);

        //    }
        //    else
        //    {
        //        if (gb != null)
        //            gb.transform.SetParent(contentCategory);
        //    }
        //    if (gb != null)
        //    {
        //        if (FanroomFriendManager.inst.numberItems[itemType] > 1)
        //        {
        //            var number = Instantiate<Text>(numberprefab, gb.transform);
        //            number.text = FanroomFriendManager.inst.numberItems[itemType].ToString();
        //        }

        //    }
        //}
        //foreach (Item item in FanroomFriendManager.inst.fanroomItem.itemList)
        //{
        //    if (item.itemType == ItemType.WallPainting)
        //    {
        //        GameObject gb = null;
        //        gb = Instantiate<GameObject>(Resources.Load<GameObject>("ItemIcons/WallPainting_" + item.id.ToString()));
        //        gb.transform.SetParent(galleryCategory.transform);
        //    }
        //}
        #endregion
        foreach (Item item in FanroomFriendManager.inst.fanroomItem.itemList)
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
                iconItemName.Add(item.id.ToString());
            }
        }
    }
    IEnumerator PostMessageOpen()
    {
        yield return new WaitForSeconds(1);
        foreach (Status st in FanroomFriendManager.inst.statusPostManager.statusPost.status)
        {
            var x = Instantiate<GameObject>(prefabStatusUI.gameObject, contentPostMessage);
            x.GetComponent<StatusUI>()._name.text = st.name;
            x.GetComponent<StatusUI>()._text.text = st.text;
        }
        StartCoroutine(ScrollToBottom());
    }
    public override void OnStart()
    {
        base.OnStart();
    }
    public void ShowNotice(Image notice)
    {
        Time.timeScale = 0;
        notice.gameObject.SetActive(true);
        //ButtonInteract(false);
    }
    public void OffNotice(Image notice)
    {
        Time.timeScale = 1;
        notice.gameObject.SetActive(false);
        //ButtonInteract(true);
    }
    public void AnswerYes(bool setUpCharacter)
    {
        Time.timeScale = 1;
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
    //public void ButtonInteract(bool canInteract)
    //{
    //    foreach (Button button in buttons)
    //    {
    //        button.interactable = canInteract;
    //    }
    //}
    public void OpenHideCategory()
    {
        if (isCategoryOpen)
        {
            category.rectTransform.DOMoveX(hidePos.position.x, 0.1f);
            hideButton.rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
            showButton.gameObject.SetActive(true);
            hideButton.gameObject.SetActive(false);
            isCategoryOpen = false;
        }
        else
        {
            category.rectTransform.DOMoveX(openPos.position.x, 0.1f);
            showButton.gameObject.SetActive(false);
            hideButton.gameObject.SetActive(true);
            isCategoryOpen = true;
        }
    }
    public void BackToFanroom()
    {
        ViewsManager.Instance.LoadSceneByName("FanRoom");
    }
    public void OpenHideStatus()
    {
        status.SetActive(!status.activeSelf);
    }
    public void SendStatus()
    {
        Status status = new Status();
        status.name = UserInfoManager.Instance.userInfo.name;
        status.text = statusText.text;
        statusText.text = "";

        var x = Instantiate<GameObject>(prefabStatusUI.gameObject, contentPostMessage);
        x.GetComponent<StatusUI>()._name.text = status.name;
        x.GetComponent<StatusUI>()._text.text = status.text;

        FanroomFriendManager.inst.statusPostManager.statusPost.status.Add(status);
        FirebaseDatabase.PostJSON("Users/" + FanroomFriendManager.userID + "/StatusFriend", JsonUtility.ToJson(FanroomFriendManager.inst.statusPostManager.statusPost), gameObject.name, "ScrollToBottom", null);
    }
    public void ScrollToBottom(string data)
    {
        Debug.Log(data);
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
    IEnumerator ScrollToBottom()
    {
        yield return new WaitForSeconds(1);
        scrollRect.normalizedPosition = new Vector2(0, 0);
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
    private void OnDisable()
    {
        for (int i = 0; i < contentCategory.childCount; i++)
        {
            Destroy(contentCategory.GetChild(i).gameObject);
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
        iconItemName.Clear();

    }
}
