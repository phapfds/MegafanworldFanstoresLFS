using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FreeKickManager : MonoBehaviour
{
    public static FreeKickManager Ins { get; private set; }
    [Header("UI REFERENCES")]
    [SerializeField] NewGoalLine goalPrefab;
    public GameObject ballPrefab;
    public Action<bool> goal;
    [SerializeField] List<FlagEffect> flagsInStadium;
    [SerializeField] GameObject explosion;
    [Header("STATUS")]
    public FreeKickState currentState;
    private Action<FreeKickState> ChangeState;
    public List<NewGoalLine> currentGoalLines = new List<NewGoalLine>();
    public int kickNum;
    public int currentGoalNum;
    private void Awake()
    {
        Ins = this;
        ViewsManager.Instance.ChangeView(ViewType.FreeKickView);

        ChangeState += ChangeStateHandle;
        goal += (result) =>
        {
            if (result)
                ChangeState(FreeKickState.KickGoal);
            else
                ChangeState(FreeKickState.KickFail);
        };
    }

    private void Start()
    {
        ChangeState?.Invoke(FreeKickState.Introduction);
    }


    private void ChangeStateHandle(FreeKickState freeKickState)
    {
        if (currentState != freeKickState)
        {
            currentState = freeKickState;
            switch (freeKickState)
            {
                case FreeKickState.Introduction:
                    StartCoroutine(Introduction());
                    break;
                case FreeKickState.Kicking:
                    StartCoroutine(Kicking());
                    break;
                case FreeKickState.KickGoal:
                    StartCoroutine(KickGoal());
                    break;
                case FreeKickState.KickFail:
                    KickFail();
                    break;
            };
        }
    }

    IEnumerator Introduction()
    {
        yield return new WaitForSeconds(5);
        ChangeState?.Invoke(FreeKickState.Kicking);
    }
    IEnumerator Kicking()
    {
        if (kickNum < 10)
        {
            yield return new WaitForSeconds(3);
            FKAudioManage.Ins.PlaySound(FKAudioType.pip);
            kickNum++;
            if (kickNum == 1)
                currentGoalNum = 1;
            else
                currentGoalNum = UnityEngine.Random.Range(1, 3);
            NewKickerController.ins.OnReset();
            #region "Create the goalines"
            foreach (NewGoalLine newGoalLine in currentGoalLines)
            {
                if (newGoalLine != null)
                    Destroy(newGoalLine.gameObject);
            }
            currentGoalLines.Clear();
            for (int i = 0; i < currentGoalNum; i++)
            {
                NewGoalLine newGoalLine = Instantiate<NewGoalLine>(goalPrefab);
                if (currentGoalLines.Count > 0)
                {
                    newGoalLine.transform.position = new Vector3(UnityEngine.Random.Range(-2.6f, 0.5f), UnityEngine.Random.Range(1.5f, 1.65f), 52.4f);
                    WallDir wallDir = currentGoalLines[0].GetComponent<WallMove>().currentDir;
                    newGoalLine.gameObject.GetComponent<WallMove>().currentDir = (WallDir)(1 - (int)wallDir);
                }
                else
                {
                    newGoalLine.transform.position = new Vector3(UnityEngine.Random.Range(0.8f, 2.5f), UnityEngine.Random.Range(1.5f, 1.65f), 52.5f);
                }
                currentGoalLines.Add(newGoalLine);
                newGoalLine.gameObject.SetActive(true);

            }
            #endregion
            GameObject ball = Instantiate<GameObject>(ballPrefab);
            yield return new WaitForSeconds(2);
            ball.GetComponent<SwipeBall>().canKick = true;
            FlagsInStadium(false);



        }
        else
        {
            yield return new WaitForSeconds(2);
            HighscoreTable.timeEnd = true;
            SetupManager.SetUpView(ViewType.LeaderBoardView);
            ViewsManager.Instance.LoadSceneByName("SetUp");
        }

    }
    IEnumerator KickGoal()
    {
        FKAudioManage.Ins.PlaySound(FKAudioType.goal);
        FlagsInStadium(true);
        yield return new WaitForSeconds(0.5f);
        ChangeState?.Invoke(FreeKickState.Kicking);
    }

    public void FlagsInStadium(bool play)
    {
        foreach (FlagEffect flagEffect in flagsInStadium)
        {
            if (flagEffect != null)
            {
                flagEffect.gameObject.SetActive(play);
                flagEffect.play = play;
            }
        }
        explosion.SetActive(play);
    }
    public void PlusScore(Transform trans)
    {
        int score;
        float distance = Math.Abs(SwipeBall.Ins.transform.position.y - trans.position.y);
        //Debug.Log(distance);
        if (distance <= 0.2)
        {
            score = 1500;
        }
        else if (distance <= 0.45)
        {
            score = 1000;
        }
        else if (distance <= 0.65)
        {
            score = 500;
        }
        else
            score = 300;
        ScoreManager.score += score;
        FreeKickView freeKickView = (FreeKickView)ViewsManager.Instance.dicViews[ViewType.FreeKickView];
        freeKickView.ShowScorePopup(Camera.main.WorldToScreenPoint(trans.position), score);
    }

    public IEnumerator DetectKickFail()
    {
        yield return new WaitForSeconds(2);
        if (currentGoalLines.Count == currentGoalNum)
            goal?.Invoke(false);
    }
    public void KickFail()
    {
        FKAudioManage.Ins.PlaySound(FKAudioType.fail);
        foreach (NewGoalLine newGoalLine in currentGoalLines)
        {
            Destroy(newGoalLine.gameObject);
        }
        currentGoalLines.Clear();
        ChangeState?.Invoke(FreeKickState.Kicking);
    }

    private void Update()
    {
        if (SwipeBall.Ins != null)
        {
            if (SwipeBall.Ins.isKicked)
            {
                SwipeBall.Ins.isKicked = false;
                StartCoroutine(DetectKickFail());
            }
        }
    }
}
