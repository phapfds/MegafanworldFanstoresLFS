using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeoutView : Views
{
    [SerializeField] Button scene2;
    [SerializeField] Text notice;
    public override void SetUp()
    {
        base.SetUp();

        if (ScoreManager.score >= 100)
        {
            scene2.interactable = true;
            if (LanguageManager.language == LanguageType.English)
                notice.text = "Congratulation! Your score is " + ScoreManager.score.ToString() + ". Let's go to stadium now.";
            else
                notice.text = "Glückwunsch! Dein Ergebnis ist " + ScoreManager.score.ToString() + ". Lass uns jetzt ins Stadion gehen.";
        }
        else
        {
            StartCoroutine(Fail());
        }

    }

    IEnumerator Fail()
    {
        scene2.interactable = false;
        if (LanguageManager.language == LanguageType.English)
            notice.text = "Unlucky! Your score is " + ScoreManager.score.ToString() + ". Not enough points to go to stadium.";
        else
            notice.text = "Unglücklich! Dein Ergebnis ist " + ScoreManager.score.ToString() + ". Nicht genug Punkte, um ins Stadion zu gehen.";
        yield return new WaitForSecondsRealtime(5);
        GoToLeaderBoard();
    }

    public void GoToScene2()
    {
#if UNITY_ANDROID
        ViewsManager.Instance.LoadSceneByName("OutsideStadium_MobileVersion");
#elif UNITY_STANDALONE_WIN
        ViewsManager.Instance.LoadSceneByName("OutsideStadium_DesktopVersion");
#endif
    }
    public void GoToLeaderBoard()
    {
        //HighscoreTable.timeEnd = true;
        SetupManager.SetUpView(ViewType.LeaderBoardView);
        ViewsManager.Instance.LoadSceneByName("SetUp");
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
