using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum City
{
    Frankfurt,
    Munich
}
public class SelectCityView : Views
{
    [SerializeField] List<Button> buttons;

    public static City city = City.Frankfurt;
    public void ButtonInteract(bool canInteract)
    {
        foreach (Button button in buttons)
        {
            if (button != null)
                button.interactable = canInteract;
        }
    }
    public override void SetUp()
    {
        base.SetUp();
        ButtonInteract(true);
    }
    public void GotoSetupView()
    {
        ViewsManager.Instance.ChangeView(ViewType.SetupView);
    }
    public void GotoIAPView()
    {
        ViewsManager.Instance.ChangeView(ViewType.SelectCityView, ViewType.IAPView);
    }
    public void GotoLeaderBoardView()
    {
        ViewsManager.Instance.ChangeView(ViewType.LeaderBoardView);
    }

    public void GotoFanroom()
    {
        ViewsManager.Instance.LoadSceneByName("FanRoom");
    }

    public void GotoArtgallery()
    {
        ViewsManager.Instance.LoadSceneByName("FrankfurtStoreNew");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowNotice(Image notice)
    {
        ButtonInteract(false);
        notice.gameObject.SetActive(true);
    }
    public void OffNotice(Image notice)
    {
        notice.gameObject.SetActive(false);
        ButtonInteract(true);
    }

    public void GotoSelectRoundView(bool munich)
    {
        if (munich)
            city = City.Munich;
        else
            city = City.Frankfurt;
        ViewsManager.Instance.ChangeView(ViewType.SelectRoundView);

    }
}
