using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Xml;

public enum IngameState
{
    Ingame,
    IAP,
    EnterKeyport,
    DrinkBeer, //or Eating Bratwurst
    Endgame,
    CatchedBySecurity,
    ChangeGameScene,
    Leaderboard,
    FanStore,
    FreeKick
}
public enum IngameType
{
    WalkingStreet,
    OutsideStadium,
}
public class InGameManager : Singleton<InGameManager>
{
    #region "OB Reference"
    [Header("OB Reference")]
    [SerializeField] Transform bembelKeyPort;
    [SerializeField] Transform beerKeyPort;
    [SerializeField] Transform brawurstKeyPort;
    public List<GameObject> gameObjects;
    [SerializeField] GameObject player;
    #endregion

    #region "Setting"
    [Header("Setting")]
    public int timeToPlay;
    public int timeBonus;
    public readonly int rateAttack = 2;
    protected bool notYetDisplayKeyport = true;
    public IngameType IngameType;
    [SerializeField] float timeKeyportSpawn;
    #endregion

    #region "Ingame State"
    [Header("GameState")]
    public float timeNow;
    public float timeAttack;
    public bool canAttack;
    public Action Endgame;
    private IngameState ingameState;
    //public static bool loadLevel2;

    public City city;

    public IngameState IngameState
    {
        set
        {
            ingameState = value;
            switch (ingameState)
            {
                case IngameState.Ingame:
                    Time.timeScale = 1;
                    ViewsManager.Instance.ChangeView(ViewType.InGameView);
                    break;
                case IngameState.EnterKeyport:
                    Time.timeScale = 0;
                    if (IngameType == IngameType.WalkingStreet)
                    {
                        ViewsManager.Instance.ChangeView(ViewType.BarInteriorView);
                    }
                    else
                    {
                        ViewsManager.Instance.ChangeView(ViewType.InsideStadiumView);
                    };
                    break;
                case IngameState.IAP:
                    Time.timeScale = 0;
                    ViewsManager.Instance.ChangeView(ViewType.IAPView);
                    break;
                case IngameState.DrinkBeer:
                    break;
                case IngameState.Endgame:
                    #region OldVersion
                    //if (IngameType == IngameType.WalkingStreet)
                    //{
                    //    //                        if (ScoreManager.score >= 100)
                    //    //                        {
                    //    //                            //loadLevel2 = true;
                    //    //#if UNITY_ANDROID
                    //    //                            ViewsManager.Instance.LoadSceneByName("OutsideStadium_MobileVersion");

                    //    //#elif UNITY_STANDALONE_WIN
                    //    //                            ViewsManager.Instance.LoadSceneByName("OutsideStadium_DesktopVersion");
                    //    //#endif
                    //    //                        }
                    //    //                        else
                    //    //                        {
                    //    //                            HighscoreTable.timeEnd = true;
                    //    //                            SetupManager.SetUpView(ViewType.LeaderBoardView);
                    //    //                            ViewsManager.Instance.LoadSceneByName("SetUp");
                    //    //                        }


                    //    Time.timeScale = 0;
                    //    ViewsManager.Instance.ChangeView(ViewType.TimeOutView);

                    //}
                    //else
                    //{
                    //    HighscoreTable.timeEnd = true;
                    //    SetupManager.SetUpView(ViewType.LeaderBoardView);
                    //    ViewsManager.Instance.LoadSceneByName("SetUp");
                    //}
                    #endregion
                    #region "Draft"
                    if (city == City.Munich && IngameType == IngameType.WalkingStreet && SelectRoundView.munichStadiumUnlock == 0 && ScoreManager.score >= SelectRoundView.scoreUnlock)
                    {
                        SelectRoundView.munichStadiumUnlock = 1;
                        PlayerPrefs.SetInt("munichStadiumUnlock", 1);
                    }
                    if (city == City.Frankfurt && IngameType == IngameType.WalkingStreet && SelectRoundView.frankfurtStadiumUnlock == 0 && ScoreManager.score >= SelectRoundView.scoreUnlock)
                    {
                        SelectRoundView.frankfurtStadiumUnlock = 1;
                        PlayerPrefs.SetInt("frankfurtStadiumUnlock", 1);
                    }
                    if (IngameType == IngameType.WalkingStreet)
                    {
                        HighscoreTable.timeEnd = true;
                        SetupManager.SetUpView(ViewType.LeaderBoardView);
                        ViewsManager.Instance.LoadSceneByName("SetUp");
                    }
                    else
                    {
                        //FreeKickView freeKickView =  ViewsManager.Instance.dicViews[ViewType.FreeKickView].
                        FreeKickView freeKickView  = ViewsManager.Instance.dicViews[ViewType.FreeKickView] as FreeKickView ;
                        freeKickView.currentTime = timeNow;
                        freeKickView.timeToPlay = timeToPlay;
                        ViewsManager.Instance.LoadSceneByName("FreeKick");
                    }
                    #endregion
                    break;
                case IngameState.ChangeGameScene:
                    break;
                case IngameState.CatchedBySecurity:
                    break;
                case IngameState.Leaderboard:
                    Time.timeScale = 0;
                    ViewsManager.Instance.ChangeView(ViewType.LeaderBoardView);
                    break;
                case IngameState.FanStore:
                    foreach (GameObject gb in gameObjects)
                    {
                        if (gb != null)
                            gb.SetActive(false);
                    }
                    break;
                case IngameState.FreeKick:

                    ViewsManager.Instance.ChangeView(ViewType.EmptyView);
                    foreach (GameObject gb in gameObjects)
                    {
                        if (gb != null)
                            gb.SetActive(false);
                    }
                    break;

            };
        }
        get
        {
            return ingameState;
        }
    }
    #endregion

