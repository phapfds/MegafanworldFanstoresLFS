using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameView : Views
{
    #region "Variable"
    [Header("UI Refernce")]
    [SerializeField] Image timeProgressBar;
    [SerializeField] Text timeNow;
    [SerializeField] Text timePlay;
    [SerializeField] Image energyBar;
    [SerializeField] Text score;
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject joystick;
    [SerializeField] Text scorePopups;
    [SerializeField] GameObject howToPlay;
    public Text credits;
    [SerializeField] List<Button> buttons;
    #endregion
    public override void OnAwake()
    {
        base.OnAwake();
        howToPlay.SetActive(true);
        Flag.score += () =>
        {
            score.text = ScoreManager.score.ToString();
        };
        Flag.scorePopups += (t, pos) =>
        {

            if (t > 0)
            {
                scorePopups.color = Color.green;
                scorePopups.text = "+" + t.ToString();
            }
            else if (t < 0)
            {
                scorePopups.text = "" + t.ToString();
                scorePopups.color = Color.red;
            }
            else
            {
                scorePopups.text = "Missed";
                scorePopups.color = Color.red;
            }
            scorePopups.transform.position = Camera.main.WorldToScreenPoint(pos);
            scorePopups.gameObject.SetActive(true);
        };
        energyBar.transform.parent.gameObject.SetActive(InGameManager.Instance.IngameType == IngameType.WalkingStreet);
    }

    public override void OnStart()
    {
        base.OnStart();
        Security.catched += Dialogue;
    }
    public void Dialogue(Transform trans)
    {
        joystick.gameObject.SetActive(false);
        energyBar.gameObject.SetActive(false);
        dialogue.SetActive(true);
    }
    public void TimeInGame()
    {
        timeNow.text = InGameManager.Instance.timeNow.ToString("0.0");
        timePlay.text = InGameManager.Instance.timeToPlay.ToString();
        timeProgressBar.fillAmount = InGameManager.Instance.timeNow / InGameManager.Instance.timeToPlay;
        if (InGameManager.Instance.timeToPlay - InGameManager.Instance.timeNow < 10)
            timeProgressBar.color = Color.red;
        energyBar.fillAmount = InGameManager.Instance.timeAttack / InGameManager.Instance.rateAttack;
        if (InGameManager.Instance.timeToPlay - InGameManager.Instance.timeNow < 1.5)
            ButtonInteract(false);
    }
    public override void SetUp()
    {
        base.SetUp();
        howToPlay.SetActive(true);
        score.text = ScoreManager.score.ToString();
        if (UserInfoManager.Instance.userInfo != null)
            credits.text = UserInfoManager.Instance.userInfo.coinsNum.ToString() + " credits";
        joystick.gameObject.SetActive(true);
        energyBar.gameObject.SetActive(true);
        dialogue.SetActive(false);
        timeProgressBar.color = Color.green;
        ButtonInteract(true);
        energyBar.transform.parent.gameObject.SetActive(InGameManager.Instance.IngameType == IngameType.WalkingStreet);

    }
    public override void OnUpdate()
    {
        TimeInGame();
        base.OnUpdate();
    }
    public void ShowNotice(Image notice)
    {
        Time.timeScale = 0;
        notice.gameObject.SetActive(true);
        ButtonInteract(false);
    }
    public void OffNotice(Image notice)
    {
        Time.timeScale = 1;
        notice.gameObject.SetActive(false);
        ButtonInteract(true);
    }
    public void AnswerYes(bool setUpCharacter)
    {
        if (setUpCharacter)
            SetupManager.SetUpView(ViewType.SetupView);
        else
            //SetupManager.SetUpView(ViewType.SelectCityView);
            SetupManager.SetUpView(ViewType.SelectRoundView);

        ViewsManager.Instance.LoadSceneByName("Setup");
    }
    public void IAPView()
    {
        InGameManager.Instance.IngameState = IngameState.IAP;
    }
    public void LeaderBoardView()
    {
        InGameManager.Instance.IngameState = IngameState.Leaderboard;
    }
    public void OnDestroy()
    {
        Security.catched -= Dialogue;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        Security.catched -= Dialogue;
    }
    public void ButtonInteract(bool canInteract)
    {
        foreach (Button button in buttons)
        {
            button.interactable = canInteract;
        }
    }
}
