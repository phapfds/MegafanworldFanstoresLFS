#if UNITY_STANDALONE_WIN || UNITY_ANDROID || UNITY_WEBGL

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using CodeMonkey.Utils;
using System;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using FirebaseWebGL.Scripts.FirebaseBridge;

public class HighscoreTable : MonoBehaviour
{
    #region "REFERENCE"
    public static HighscoreTable inst;

    public List<Transform> entryContainer;
    public List<Transform> entryTemplate;
    private List<List<Transform>> entryTransformList;

    private static string userID;
    private static string characterName;
    public static string club;
    private readonly DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
    private static string month;
    private string dateTimeNow;
    private string shortName;


    private HighscoresManager.Highscores myHighscores_monthly;
    private HighscoresManager.Highscores myHighscores_overall;
    private HighscoresManager.Highscores myHighscores_allClub_monthly;

    private HighscoresManager.Highscores clubRanking_monthly;
    private HighscoresManager.Highscores clubRanking_overall;
    private HighscoresManager.Highscores clubHighscore_monthly;

    private HighscoresManager.Highscores allClubsHighscore_overall;
    private HighscoresManager.Highscores allClubsRanking_monthly;
    private HighscoresManager.Highscores allClubsHighscores_monthly;

    private HighscoresManager.Highscores clubsCompetition;
    private HighscoresManager.Highscores clubsCompetition_monthly;


    public Text title;
    public Text time_name;
    public static bool inGame;
    public Transform position;
    public string[] pos_ = new string[8];

    public static bool timeEnd;

    public List<GameObject> needDestroy = new List<GameObject>();
    public Color color = new Color();

