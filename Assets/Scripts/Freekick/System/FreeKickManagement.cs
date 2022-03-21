using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FreeKickState
{
    Null,
    FindMatch,
    Introduction,
    Ingame,
    Kicking,
    KickGoal,
    KickFail,
    GoalKeeper,
    PauseGame
}
public class FreeKickManagement : MonoBehaviour
{
    public static FreeKickManagement ins;
    [SerializeField] public event EventHandler<FreeKickState> ChangeState;
    [Header("INGAME STATE")]
    public FreeKickState currentState;
    private int numberShoot;
    [Header("UI REFERENCE")]
    public Camera introCam;
    public Transform robot;
    public Camera kickerCam;
    public Camera gKCam;
    public GameObject kicker;
    public GameObject goalKeeper;
    public GameObject ball;
    public GameObject upFUI;
    public GameObject arrow;
    public List<GameObject> UIResult;
    public List<Canvas> mainUI;
    public GameObject terrain;
    [Header("WALLS OBSTACLE")]
    //[SerializeField] List<GameObject> walls;
    [SerializeField] List<GameObject> aims;
    [SerializeField] GameObject aimSingle;
    [Header("KICKER")]
    [SerializeField] Animator animKicker;
    [SerializeField] KickerInputManager kickerInputManager;
    [Header("BALL")]
    public BallPower _ball;

    private void Awake()
    {
        ins = this;
        ViewsManager.Instance.ChangeView(ViewType.EmptyView);
        ChangeState += ChangeSateHandle;
        numberShoot = 1;
        _ball.IsKicked += Ball_IsKicked;
    }

    private void Ball_IsKicked(object sender, EventArgs e)
    {
        StopCoroutineGoalDetect();
        StartCoroutine(GoalDetect());
    }

    IEnumerator GoalDetect()
    {
        yield return new WaitForSeconds(2f);
        GoalLine_Goal(this, false);
        //GKController.inst.GoalLine_Goal(this, false);
        KickerController.ins.GoalLine_Goal(this, false);
        KickerResult.inst.GoalLine_Goal(this, false);
    }

    public void StopCoroutineGoalDetect()
    {
        StopAllCoroutines();
    }
    private void Start()
    {
        ChangeState?.Invoke(this, currentState);
        kicker.GetComponent<KickerInputManager>().Kick += KickerInputManager_Kick;
    }

    private void KickerInputManager_Kick(object sender, TypeKickInput e)
    {

        upFUI.SetActive(true);
    }
    private void ChangeSateHandle(object sender, FreeKickState e)
    {
        switch (e)
        {
            case (FreeKickState.FindMatch):
                StartCoroutine(FindMatchState());
                break;
            case (FreeKickState.Introduction):
                StartCoroutine(IntroductionState());
                break;
            case (FreeKickState.Ingame):
                InGameState();
                break;
            case (FreeKickState.Kicking):
                StartCoroutine(KickerState());
                break;
            case (FreeKickState.GoalKeeper):
                StartCoroutine(GoalKeeperState());
                break;
        }
    }
    IEnumerator FindMatchState()
    {
        mainUI[0].gameObject.SetActive(true);
        mainUI[1].gameObject.SetActive(false);
        yield return null;
    }
    public void ChangeToIntroductionState()
    {
        currentState = FreeKickState.Introduction;
        ChangeState?.Invoke(this, currentState);
    }
    public void GoalLine_Goal(object sender, bool e)
    {
        //if (currentState == FreeKickState.Kicker)
        //    currentState = FreeKickState.GoalKeeper;
        //else
        //    currentState = FreeKickState.Kicker;
        currentState = FreeKickState.Kicking;
        ChangeState?.Invoke(this, currentState);
    }
    IEnumerator IntroductionState()
    {
        terrain.SetActive(true);
        mainUI[0].gameObject.SetActive(false);
        mainUI[1].gameObject.SetActive(true);
        introCam.gameObject.SetActive(true);
        robot.gameObject.SetActive(true);
        yield return new WaitForSeconds(15);
        introCam.gameObject.SetActive(false);
        robot.gameObject.SetActive(false);
        currentState = FreeKickState.Ingame;
        ChangeState?.Invoke(this, currentState);
    }
    public void InGameState()
    {

        terrain.SetActive(false);
        UIResult[0].SetActive(true);
        //UIResult[1].SetActive(true);
        currentState = FreeKickState.Kicking;
        ChangeState?.Invoke(this, currentState);
        kickerCam.gameObject.SetActive(true);

    }
    IEnumerator KickerState()
    {
        upFUI.SetActive(false);

        yield return new WaitForSeconds(5);
        if (numberShoot < 6)
        {
            arrow.GetComponentInParent<Arrow>().OnReset();
            ball.SetActive(true);
            kicker.SetActive(true);
            //goalKeeper.SetActive(true);
            OnReset();
            //for (int i = 0; i < numberShoot; i++)
            //{
            //    walls[i].SetActive(true);
            //}
            aims[numberShoot - 1].SetActive(true);
            if (aimSingle != null)
                aimSingle.SetActive(numberShoot == 4);
            kickerCam.gameObject.SetActive(true);
            gKCam.gameObject.SetActive(false);
            //upFUI.SetActive(true);
            arrow.GetComponent<MeshRenderer>().enabled = true;
            numberShoot += 1;
        }
        else
        {
            HighscoreTable.timeEnd = true;
            SetupManager.SetUpView(ViewType.LeaderBoardView);
            ViewsManager.Instance.LoadSceneByName("SetUp");
        }
    }
    IEnumerator GoalKeeperState()
    {
        yield return new WaitForSeconds(5);
        arrow.GetComponentInParent<Arrow>().OnReset();
        OnReset();
        kickerCam.gameObject.SetActive(false);
        gKCam.gameObject.SetActive(true);
        upFUI.SetActive(false);
        arrow.GetComponent<MeshRenderer>().enabled = false;
    }
    public void OnReset()
    {
        ball.GetComponent<BallPower>().OnReset();
        kicker.GetComponent<KickerController>().OnReset();
        animKicker.SetTrigger("Reset");
        kickerInputManager.Onreset();
        //goalKeeper.GetComponent<GKController>().OnReset();
        //foreach (GameObject gb in walls)
        //{
        //    gb.SetActive(false);

        //}
        foreach (GameObject aim in aims)
        {
            if (aim != null)
                aim.SetActive(false);
        }
    }


}
