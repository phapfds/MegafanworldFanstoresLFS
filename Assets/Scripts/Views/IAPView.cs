using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class IAPView : Views
{
    [Header("Variable ref")]
    private int totalCredits_;
    private int totalPrice_;
    private ViewType previousView;

    [Header("UI Reference")]
    [SerializeField] Text creditsNow;
    [SerializeField] List<Text> numberItems;
    [SerializeField] List<Text> totalcredits;
    [SerializeField] Text totalCredits;
    [SerializeField] Image purchase;
    [SerializeField] Image resume;
    [SerializeField] Image noticeOrdering;
    [SerializeField] Image noticeSuccess;
    [SerializeField] Text _noticeSucess;

    public override void SetUp()
    {
        previousView = ViewsManager.Instance.previousView;
        purchase.gameObject.SetActive(true);
        resume.gameObject.SetActive(true);
        foreach (Text t in numberItems)
            t.text = 0.ToString();
        noticeSuccess.gameObject.SetActive(false);
        if (UserInfoManager.Instance.userInfo != null)
            creditsNow.text = UserInfoManager.Instance.userInfo.coinsNum.ToString() + " credits";
        OnValueChange();
    }

    public void RemoveItem(Text numberItem)
    {
        int n = Int32.Parse(numberItem.text) - 1;
        if (n < 0) n = 0;
        numberItem.text = n.ToString();
        OnValueChange();
    }
    public void AddItem(Text numberItem)
    {
        numberItem.text = (Int32.Parse(numberItem.text) + 1).ToString();
        OnValueChange();
    }
    public void OnValueChange()
    {
        int numberItem0_ = Int32.Parse(numberItems[0].text);
        int numberItem1_ = Int32.Parse(numberItems[1].text);
        int numberItem2_ = Int32.Parse(numberItems[2].text);
        totalcredits[0].text = (numberItem0_ * 30).ToString() + " credits";
        totalcredits[1].text = (numberItem1_ * 60).ToString() + " credits";
        totalcredits[2].text = (numberItem2_ * 120).ToString() + " credits";
        totalCredits_ = numberItem0_ * 30 + numberItem1_ * 60 + numberItem2_ * 120;
        totalCredits.text = totalCredits_.ToString() + " credits";
        totalPrice_ = numberItem0_ * 3 + numberItem1_ * 5 + numberItem2_ * 10;
    }
    public void Purchase()
    {
        if (totalPrice_ > 0)
        {
            purchase.gameObject.SetActive(false);
            resume.gameObject.SetActive(false);
            StartCoroutine(BuyCompleteCoroutine());
        }
    }

    public IEnumerator BuyCompleteCoroutine()
    {

        UserInfoManager.Instance.userInfo.coinsNum += totalCredits_;
        UserInfoManager.Instance.SaveData();
        #region "UI Notice"
        if (LanguageManager.language == LanguageType.German)
            _noticeSucess.text = "Ihre Credits sind " + UserInfoManager.Instance.userInfo.coinsNum.ToString() + " credits";
        else
            _noticeSucess.text = "Your credits is " + UserInfoManager.Instance.userInfo.coinsNum.ToString() + " credits";
        #endregion
        creditsNow.text = UserInfoManager.Instance.userInfo.coinsNum.ToString() + " credits";
        noticeSuccess.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        Resume();
    }
    public void Resume()
    {
        if (previousView == ViewType.InGameView)
        {
            InGameManager.Instance.IngameState = IngameState.Ingame;
        }
        else
        {
            ViewsManager.Instance.ChangeView(previousView);
        }
    }
}
