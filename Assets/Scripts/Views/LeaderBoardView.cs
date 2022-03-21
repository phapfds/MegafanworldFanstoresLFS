using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeaderBoardView : Views
{
    [SerializeField] Text textBtn;
    private ViewType previousView;
    [SerializeField] Button backBtn;
    [SerializeField] Button quitBtn;
    private bool loadDone;
    public override void OnAwake()
    {
        base.OnAwake();
        HighscoresManager.done += () =>
        {
            loadDone = true;
        };
    }
    public override void SetUp()
    {
        base.SetUp();
        backBtn.interactable = false;
        quitBtn.interactable = false;
        previousView = ViewsManager.Instance.previousView;
        #region "UI"
        if (previousView == ViewType.InGameView)
        {
            if (InGameManager.Instance.IngameState == IngameState.Endgame)
                textBtn.text = "Restart";
            else
                textBtn.text = "Resume";
        }
        else
        {
            textBtn.text = "Back"; ;
        }
        #endregion


        backBtn.interactable = true;



    }
    public void Resume()
    {
        if (previousView == ViewType.InGameView)
        {
            if (InGameManager.Instance.IngameState == IngameState.Endgame)
            {
                Time.timeScale = 1;
                ViewsManager.Instance.LoadSceneByName("SetUp");
            }
            else
                InGameManager.Instance.IngameState = IngameState.Ingame;
        }
        else
        {
            ViewsManager.Instance.ChangeView(previousView);
        }
    }

    public void ShowNotice(Image notice)
    {
        notice.gameObject.SetActive(true);
        quitBtn.interactable = false;
        backBtn.interactable = false;
    }
    public void OffNotice(Image notice)
    {
        notice.gameObject.SetActive(false);
        quitBtn.interactable = true;
        backBtn.interactable = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (loadDone)
        {
            loadDone = false;
            backBtn.interactable = true;
            quitBtn.interactable = true;
        }
    }
}
