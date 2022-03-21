using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public enum OrderResult
{
    Null,
    Success,
    Fail,
    OutOfStock,
}

public class FanstoreView : Views
{
    [Header("UI Referance")]
    public Text credits;
    [SerializeField] List<Button> buttons;
    [SerializeField] GameObject joystick;

    [Header("Show item detail")]
    [SerializeField] Image showItemDetail;
    [SerializeField] Text nameItem;
    [SerializeField] Text detail;
    [SerializeField] Text price;
    [SerializeField] Item currentItem = new Item();

    [Header("Cart")]
    [SerializeField] GameObject cart;
    [SerializeField] ItemToCart itemToCartprefab;
    [SerializeField] Transform cartContent;
    private List<ItemToCart> itemsInCart = new List<ItemToCart>(); //Display in UI
    [SerializeField] Text totalPrice;
    private int totalPrice_;

    [Header("Result order")]
    public Image orderSuccess;
    public Image orderFail;
    private OrderResult orderResult;

    [Header("History")]
    public Image tradingHistory;
    public Transform contentHistory;
    public HistoryItemObject itemHistoryPrefab;
    [SerializeField] ScrollRect scrollRectTradingHistory;


    public override void SetUp()
    {
        orderResult = OrderResult.Null;
        base.SetUp();
        //ButtonInteract(true);
        FanstoreManager.inst.ShowItemDetail += ShowItemDetail;
        credits.text = UserInfoManager.Instance.userInfo.coinsNum.ToString() + " credits";

    }

    public void ShowItemDetail(int id)
    {
        showItemDetail.gameObject.SetActive(true);
        currentItem = FanstoreDatabase.ins.fanstoreItem.itemList[FanstoreDatabase.ins.SearchItem(id)];
        nameItem.text = currentItem.nameItem;
        detail.text = currentItem.description;
        price.text = currentItem.price.ToString();
    }
    public void AddToCart(bool addItemToCart)
    {
        //ButtonInteract(false);
        showItemDetail.gameObject.SetActive(false);
        cart.gameObject.SetActive(true);
        if (addItemToCart)
        {
            ItemToCart item = Instantiate(itemToCartprefab);
            item.transform.SetParent(cartContent);
            item.nameItem.text = currentItem.nameItem;
            item.price.text = currentItem.price.ToString() + " credits";
            item.id = currentItem.id;
            item.transform.localScale = Vector3.one;
            item.gameObject.SetActive(true);
            itemsInCart.Add(item);
            FanstoreManager.inst.itemsInCart.Add(currentItem);
        }
        totalPrice_ = 0;
        foreach (Item item in FanstoreManager.inst.itemsInCart)
        {
            totalPrice_ += item.price;
        }

        totalPrice.text = totalPrice_.ToString() + " Credits";
    }

    public void RemoveItem(int priceItem)
    {
        totalPrice_ -= priceItem;
        totalPrice.text = totalPrice_.ToString() + " Credits";

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
    public void OffNotice(GameObject notice)
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
        ViewsManager.Instance.ChangeView(ViewType.FanstoreView, ViewType.IAPView);
    }
    public void LeaderBoardView()
    {
        ViewsManager.Instance.ChangeView(ViewType.FanstoreView, ViewType.LeaderBoardView);
    }

    public void QuitGame()
    {
        FanstoreManager.inst.ShowItemDetail -= ShowItemDetail;

        Application.Quit();
    }
    private void OnDisable()
    {
        FanstoreManager.inst.ShowItemDetail -= ShowItemDetail;

    }
    //public void ButtonInteract(bool canInteract)
    //{
    //    foreach (Button button in buttons)
    //    {
    //        button.interactable = canInteract;
    //    }
    //}
    public void Back()
    {
        //ViewsManager.Instance.LoadSceneByName("FanRoom");
        //if (SceneManager.sceneCount > 1)
        //{
        //    foreach (GameObject gb in InGameManager.Instance.gameObjects)
        //    {
        //        if (gb != null)
        //            gb.SetActive(true);
        //    }
        //    InGameManager.Instance.IngameState = IngameState.Ingame;
        //    SceneManager.UnloadSceneAsync("FanStore");
        //}
        SetupManager.SetUpView(ViewType.SetupView);
        ViewsManager.Instance.LoadSceneByName("Setup");
    }

    public void CheckOut()
    {
        if (totalPrice_ > 0)
        {
            if (UserInfoManager.Instance.userInfo.coinsNum >= totalPrice_)
            {
                orderResult = OrderResult.Success;
            }
            else
            {
                orderResult = OrderResult.Fail;
            }


            switch (orderResult)
            {
                case OrderResult.Success:
                    UserInfoManager.Instance.userInfo.coinsNum -= totalPrice_;
                    UserInfoManager.Instance.SaveData();
                    credits.text = UserInfoManager.Instance.userInfo.coinsNum.ToString() + " credits";
                    cart.gameObject.SetActive(false);
                    ///-----------------------------SAVE DATA TO FANROOM------------------///
                    foreach (Item item in FanstoreManager.inst.itemsInCart)
                    {
                        if (item != null)
                        {
                            FanroomDatabase.ins.fanroomItem.itemList.Add(item);
                            HistoryTradingDatabase.ins.historyItem.items.Add(new HistoryItem(item.nameItem, item.itemType, item.id, "Buy time (" + System.DateTime.Now.ToString() + ")", item.price, 1));
                        }
                    }
                    FanroomDatabase.ins.SaveData();
                    HistoryTradingDatabase.ins.SaveDataLocal();
                    //FanroomDatabase.ins.DebugLogData();
                    ///--------------------------------------------------------------------///
                    //-------ADD HONOR POINT-------//
                    HonorPointManage.ins.AddHonorPoint(FanstoreManager.inst.itemsInCart.Count * 0.1f);
                    Debug.Log(FanstoreManager.inst.itemsInCart.Count * 0.1f);
                    //-----------------------------//
                    foreach (ItemToCart item in itemsInCart)
                    {
                        if (item != null)
                            item.RemoveItem();
                    }
                    itemsInCart.Clear();
                    FanstoreManager.inst.itemsInCart.Clear();
                    orderSuccess.gameObject.SetActive(true);
                    //ButtonInteract(true);
                    break;
                case OrderResult.Fail:
                    orderFail.gameObject.SetActive(true);
                    break;
            }

            orderResult = OrderResult.Null;
        }
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

    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRectTradingHistory.normalizedPosition = new Vector2(0, 0);
    }
}