    #endregion
    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#7FA7E7", out color);
    }

    private void OnEnable()
    {
        if (inst == null) inst = this;
        userID = UserInfoManager.Instance.userInfo.userID;
        characterName = UserInfoManager.Instance.userInfo.name;
        club = Enum.GetName(typeof(Club), UserInfoManager.Instance.userInfo.club);
        Calendar cal = dfi.Calendar;
        int month_ = cal.GetMonth(DateTime.Now);
        month = month_ + "_" + DateTime.Now.Year;
        //dateTimeNow = DateTime.Now.DayOfWeek.ToString().Substring(0, 3).ToUpper() + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
        //CultureInfo german = new CultureInfo("de-DE");
        CultureInfo german = new CultureInfo("en-US");

        var day = german.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
        dateTimeNow = day.Substring(0, 2) + " " + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString(); //+" Uhr";
        shortName = Truncate(characterName, 15) + '_' + club;
        //Instatiate
        entryTransformList = new List<List<Transform>>();

        //ScoreManager.score = 1000 * UnityEngine.Random.Range(1, 10);
        //timeEnd = true;

        FirebaseDatabase.GetRawData("Users/" + userID + "/scores/" + month + "/allClub", gameObject.name, "WriteNewScore_AllClub_Monthly", null);
        FirebaseDatabase.GetRawData("Users/" + userID + "/scores/" + month + "/" + club, gameObject.name, "WriteNewScore_Club_Monthly", null);
        FirebaseDatabase.GetRawData("Users/" + userID + "/scores/2021/" + club, gameObject.name, "WriteNewScore_Club_Overall", null);

        #region "Test"

        //if (timeEnd) //timeEnd
        //{
        //myHighscores_monthly = Sort(await HighscoresManager.WriteNewScore_Club_Monthly(userID, month, ScoreManager.score, dateTimeNow, club));
        //myHighscores_overall = Sort(await HighscoresManager.WriteNewScore_Club_Overall(userID, ScoreManager.score, dateTimeNow, club));
        //myHighscores_allClub_monthly = Sort(await HighscoresManager.WriteNewScore_AllClub_Monthly(userID, month, ScoreManager.score, dateTimeNow, club));
        //FirebaseDatabase.GetRawData("Users/" + userID + "/scores/" + month + "/allClub", gameObject.name, "WriteNewScore_AllClub_Monthly", null);

        //}
        //else
        //{
        //myHighscores_monthly = Sort(await HighscoresManager.ReadScores_Club_Monthly(userID, month, club));
        //myHighscores_overall = Sort(await HighscoresManager.ReadScores_Club_Overall(userID, club));
        //myHighscores_allClub_monthly = Sort(await HighscoresManager.ReadScores_AllClub_Monthly(userID, month, club));
        //FirebaseDatabase.GetRawData("Users/" + userID + "/scores/" + month + "/allClub", gameObject.name, "ReadScores_AllClub_Monthly", null);

        //}
        //Test
        //ScoreManager.score = 1000 * UnityEngine.Random.Range(0, 3);
        //timeEnd = true;

        //myHighscores_monthly = Sort(await HighscoresManager.WriteNewScore_Club_Monthly(userID, month, ScoreManager.score, dateTimeNow, club));
        //myHighscores_overall = Sort(await HighscoresManager.WriteNewScore_Club_Overall(userID, ScoreManager.score, dateTimeNow, club));

        //FirebaseDatabase.GetRawData("Users/" + userID + "/scores/" + month + "/allClub", gameObject.name, "WriteNewScore_AllClub_Monthly", null);

        //myHighscores_allClub_monthly = Sort(await HighscoresManager.WriteNewScore_AllClub_Monthly(userID, month, ScoreManager.score, dateTimeNow, club));
        //My high scores //1
        //entryTransformList.Add(new List<Transform>());
        //foreach (HighscoresManager.HighscoreEntry highscoreEntry in myHighscores_allClub_monthly.highscoreEntryList)
        //{
        //    CreateHighscoreEntryTransform(highscoreEntry, entryTemplate[0], entryContainer[0], entryTransformList[entryTransformList.Count - 1]);
        //}
        ////My club high score monthly //2
        //clubHighscore_monthly = Sort(await HighscoresManager.Club_Highscore_Monthly(club, month, Max(myHighscores_monthly), userID, characterName));
        //pos_[1] = SearchPosition(clubHighscore_monthly, characterName);
        //entryTransformList.Add(new List<Transform>());
        //foreach (HighscoresManager.HighscoreEntry highscoreEntry in clubHighscore_monthly.highscoreEntryList)
        //{
        //    CreateHighscoreEntryTransform(highscoreEntry, entryTemplate[1], entryContainer[1], entryTransformList[entryTransformList.Count - 1]);
        //}
        ////All clubs high score monthly //3
        //allClubsHighscores_monthly = Sort(await HighscoresManager.AllClubs_Highscore_Monthly(userID, club, month, Max(myHighscores_monthly), shortName));
        //entryTransformList.Add(new List<Transform>());
        //pos_[2] = SearchPosition(allClubsHighscores_monthly, shortName);
        //foreach (HighscoresManager.HighscoreEntry highscoreEntry in allClubsHighscores_monthly.highscoreEntryList)
        //{
        //    CreateHighscoreEntryTransform(highscoreEntry, entryTemplate[2], entryContainer[2], entryTransformList[entryTransformList.Count - 1]);
        //}
        ////All clubs high scores overall time //4
        //allClubsHighscore_overall = Sort(await HighscoresManager.AllClubs_Highscore_Overall(userID, club, Max(myHighscores_overall), shortName));
        //entryTransformList.Add(new List<Transform>());
        //pos_[3] = SearchPosition(allClubsHighscore_overall, shortName);
        //foreach (HighscoresManager.HighscoreEntry highscoreEntry in allClubsHighscore_overall.highscoreEntryList)
        //{
        //    CreateHighscoreEntryTransform(highscoreEntry, entryTemplate[3], entryContainer[3], entryTransformList[entryTransformList.Count - 1]);
        //}
        ////My club ranking monthly//5
        //clubRanking_monthly = Sort(await HighscoresManager.Club_Ranking_Monthly(club, Sum(myHighscores_monthly), userID, characterName, month));
        //pos_[4] = SearchPosition(clubRanking_monthly, characterName);
        //entryTransformList.Add(new List<Transform>());
        //foreach (HighscoresManager.HighscoreEntry highscoreEntry in clubRanking_monthly.highscoreEntryList)
        //{
        //    CreateHighscoreEntryTransform(highscoreEntry, entryTemplate[4], entryContainer[4], entryTransformList[entryTransformList.Count - 1]);
        //}
        ////All clubs ranking monthly (sum) //6
        //allClubsRanking_monthly = Sort(await HighscoresManager.AllClubs_Ranking_Monthly(userID, club, month, Sum(myHighscores_monthly), shortName));
        //entryTransformList.Add(new List<Transform>());
        //pos_[5] = SearchPosition(allClubsRanking_monthly, shortName);
        //foreach (HighscoresManager.HighscoreEntry highscoreEntry in allClubsRanking_monthly.highscoreEntryList)
        //{
        //    CreateHighscoreEntryTransform(highscoreEntry, entryTemplate[5], entryContainer[5], entryTransformList[entryTransformList.Count - 1]);
        //}
        ////All clubs competition monthly //7
        //clubsCompetition_monthly = Sort(await HighscoresManager.ClubsCompetition_Monthly(Sum(clubRanking_monthly), month, club));
        //entryTransformList.Add(new List<Transform>());
        //foreach (HighscoresManager.HighscoreEntry highscoreEntry in clubsCompetition_monthly.highscoreEntryList)
        //{
        //    CreateHighscoreEntryTransform(highscoreEntry, entryTemplate[6], entryContainer[6], entryTransformList[entryTransformList.Count - 1]);
        //}
        //// Club ranking overall
        //clubRanking_overall = Sort(await HighscoresManager.Club_Ranking_Overall(club, Sum(myHighscores_overall), userID, characterName));
        ////All clubs competition //8
        //clubsCompetition = Sort(await HighscoresManager.ClubsCompetition(Sum(clubRanking_overall), club));
        //entryTransformList.Add(new List<Transform>());
        //foreach (HighscoresManager.HighscoreEntry highscoreEntry in clubsCompetition.highscoreEntryList)
        //{
        //    CreateHighscoreEntryTransform(highscoreEntry, entryTemplate[7], entryContainer[7], entryTransformList[entryTransformList.Count - 1]);
        //}
        #endregion

    }

    public void WriteNewScore_AllClub_Monthly(string data) //userID, month, ScoreManager.score, dateTimeNow, club
    {
        if (data == "null") data = null;
        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
        }
        if (timeEnd)
        {
            HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = ScoreManager.score, time_name = dateTimeNow, userId = userID };
            highscores.highscoreEntryList.Add(highscoreEntry);
            string json = JsonUtility.ToJson(highscores);
            FirebaseDatabase.PostJSON("Users/" + userID + "/scores/" + month + "/allClub", json, gameObject.name, null, null);
            myHighscores_allClub_monthly = Sort(json);
            //My high scores //1
            entryTransformList.Add(new List<Transform>());
            foreach (HighscoresManager.HighscoreEntry highscoreEntryy in myHighscores_allClub_monthly.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(highscoreEntryy, entryTemplate[0], entryContainer[0], entryTransformList[entryTransformList.Count - 1]);
            }
        }

        else
        {
            string json = JsonUtility.ToJson(highscores);
            myHighscores_allClub_monthly = Sort(json);
            //My high scores //1
            entryTransformList.Add(new List<Transform>());
            foreach (HighscoresManager.HighscoreEntry highscoreEntryy in myHighscores_allClub_monthly.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(highscoreEntryy, entryTemplate[0], entryContainer[0], entryTransformList[entryTransformList.Count - 1]);
            }
        }


    }
    public void WriteNewScore_Club_Monthly(string data)
    {
        if (data == "null") data = null;
        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
        }
        if (timeEnd)
        {
            HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = ScoreManager.score, time_name = dateTimeNow, userId = userID };
            highscores.highscoreEntryList.Add(highscoreEntry);
            string json = JsonUtility.ToJson(highscores);
            FirebaseDatabase.PostJSON("Users/" + userID + "/scores/" + month + "/" + club, json, gameObject.name, null, null);
            myHighscores_monthly = Sort(json);
        }

        else
        {
            string json = JsonUtility.ToJson(highscores);
            myHighscores_monthly = Sort(json);
        }

        FirebaseDatabase.GetRawData("ScoreTables/ClubHighscoresMonthly/" + month + "/" + club, gameObject.name, "Club_Highscore_Monthly", null);
        FirebaseDatabase.GetRawData("ScoreTables/AllClubsHighscoresMonthly/" + month, gameObject.name, "AllClubs_Highscore_Monthly", null);
        FirebaseDatabase.GetRawData("ScoreTables/ClubRankingMonthly/" + month + "/" + club, gameObject.name, "Club_Ranking_Monthly", null);
        FirebaseDatabase.GetRawData("ScoreTables/AllClubsRankingMonthly/" + month, gameObject.name, "AllClubs_Ranking_Monthly", null);

    }
    public void WriteNewScore_Club_Overall(string data)
    {
        if (data == "null") data = null;
        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
        }
        if (timeEnd)
        {
            HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = ScoreManager.score, time_name = dateTimeNow, userId = userID };
            highscores.highscoreEntryList.Add(highscoreEntry);
            string json = JsonUtility.ToJson(highscores);
            FirebaseDatabase.PostJSON("Users/" + userID + "/scores/2021/" + club, json, gameObject.name, null, null);
            myHighscores_overall = Sort(json);
        }
        else
        {
            string json = JsonUtility.ToJson(highscores);
            myHighscores_overall = Sort(json);
        }

        FirebaseDatabase.GetRawData("ScoreTables/AllClubsHighscoresOverall", gameObject.name, "AllClubs_Highscore_Overall", null);
        //FirebaseDatabase.GetRawData("ScoreTables/ClubsCompetition/", gameObject.name, "ClubsCompetition", null);
        FirebaseDatabase.GetRawData("ScoreTables/ClubRankingOverall/" + club, gameObject.name, "Club_Ranking_Overall", null);


    }
    public void Club_Highscore_Monthly(string data)
    {
        int score_ = Max(myHighscores_monthly);
        if (data == "null") data = null;
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score_, time_name = characterName, userId = userID };
        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
            highscores.highscoreEntryList.Add(highscoreEntry);
        }
        else
        {
            int i;
            for (i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                if (userID == highscores.highscoreEntryList[i].userId)
                {
                    highscores.highscoreEntryList[i].score = score_;
                    if (highscores.highscoreEntryList[i].time_name != characterName)
                        highscores.highscoreEntryList[i].time_name = characterName;
                    break;
                }
            }
            if (i == highscores.highscoreEntryList.Count)
                highscores.highscoreEntryList.Add(highscoreEntry);
        }
        string json = JsonUtility.ToJson(highscores);
        FirebaseDatabase.PostJSON("ScoreTables/ClubHighscoresMonthly/" + month + "/" + club, json, gameObject.name, null, null);

        //My club high score monthly //2
        clubHighscore_monthly = Sort(json);
        pos_[1] = SearchPosition(clubHighscore_monthly, characterName);
        entryTransformList.Add(new List<Transform>());
        foreach (HighscoresManager.HighscoreEntry highscoreEntry_ in clubHighscore_monthly.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry_, entryTemplate[1], entryContainer[1], entryTransformList[entryTransformList.Count - 1]);
        }
    }
    public void AllClubs_Highscore_Monthly(string data)
    {
        if (data == "null") data = null;
        int score_ = Max(myHighscores_monthly);
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score_, time_name = shortName, userId = userID };
        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
            highscores.highscoreEntryList.Add(highscoreEntry);
        }
        else
        {
            int i;
            for (i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                string nameClub = highscores.highscoreEntryList[i].time_name;
                //Debug.Log(nameClub.Substring(nameClub.Length - 4));
                int t = nameClub.Length - club.Length;
                if (t < 0) t = 0;
                //Debug.LogError(nameClub.Substring(t));
                if (userID == highscores.highscoreEntryList[i].userId && nameClub.Substring(t) == club)
                {
                    highscores.highscoreEntryList[i].score = score_;
                    if (highscores.highscoreEntryList[i].time_name != shortName)
                        highscores.highscoreEntryList[i].time_name = shortName;
                    break;
                }
            }
            if (i == highscores.highscoreEntryList.Count)
                highscores.highscoreEntryList.Add(highscoreEntry);
        }

        string json = JsonUtility.ToJson(highscores);
        FirebaseDatabase.PostJSON("ScoreTables/AllClubsHighscoresMonthly/" + month, json, gameObject.name, null, null);

        //All clubs high score monthly //3
        allClubsHighscores_monthly = Sort(json);
        entryTransformList.Add(new List<Transform>());
        pos_[2] = SearchPosition(allClubsHighscores_monthly, shortName);
        foreach (HighscoresManager.HighscoreEntry highscoreEntry_ in allClubsHighscores_monthly.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry_, entryTemplate[2], entryContainer[2], entryTransformList[entryTransformList.Count - 1]);
        }

    }
    public void AllClubs_Highscore_Overall(string data)
    {
        if (data == "null") data = null;
        int score_ = Max(myHighscores_overall);
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score_, time_name = shortName, userId = userID };

        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
            highscores.highscoreEntryList.Add(highscoreEntry);
        }
        else
        {
            int i;
            for (i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                string nameClub = highscores.highscoreEntryList[i].time_name;
                int t = nameClub.Length - club.Length;
                if (t < 0) t = 0;
                //Debug.LogError(nameClub.Substring(t));

                if (userID == highscores.highscoreEntryList[i].userId && nameClub.Substring(t) == club)
                {
                    highscores.highscoreEntryList[i].score = score_;
                    if (highscores.highscoreEntryList[i].time_name != shortName)
                        highscores.highscoreEntryList[i].time_name = shortName;
                    break;
                }
            }
            if (i == highscores.highscoreEntryList.Count)
                highscores.highscoreEntryList.Add(highscoreEntry);
        }


        string json = JsonUtility.ToJson(highscores);
        FirebaseDatabase.PostJSON("ScoreTables/AllClubsHighscoresOverall", json, gameObject.name, null, null);

        //All clubs high scores overall time //4
        allClubsHighscore_overall = Sort(json);
        entryTransformList.Add(new List<Transform>());
        pos_[3] = SearchPosition(allClubsHighscore_overall, shortName);
        foreach (HighscoresManager.HighscoreEntry highscoreEntry_ in allClubsHighscore_overall.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry_, entryTemplate[3], entryContainer[3], entryTransformList[entryTransformList.Count - 1]);
        }

    }
    public void Club_Ranking_Monthly(string data)
    {
        if (data == "null") data = null;
        int score_ = Sum(myHighscores_monthly);
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score_, time_name = characterName, userId = userID };
        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
            highscores.highscoreEntryList.Add(highscoreEntry);
        }
        else
        {
            int i;
            for (i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                if (userID == highscores.highscoreEntryList[i].userId)
                {
                    highscores.highscoreEntryList[i].score = score_;
                    if (highscores.highscoreEntryList[i].time_name != characterName)
                        highscores.highscoreEntryList[i].time_name = characterName;
                    break;
                }
            }
            if (i == highscores.highscoreEntryList.Count)
                highscores.highscoreEntryList.Add(highscoreEntry);
        }
        string json = JsonUtility.ToJson(highscores);
        FirebaseDatabase.PostJSON("ScoreTables/ClubRankingMonthly/" + month + "/" + club, json, gameObject.name, null, null);

        //My club ranking monthly//5
        clubRanking_monthly = Sort(json);
        pos_[4] = SearchPosition(clubRanking_monthly, characterName);
        entryTransformList.Add(new List<Transform>());
        foreach (HighscoresManager.HighscoreEntry highscoreEntryy in clubRanking_monthly.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntryy, entryTemplate[4], entryContainer[4], entryTransformList[entryTransformList.Count - 1]);
        }

        FirebaseDatabase.GetRawData("ScoreTables/ClubsCompetitionMonthly/" + month, gameObject.name, "ClubsCompetition_Monthly", null);


    }
    public void AllClubs_Ranking_Monthly(string data)
    {
        if (data == "null") data = null;
        int score_ = Sum(myHighscores_monthly);
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score_, time_name = shortName, userId = userID };

        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
            highscores.highscoreEntryList.Add(highscoreEntry);
        }
        else
        {
            int i;
            for (i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                string nameClub = highscores.highscoreEntryList[i].time_name;
                int t = nameClub.Length - club.Length;
                if (t < 0) t = 0;
                //Debug.LogError(nameClub.Substring(t));

                if (userID == highscores.highscoreEntryList[i].userId && nameClub.Substring(t) == club)
                {
                    highscores.highscoreEntryList[i].score = score_;
                    if (highscores.highscoreEntryList[i].time_name != shortName)
                        highscores.highscoreEntryList[i].time_name = shortName;
                    break;
                }
            }
            if (i == highscores.highscoreEntryList.Count)
                highscores.highscoreEntryList.Add(highscoreEntry);
        }
        string json = JsonUtility.ToJson(highscores);
        FirebaseDatabase.PostJSON("ScoreTables/AllClubsRankingMonthly/" + month, json, gameObject.name, null, null);
        //All clubs ranking monthly (sum) //6
        allClubsRanking_monthly = Sort(json);
        entryTransformList.Add(new List<Transform>());
        pos_[5] = SearchPosition(allClubsRanking_monthly, shortName);
        foreach (HighscoresManager.HighscoreEntry highscoreEntry_ in allClubsRanking_monthly.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry_, entryTemplate[5], entryContainer[5], entryTransformList[entryTransformList.Count - 1]);
        }
    }
    public void ClubsCompetition_Monthly(string data)
    {
        if (data == "null") data = null;
        int score_ = Sum(clubRanking_monthly);
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score_, time_name = club };

        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
            highscores.highscoreEntryList.Add(highscoreEntry);
        }
        else
        {
            int i;
            for (i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                if (club == highscores.highscoreEntryList[i].time_name)
                {
                    highscores.highscoreEntryList[i].score = score_;
                    break;
                }
            }
            if (i == highscores.highscoreEntryList.Count)
                highscores.highscoreEntryList.Add(highscoreEntry);
        }
        string json = JsonUtility.ToJson(highscores);
        FirebaseDatabase.PostJSON("ScoreTables/ClubsCompetitionMonthly/" + month, json, gameObject.name, null, null);
        //All clubs competition monthly //7
        clubsCompetition_monthly = Sort(json);
        entryTransformList.Add(new List<Transform>());
        foreach (HighscoresManager.HighscoreEntry highscoreEntry_ in clubsCompetition_monthly.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry_, entryTemplate[6], entryContainer[6], entryTransformList[entryTransformList.Count - 1]);
        }
    }
    public void Club_Ranking_Overall(string data)
    {
        if (data == "null") data = null;
        int score_ = Sum(myHighscores_overall);
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score_, time_name = characterName, userId = userID };

        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
            highscores.highscoreEntryList.Add(highscoreEntry);
        }
        else
        {
            int i;
            for (i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                if (userID == highscores.highscoreEntryList[i].userId)
                {
                    highscores.highscoreEntryList[i].score = score_;
                    if (highscores.highscoreEntryList[i].time_name != characterName)
                        highscores.highscoreEntryList[i].time_name = characterName;
                    break;
                }
            }
            if (i == highscores.highscoreEntryList.Count)
                highscores.highscoreEntryList.Add(highscoreEntry);
        }
        string json = JsonUtility.ToJson(highscores);
        clubRanking_overall = Sort(json);
        FirebaseDatabase.PostJSON("ScoreTables/ClubRankingOverall/" + club, json, gameObject.name, null, null);
        FirebaseDatabase.GetRawData("ScoreTables/ClubsCompetition/", gameObject.name, "ClubsCompetition", null);
    }
    public void ClubsCompetition(string data)
    {
        if (data == "null") data = null;
        int score_ = Sum(clubRanking_overall);
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score_, time_name = club };

        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
        if (highscores == null)
        {
            highscores = new HighscoresManager.Highscores()
            {
                highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
            };
            highscores.highscoreEntryList.Add(highscoreEntry);
        }
        else
        {
            int i;
            for (i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                if (club == highscores.highscoreEntryList[i].time_name)
                {
                    highscores.highscoreEntryList[i].score = score_;
                    break;
                }
            }
            if (i == highscores.highscoreEntryList.Count)
                highscores.highscoreEntryList.Add(highscoreEntry);
        }
        string json = JsonUtility.ToJson(highscores);
        FirebaseDatabase.PostJSON("ScoreTables/ClubsCompetition/", json, gameObject.name, null, null);
        //All clubs competition //8
        clubsCompetition = Sort(json);
        entryTransformList.Add(new List<Transform>());
        foreach (HighscoresManager.HighscoreEntry highscoreEntry_ in clubsCompetition.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry_, entryTemplate[7], entryContainer[7], entryTransformList[entryTransformList.Count - 1]);
        }
    }
    //Sort list scores
    private HighscoresManager.Highscores Sort(string scores)
    {
        HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(scores);
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoresManager.HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        return highscores;
    }

    public void OnChangeTable(int index)
    {
        for (int i = 0; i < entryContainer.Count; i++)
        {
            entryContainer[i].gameObject.SetActive(i == index);
        }
        switch (index)
        {
            case 0:
                position.gameObject.SetActive(false);
                if (LanguageManager.language == LanguageType.German)
                {
                    title.text = "Pers. Highscore";
                    time_name.text = "ZEIT";
                }
                else
                {
                    title.text = "My high scores monthly";
                    time_name.text = "TIME";
                }
                break;
            case 1:
                position.gameObject.SetActive(true);
                if (LanguageManager.language == LanguageType.German)
                {
                    title.text = club + " highscore monat";
                    time_name.text = "NAME";
                }
                else
                {
                    title.text = club + " high scores monthly";
                    time_name.text = "NAME";
                }
                break;
            case 2:
                position.gameObject.SetActive(true);
                if (LanguageManager.language == LanguageType.German)
                {
                    title.text = "Overall highscore monat";
                    time_name.text = "NAME";
                }
                else
                {
                    title.text = "All clubs high scores monthly";
                    time_name.text = "NAME";
                }
                break;
            case 3:

                position.gameObject.SetActive(true);
                if (LanguageManager.language == LanguageType.German)
                {
                    title.text = "Overall highscore";
                    time_name.text = "NAME";
                }
                else
                {
                    title.text = "All clubs high scores overall time";
                    time_name.text = "NAME";
                }
                break;
            case 4:
                position.gameObject.SetActive(true);
                if (LanguageManager.language == LanguageType.German)
                {
                    title.text = club + " ranking monat";
                    time_name.text = "NAME";
                }
                else
                {
                    title.text = club + " ranking monthly";
                    time_name.text = "NAME";
                }
                break;
            case 5:
                position.gameObject.SetActive(true);
                if (LanguageManager.language == LanguageType.German)
                {
                    title.text = "Monatpunkte";
                    time_name.text = "NAME";
                }
                else
                {
                    title.text = "All Clubs ranking monthly";
                    time_name.text = "NAME";
                }
                break;
            case 6:
                position.gameObject.SetActive(false);
                if (LanguageManager.language == LanguageType.German)
                {
                    title.text = "Gesamttabelle monat";
                    time_name.text = "NAME";
                }
                else
                {
                    title.text = "All clubs competition monthly";
                    time_name.text = "NAME";
                }
                break;
            case 7:
                position.gameObject.SetActive(false);
                if (LanguageManager.language == LanguageType.German)
                {
                    title.text = "Gesamttabelle";
                    time_name.text = "NAME";
                }
                else
                {
                    title.text = "All clubs competition";
                    time_name.text = "NAME";
                }
                break;
        }
        if (position.gameObject.activeSelf)
        {
            if (LanguageManager.language == LanguageType.German)
            {
                position.Find("currentPos").GetComponent<Text>().text = "Meine aktuelle Position: " + pos_[index];
            }
            else
            {
                position.Find("currentPos").GetComponent<Text>().text = "You are " + pos_[index] + " in this ranking";
            }
        }

    }
    private int Sum(HighscoresManager.Highscores highscores)
    {
        int sum = 0;
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {

            sum += highscores.highscoreEntryList[i].score;
        }
        return sum;
    }
    private int Max(HighscoresManager.Highscores highscores)
    {
        int max = 0;
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            int score = highscores.highscoreEntryList[i].score;
            if (score > max) max = score;
        }
        return max;
    }
    //Search list scores
    private string SearchPosition(HighscoresManager.Highscores highscores, string time_name)
    {
        int position = 0;
        string output = "";
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            position = i + 1;
            if (highscores.highscoreEntryList[i].time_name == time_name)
            {
                break;
            }
        }
        switch (position)
        {
            default:
                output = position + "th"; break;
            case 1: output = "1st"; break;
            case 2: output = "2nd"; break;
            case 3: output = "3rd"; break;
        }
        return output;
    }
    //Truncate string
    private string Truncate(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }
    public void CreateHighscoreEntryTransform(HighscoresManager.HighscoreEntry highscoreEntry, Transform entryTemplate, Transform container, List<Transform> transformList)
    {
        float templateHeight = 60f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        needDestroy.Add(entryTransform.gameObject);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string time_name = highscoreEntry.time_name;
        entryTransform.Find("nameText").GetComponent<Text>().text = time_name;

        // Set background visible odds and evens, easier to read
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);
        if (timeEnd)
        {
            GameObject background = entryTransform.Find("background").gameObject;
            if (highscoreEntry.time_name == dateTimeNow || highscoreEntry.time_name == UserInfoManager.Instance.userInfo.name || highscoreEntry.time_name.Contains(shortName))
            {
                background.GetComponent<Image>().color = color;
                background.SetActive(true);
            }
        }

        // Highlight First
        if (rank == 1)
        {
            entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
        }
        // Set trophy
        switch (rank)
        {
            default:
                entryTransform.Find("trophy").gameObject.SetActive(false);
                break;
            case 1:
                //entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("FFD200");
                break;
            case 2:
                //entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("C6C6C6");
                break;
            case 3:
                //entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("B76F56");
                break;
        }
        transformList.Add(entryTransform);
    }
    //UI
    private void OnDisable()
    {
        entryTransformList.Clear();
        foreach (GameObject ob in needDestroy)
        {
            ob.SetActive(false);
        }
        if (timeEnd)
            timeEnd = false;
        ScoreManager.score = 0;
    }
}
#endif
