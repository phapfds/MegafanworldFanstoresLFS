using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ViewType
{
    EmptyView,
    LoadingView,
    DowloadUpdateView,
    LoginView,
    SignUpView,
    ForgotPassView,
    SetupView,
    SelectRoundView,
    InGameView,
    LeaderBoardView,
    IAPView,
    BarInteriorView,
    InsideStadiumView,
    CatchBySecurityView,
    TheCapinetView,
    TimeOutView,
    SelectCityView,
    FanroomView,
    FanstoreView,
    FanroomFriendView,
    MoveItemView,
    FreeKickView
}
public class Views : MonoBehaviour
{
    public ViewType viewType;

    private void Awake()
    {
        OnAwake();
    }
    public virtual void OnAwake()
    {

    }
    private void Start()
    {
        OnStart();
    }
    public virtual void OnStart()
    {

    }
    private void OnEnable()
    {
        SetUp();
    }
    public virtual void SetUp()
    {

    }

    public virtual void SetUp(ViewType previousView)
    {

    }

    public void Update()
    {
        OnUpdate();
    }
    public virtual void OnUpdate()
    {

    }
}
