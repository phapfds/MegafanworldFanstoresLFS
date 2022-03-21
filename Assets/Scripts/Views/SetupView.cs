using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirebaseWebGL.Scripts.FirebaseBridge;
using System;

public class SetupView : Views
{
    [Header("UI reference")]
    [SerializeField] InputField nickName;
    [SerializeField] Text nameClub;
    private int clubNum;
    [SerializeField] Image exit;


    [Header("Character material")]
    public Material shirtMart;
    public List<Texture> shirtTexs;

    public void GotoFanroom()
    {
        OnPlay();
        ViewsManager.Instance.LoadSceneByName("FanRoom");
    }

    public void GotoArtgallery()
    {
        OnPlay();
        ViewsManager.Instance.LoadSceneByName("Gallery");
    }

    public void GotoFanStore()
    {
        OnPlay();
        ViewsManager.Instance.LoadSceneByName("FrankfurtStore");
    }

    public void GotoFrankfurtStore()
    {
        OnPlay();
        ViewsManager.Instance.LoadSceneByName("FrankfurtStoreNew");
    }

    public void GotoUEFAStore()
    {
        OnPlay();
        ViewsManager.Instance.LoadSceneByName("UEFAStore");
    }

    public void GotoMunichStore()
    {
        OnPlay();
        ViewsManager.Instance.LoadSceneByName("MunichStore");
    }

    public void GotoDortmundStore()
    {
        OnPlay();
        ViewsManager.Instance.LoadSceneByName("DortmundStore");
    }

    public void GotoHamburgStore()
    {
        OnPlay();
        ViewsManager.Instance.LoadSceneByName("HamburgStore");
    }

    public void GotoFurniture()
    {
        OnPlay();
        ViewsManager.Instance.LoadSceneByName("FurnitureStore");
    }

    public void OnPlay()
    {
        UserInfoManager.Instance.userInfo.name = nickName.text;
        //UserInfoManager.Instance.userInfo.club = (Club)clubNum;
        UserInfoManager.Instance.SaveData();
        //ViewsManager.Instance.ChangeView(ViewType.SelectCityView);
    }
    public override void SetUp()
    {
        //ChatManager.inst.EnableChatSystem();

        exit.gameObject.SetActive(false);
        if (UserInfoManager.Instance.userInfo != null)
        {
            clubNum = (int)UserInfoManager.Instance.userInfo.club;
            base.SetUp();
            if (UserInfoManager.Instance.userInfo.name != "")
            {
                nickName.text = UserInfoManager.Instance.userInfo.name;
            }
            //nameClub.text = ((Club)clubNum).ToString();
            UserInfoManager.Instance.ChangeShirtMaterial(clubNum);
        }

        URLParameters.Instance.RegisterOnceOnDone((x) =>
        {
            string nickname = URLParameters.Instance.SearchParameters["name"];
            nickName.text = nickname;

        });

    }
    public void ForwardButton()
    {
        clubNum = clubNum > 12 ? 0 : clubNum + 1;
        //Debug.Log(clubNum);
        //nameClub.text = ((Club)clubNum).ToString();
        UserInfoManager.Instance.ChangeShirtMaterial(clubNum);

    }
    public void ReverseButton()
    {
        clubNum = clubNum < 1 ? 13 : clubNum - 1;
        //Debug.Log(clubNum);
        //nameClub.text = ((Club)clubNum).ToString();
        UserInfoManager.Instance.ChangeShirtMaterial(clubNum);
    }
    public void LogOut()
    {
        FirebaseAuth.SignOut(gameObject.name, null, null);
        FanroomDatabase.ins.ClearData();
        HistoryTradingDatabase.ins.DeleteDataLocal();
        UserInfoManager.Instance.userInfo = null;
        ViewsManager.Instance.LoadSceneByName("Login");
        ChatManager.inst.DisableChatSystem();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKey(KeyCode.Escape))
        {
            ShowNotice(exit);
        }
    }
    public void ShowNotice(Image notice)
    {
        notice.gameObject.SetActive(true);
    }
    public void OffNotice(Image notice)
    {
        notice.gameObject.SetActive(false);
    }

    public void OpenGame(bool munich)
    {

        URLParameters.Instance.RegisterOnceOnDone((x) =>
        {
            if (URLParameters.Instance.SearchParameters.ContainsKey("city"))
            {
                int club = Int32.Parse(URLParameters.Instance.SearchParameters["city"]);
                UserInfoManager.Instance.userInfo.club = (Club)club;
                if (shirtTexs[club] != null)
                    shirtMart.mainTexture = shirtTexs[club];
            }
        });

        if (munich)
        {
            //UserInfoManager.Instance.userInfo.club = Club.Munchen;
            SelectCityView.city = City.Munich;
        }
        else
        {
            SelectCityView.city = City.Frankfurt;
            //UserInfoManager.Instance.userInfo.club = Club.Frankfurt;
        }
        OnPlay();
        ViewsManager.Instance.ChangeView(ViewType.SelectRoundView);
    }
}
