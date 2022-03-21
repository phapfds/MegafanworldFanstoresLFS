using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HighscoresManagerRESTAPI : MonoBehaviour
{
    #region "Write new score to monthly table"
    public static void WriteNewScore_Club_Monthly(string userID, string month, int score, string time, string club, Action<string> callback)
    {
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score, time_name = time, userId = userID };
        DatabaseRESTAPI.Read_MyTable_Club_Monthly(userID, month, club, (data) =>
        {
            HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
            if (highscores == null)
            {
                highscores = new HighscoresManager.Highscores()
                {
                    highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
                };
            }
            highscores.highscoreEntryList.Add(highscoreEntry);
            string json = JsonUtility.ToJson(highscores);
            DatabaseRESTAPI.Write_MyTable_Club_Monthly(userID, month, club, json, () =>
            {
                callback(json);
            });
        });
    }
    public static void ReadScores_Club_Monthly(string userID, string month, string club, Action<string> callback)
    {
        DatabaseRESTAPI.Read_MyTable_Club_Monthly(userID, month, club, (data) =>
        {
            HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
            if (highscores == null)
            {
                highscores = new HighscoresManager.Highscores()
                {
                    highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
                };
            }
            string json = JsonUtility.ToJson(highscores);
            callback(json);
        });
    }
    #endregion
    #region  "Write new score to overall time table"
    public static void WriteNewScore_Club_Overall(string userID, int score, string time, string club, Action callback)
    {
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score, time_name = time, userId = userID };
        DatabaseRESTAPI.Read_MyTable_Club_Overall(userID, club, (data) =>
        {
            HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
            if (highscores == null)
            {
                highscores = new HighscoresManager.Highscores()
                {
                    highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
                };
            }
            highscores.highscoreEntryList.Add(highscoreEntry);
            string json = JsonUtility.ToJson(highscores);
            DatabaseRESTAPI.Write_MyTable_Club_Overall(userID, club, json, () =>
            {
                callback();
            });
        });
    }
    public static void ReadScores_Club_Overall(string userID, string club, Action<string> callback)
    {
        DatabaseRESTAPI.Read_MyTable_Club_Overall(userID, club, (data) =>
        {
            HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
            if (highscores == null)
            {
                highscores = new HighscoresManager.Highscores()
                {
                    highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
                };
            }
            string json = JsonUtility.ToJson(highscores);
            callback(json);
        });
    }
    #endregion
    #region "Write new score to monthly all clubs table"
    public static void WriteNewScore_AllClub_Monthly(string userID, string month, int score, string time, Action<string> callback)
    {
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score, time_name = time, userId = userID };
        DatabaseRESTAPI.Read_MyTable_AllClub_Monthly(userID, month, (data) =>
        {
            HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
            if (highscores == null)
            {
                highscores = new HighscoresManager.Highscores()
                {
                    highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
                };
            }
            highscores.highscoreEntryList.Add(highscoreEntry);
            string json = JsonUtility.ToJson(highscores);
            DatabaseRESTAPI.Write_MyTable_AllClub_Monthly(userID, month, json, () =>
                {
                    callback(json);
                });
        });
    }

    public static void ReadScores_AllClub_Monthly(string userID, string month, Action<string> callback)
    {
        DatabaseRESTAPI.Read_MyTable_AllClub_Monthly(userID, month, (data) =>
        {
            HighscoresManager.Highscores highscores = JsonUtility.FromJson<HighscoresManager.Highscores>(data);
            if (highscores == null)
            {
                highscores = new HighscoresManager.Highscores()
                {
                    highscoreEntryList = new List<HighscoresManager.HighscoreEntry>()
                };
            }
            string json = JsonUtility.ToJson(highscores);
            callback(json);
        });
    }
    #endregion
    #region "My club high score monthly
    public static void Club_Highscore_Monthly(string club, string month, int score, string userID, string name, Action<string> callback)
    {
        Debug.LogError("Club_Highscore_Monthly");
        HighscoresManager.HighscoreEntry highscoreEntry = new HighscoresManager.HighscoreEntry { score = score, time_name = name, userId = userID };
        DatabaseRESTAPI.Read_Club_Highscore_Monthly(club, month, (data) =>
        {
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
                        highscores.highscoreEntryList[i].score = score;
                        if (highscores.highscoreEntryList[i].time_name != name)
                            highscores.highscoreEntryList[i].time_name = name;
                        break;
                    }
                }
                if (i == highscores.highscoreEntryList.Count)
                    highscores.highscoreEntryList.Add(highscoreEntry);
            }
            string json = JsonUtility.ToJson(highscores);
            DatabaseRESTAPI.Write_Club_Highscore_Monthly(club, month, json, () =>
            {
                callback(json);
            });
        });
    }
    #endregion
    //My club ranking monthly
}
