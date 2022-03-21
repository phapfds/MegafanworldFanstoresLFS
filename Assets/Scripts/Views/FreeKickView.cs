using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class FreeKickView : Views
{
    [Header("UI References")]
    [SerializeField] Text score;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] bool isCount;
    [SerializeField] Image timeProgressBar;
    [SerializeField] Text timeNow;
    [SerializeField] Text timePlay;
    [SerializeField] Text scorePopup;
    [SerializeField] GameObject notice;
    [Header("Game state")]
    public float currentTime;
    public float timeToPlay;
    public override void SetUp()
    {
        base.SetUp();
        //Debug.Log(currentTime);
        timeNow.text = currentTime.ToString("0.0");
        timePlay.text = timeToPlay.ToString();
        timeProgressBar.fillAmount = currentTime / timeToPlay;
        notice.SetActive(true);
    }

    

    public void CloseNotice()
    {
        this.notice.SetActive(false);
    }

    public void ShowScorePopup(Vector2 pos ,int score)
    {
        Text scorePopup_ = Instantiate<Text>(scorePopup, gameObject.transform);
        scorePopup_.text = "+" + score.ToString();
        scorePopup_.transform.position = pos;
        scorePopup_.gameObject.SetActive(true);
        Destroy(scorePopup_, 3);
    }

    public override void OnUpdate()
    {
        score.text = ScoreManager.score.ToString();
        if (FreeKickManager.Ins.currentState == FreeKickState.Introduction && !isCount)
        {
            StartCoroutine(Count());
        }
        TimeInGame();
    }

    public void TimeInGame()
    {
        if (FreeKickManager.Ins.currentState != FreeKickState.Null && FreeKickManager.Ins.currentState != FreeKickState.Introduction)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeToPlay)
            {
                HighscoreTable.timeEnd = true;
                SetupManager.SetUpView(ViewType.LeaderBoardView);
                ViewsManager.Instance.LoadSceneByName("SetUp");
            }
            timeProgressBar.fillAmount = currentTime / timeToPlay;
            if (timeToPlay - currentTime < 10)
                timeProgressBar.color = Color.red;
            timeNow.text = currentTime.ToString("0.0");
        }

    }

    IEnumerator Count()
    {
        isCount = true;
        yield return new WaitForSeconds(3);
        textMeshPro.gameObject.SetActive(true);
        textMeshPro.text = 3.ToString();
        yield return new WaitForSeconds(1);
        textMeshPro.text = 2.ToString();
        yield return new WaitForSeconds(1);
        textMeshPro.text = 1.ToString();
        yield return new WaitForSeconds(1f);
        textMeshPro.gameObject.SetActive(false);
    }

    public void ShowNotice(Image notice)
    {
        Time.timeScale = 0;
        notice.gameObject.SetActive(true);
    }
    public void OffNotice(Image notice)
    {
        Time.timeScale = 1;
        notice.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Replay()
    {
        SceneManager.LoadScene("Freekick");
    }
}