    private void Awake()
    {
        //loadLevel2 = false;
        if (IngameType == IngameType.WalkingStreet) ScoreManager.Reset();
        ScoreManager.Reset();
        IngameState = IngameState.Ingame;
        Endgame += EndGame;
        m_ShuttingDown = false;
        m_Instance = null;
        if (IngameType == IngameType.OutsideStadium)
        {
            Security.catched += PlayerIsCatched;
        }
        player = GameObject.Find("Ch42_nonPBR");
    }
    private void Update()
    {
        TimeInGame();
    }
    protected void TimeInGame()
    {
        if (ingameState != IngameState.FanStore)
            timeNow += Time.deltaTime;
        if (timeNow >= timeKeyportSpawn)
            //       if (timeNow >= 20)
            DisplayKeyport();
        if (timeNow >= timeToPlay)
            Endgame?.Invoke();


        if (Security.securityState != SecurityState.Punispunishment)
        {
            if (IngameType == IngameType.WalkingStreet)
            {
                if (Mascot.Instance.mascotState == MascotState.Idle)
                    timeAttack = timeAttack < rateAttack ? timeAttack + Time.deltaTime : rateAttack;
            }
            else
                timeAttack = timeAttack < rateAttack ? timeAttack + Time.deltaTime : rateAttack;
            canAttack = timeAttack >= rateAttack;
        }
    }
    protected void DisplayKeyport()
    {
        if (notYetDisplayKeyport)
        {
            notYetDisplayKeyport = false;
            Transform ob;
            if (IngameType == IngameType.WalkingStreet)
            {
                //if (UserInfoManager.Instance.userInfo.club == Club.Frankfurt)
                if (SelectCityView.city == City.Frankfurt)
                {
                    ob = Instantiate(bembelKeyPort, player.transform.position + 3 * Vector3.right, Quaternion.identity); //4*
                    ob.transform.position = new Vector3(ob.transform.position.x, 1, ob.transform.position.z);
                }
                else
                {
                    ob = Instantiate(beerKeyPort, player.transform.position + 3 * Vector3.right, Quaternion.identity);
                    ob.transform.position = new Vector3(ob.transform.position.x, 0.5f, ob.transform.position.z);
                }
            }
            else
            {
                ob = Instantiate(brawurstKeyPort, player.transform.position + 4 * Vector3.right, Quaternion.identity);
                ob.transform.position = new Vector3(ob.transform.position.x, 0.25f, ob.transform.position.z);
            }
            Waypoint.keyport = ob.gameObject;
        }
    }
    public void EndGame()
    {
        IngameState = IngameState.Endgame;
        Endgame -= EndGame;
    }
    private void PlayerIsCatched(Transform ob)
    {
        IngameState = IngameState.CatchedBySecurity;
    }

    public void DisableObjects()
    {
        foreach (GameObject gameObject_ in gameObjects)
        {
            if (gameObject_ != null)
                gameObject_.SetActive(false);
        }
    }

    public void EnableObjects()
    {
        if (gameObjects[0] != null)
            gameObjects[0].SetActive(true);
    }

    private void OnDestroy()
    {
        m_ShuttingDown = false;
        m_Instance = null;
        if (Security.catched != null)
            Security.catched -= PlayerIsCatched;
    }
}
