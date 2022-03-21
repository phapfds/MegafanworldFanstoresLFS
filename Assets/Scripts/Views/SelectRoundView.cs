using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectRoundView : Views
{
    [SerializeField] List<Button> buttons;
    [SerializeField] GameObject frankfurtButton;
    [SerializeField] GameObject munichButton;
    [SerializeField] GameObject munichStadiumButton;
    [SerializeField] GameObject frankfurtStadiumButton;
    [SerializeField] GameObject unlockNotice;

    public static int munichStadiumUnlock; //UNLOCK 1; LOCK 0
    public static int frankfurtStadiumUnlock;
    public static int scoreUnlock = 2000;

    public override void OnAwake()
    {
        base.OnAwake();
        Debug.Log( PlayerPrefs.GetInt("frankfurtStadiumUnlock"));
        frankfurtStadiumUnlock = PlayerPrefs.GetInt("frankfurtStadiumUnlock", 0);
        munichStadiumUnlock = PlayerPrefs.GetInt("munichStadiumUnlock", 0);
    }

    public void ButtonInteract(bool canInteract)
    {
        foreach (Button button in buttons)
        {
            button.interactable = canInteract;
        }
    }

    public void BackButton()
    {
        ViewsManager.Instance.ChangeView(ViewType.SetupView);
    }
    public override void SetUp()
    {


        base.SetUp();
        Debug.Log(PlayerPrefs.GetInt("frankfurtStadiumUnlock"));

        frankfurtStadiumUnlock = PlayerPrefs.GetInt("frankfurtStadiumUnlock", 0);
        munichStadiumUnlock = PlayerPrefs.GetInt("munichStadiumUnlock", 0);
        ButtonInteract(true);
        //Debug.Log((City)SelectCityView.city);
        frankfurtButton.SetActive(SelectCityView.city == City.Frankfurt);
        munichButton.SetActive(SelectCityView.city == City.Munich);

        munichStadiumButton.SetActive(SelectCityView.city == City.Munich);
        frankfurtStadiumButton.SetActive(SelectCityView.city == City.Frankfurt);
        munichStadiumButton.GetComponent<Button>().interactable = munichStadiumUnlock == 1;
        frankfurtStadiumButton.GetComponent<Button>().interactable = frankfurtStadiumUnlock == 1;
        if (SelectCityView.city == City.Frankfurt)
        {
            unlockNotice.SetActive(frankfurtStadiumUnlock != 1);
        }
        else if (SelectCityView.city == City.Munich)
        {
            unlockNotice.SetActive(munichStadiumUnlock != 1);
        }
    }
    public void GotoSetupView()
    {
        ViewsManager.Instance.ChangeView(ViewType.SetupView);
    }
    public void GotoIAPView()
    {
        ViewsManager.Instance.ChangeView(ViewType.SelectRoundView, ViewType.IAPView);
    }
    public void GotoLeaderBoardView()
    {
        ViewsManager.Instance.ChangeView(ViewType.LeaderBoardView);
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

    public void OnWalkingStreetPlay()
    {

        if (SelectCityView.city == City.Munich)
            ViewsManager.Instance.LoadSceneByName("Munich");
        else
            if (SelectCityView.city == City.Frankfurt)
            ViewsManager.Instance.LoadSceneByName("Frankfurt_DesktopVersion");
    }

    public void OnStadiumPlay()
    {
        if (SelectCityView.city == City.Munich)
            ViewsManager.Instance.LoadSceneByName("MunichStadium");
        else
    if (SelectCityView.city == City.Frankfurt)
            ViewsManager.Instance.LoadSceneByName("FrankfurtStadium_DesktopVersion");
    }
    private void OnDisable()
    {
        frankfurtButton.SetActive(false);
        munichButton.SetActive(false);
        unlockNotice.SetActive(true);
    }
}


