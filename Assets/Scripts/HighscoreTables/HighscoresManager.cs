using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using FirebaseWebGL.Scripts.FirebaseBridge;
public class HighscoresManager : MonoBehaviour
{
    public static Action done;

    #region "Write new score to monthly table"
    //async public static Task<string> WriteNewScore_Club_Monthly(string userID, string month, int score, string time, string club)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = time, userId = userID };
    //    string jsonString = null;

    //    await FBDatabase.Read_MyTable_Club_Monthly(userID, month, club).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //    }
    //    highscores.highscoreEntryList.Add(highscoreEntry);
    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_MyTable_Club_Monthly(userID, month, club, json).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            //Debug.Log("Write new score to monthly table");
    //        }
    //    });
    //    return json;
    //}
    //async public static Task<string> ReadScores_Club_Monthly(string userID, string month, string club)
    //{
    //    string jsonString = null;
    //    await FBDatabase.Read_MyTable_Club_Monthly(userID, month, club).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //    }
    //    string json = JsonUtility.ToJson(highscores);
    //    return json;
    //}
    #endregion
    #region "Write new score to overall time table"
    //async public static Task<string> WriteNewScore_Club_Overall(string userID, int score, string time, string club)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = time, userId = userID };
    //    string jsonString = null;
    //    await FBDatabase.Read_MyTable_Club_Overall(userID, club).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //    }
    //    highscores.highscoreEntryList.Add(highscoreEntry);
    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_MyTable_Club_Overall(userID, club, json).ContinueWith(task =>
    //     {
    //         if (task.IsCompleted)
    //         {
    //             //Debug.Log("Write new score to overall time table");
    //         }
    //     });
    //    return json;
    //}
    //async public static Task<string> ReadScores_Club_Overall(string userID, string club)
    //{
    //    string jsonString = null;
    //    await FBDatabase.Read_MyTable_Club_Overall(userID, club).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //    }
    //    string json = JsonUtility.ToJson(highscores);
    //    return json;
    //}
    #endregion
    #region "Write new score to monthly all clubs table"


    //async public static Task<string> WriteNewScore_AllClub_Monthly(string userID, string month, int score, string time, string club)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = time, userId = userID };
    //    string jsonString = null;

    //    await FBDatabase.Read_MyTable_AllClub_Monthly(userID, month, club).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });

    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //    }
    //    highscores.highscoreEntryList.Add(highscoreEntry);
    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_MyTable_AllClub_Monthly(userID, month, club, json).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            //Debug.Log("Write new score to monthly all clubs table");
    //        }
    //    });
    //    return json;
    //}
    //async public static Task<string> ReadScores_AllClub_Monthly(string userID, string month, string club)
    //{
    //    string jsonString = null;
    //    await FBDatabase.Read_MyTable_AllClub_Monthly(userID, month, club).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //    }
    //    string json = JsonUtility.ToJson(highscores);
    //    return json;
    //}
    #endregion
    #region "My club ranking monthly"
    //async public static Task<string> Club_Ranking_Monthly(string club, int score, string userID, string name, string month)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = name, userId = userID };
    //    string jsonString = null;
    //    await FBDatabase.Read_Club_Ranking_Monthly(club, month).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //        highscores.highscoreEntryList.Add(highscoreEntry);
    //    }
    //    else
    //    {
    //        int i;
    //        for (i = 0; i < highscores.highscoreEntryList.Count; i++)
    //        {
    //            if (userID == highscores.highscoreEntryList[i].userId)
    //            {
    //                highscores.highscoreEntryList[i].score = score;
    //                if (highscores.highscoreEntryList[i].time_name != name)
    //                    highscores.highscoreEntryList[i].time_name = name;
    //                break;
    //            }
    //        }
    //        if (i == highscores.highscoreEntryList.Count)
    //            highscores.highscoreEntryList.Add(highscoreEntry);
    //    }


    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_Club_Ranking_Monthly(club, month, json).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            //Debug.Log("My club ranking monthly");
    //        }
    //    });
    //    return json;
    //}
    #endregion
    #region "My club ranking overall"
    //async public static Task<string> Club_Ranking_Overall(string club, int score, string userID, string name)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = name, userId = userID };
    //    string jsonString = null;
    //    await FBDatabase.Read_Club_Ranking_Overall(club).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //        highscores.highscoreEntryList.Add(highscoreEntry);
    //    }
    //    else
    //    {
    //        int i;
    //        for (i = 0; i < highscores.highscoreEntryList.Count; i++)
    //        {
    //            if (userID == highscores.highscoreEntryList[i].userId)
    //            {
    //                highscores.highscoreEntryList[i].score = score;
    //                if (highscores.highscoreEntryList[i].time_name != name)
    //                    highscores.highscoreEntryList[i].time_name = name;
    //                break;
    //            }
    //        }
    //        if (i == highscores.highscoreEntryList.Count)
    //            highscores.highscoreEntryList.Add(highscoreEntry);
    //    }


    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_Club_Ranking_Overall(club, json).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            //Debug.Log("My club ranking monthly");
    //        }
    //    });
    //    return json;
    //}
    #endregion
    #region "My club high score monthly"
    //async public static Task<string> Club_Highscore_Monthly(string club, string month, int score, string userID, string name)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = name, userId = userID };
    //    string jsonString = null;
    //    await FBDatabase.Read_Club_Highscore_Monthly(club, month).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //        highscores.highscoreEntryList.Add(highscoreEntry);
    //    }
    //    else
    //    {
    //        int i;
    //        for (i = 0; i < highscores.highscoreEntryList.Count; i++)
    //        {
    //            if (userID == highscores.highscoreEntryList[i].userId)
    //            {
    //                highscores.highscoreEntryList[i].score = score;
    //                if (highscores.highscoreEntryList[i].time_name != name)
    //                    highscores.highscoreEntryList[i].time_name = name;
    //                break;
    //            }
    //        }
    //        if (i == highscores.highscoreEntryList.Count)
    //            highscores.highscoreEntryList.Add(highscoreEntry);
    //    }
    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_Club_Highscore_Monthly(club, month, json).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            //Debug.Log("My club high score monthly");
    //        }
    //    });
    //    return json;
    //}
    #endregion
    #region "All club ranking monthly"
    //async public static Task<string> AllClubs_Ranking_Monthly(string userID, string club, string month, int score, string name)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = name, userId = userID };
    //    string jsonString = null;
    //    await FBDatabase.Read_AllClubs_Ranking_Monthly(month).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //        highscores.highscoreEntryList.Add(highscoreEntry);
    //    }
    //    else
    //    {
    //        int i;
    //        for (i = 0; i < highscores.highscoreEntryList.Count; i++)
    //        {
    //            string nameClub = highscores.highscoreEntryList[i].time_name;
    //            int t = nameClub.Length - club.Length;
    //            if (t < 0) t = 0;
    //            //Debug.LogError(nameClub.Substring(t));

    //            if (userID == highscores.highscoreEntryList[i].userId && nameClub.Substring(t) == club)
    //            {
    //                highscores.highscoreEntryList[i].score = score;
    //                if (highscores.highscoreEntryList[i].time_name != name)
    //                    highscores.highscoreEntryList[i].time_name = name;
    //                break;
    //            }
    //        }
    //        if (i == highscores.highscoreEntryList.Count)
    //            highscores.highscoreEntryList.Add(highscoreEntry);
    //    }


    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_AllClubs_Ranking_Monthly(month, json).ContinueWith(task =>
    //     {
    //         if (task.IsCompleted)
    //         {
    //             //Debug.Log("All club ranking monthly");
    //         }
    //     });
    //    return json;
    //}
    #endregion
    #region "All club ranking overall time"
    //async public static Task<string> AllClubs_Ranking_Overall(string userID, string club, string month, int score, string name)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = name, userId = userID };
    //    string jsonString = null;
    //    await FBDatabase.Read_AllClubs_Ranking_Overall().ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //        highscores.highscoreEntryList.Add(highscoreEntry);
    //    }
    //    else
    //    {
    //        int i;
    //        for (i = 0; i < highscores.highscoreEntryList.Count; i++)
    //        {
    //            string nameClub = highscores.highscoreEntryList[i].time_name;
    //            int t = nameClub.Length - club.Length;
    //            if (t < 0) t = 0;
    //            Debug.LogError(nameClub.Substring(t));

    //            if (userID == highscores.highscoreEntryList[i].userId && nameClub.Substring(t) == club)
    //            {
    //                highscores.highscoreEntryList[i].score = score;
    //                if (highscores.highscoreEntryList[i].time_name != name)
    //                    highscores.highscoreEntryList[i].time_name = name;
    //                break;
    //            }
    //        }
    //        if (i == highscores.highscoreEntryList.Count)
    //            highscores.highscoreEntryList.Add(highscoreEntry);
    //    }


    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_AllClubs_Ranking_Overall(json).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            Debug.Log("All club ranking overall time");
    //        }
    //    });
    //    return json;
    //}
    #endregion
    #region "All club high scores overall time"
    //async public static Task<string> AllClubs_Highscore_Overall(string userID, string club, int score, string name)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = name, userId = userID };
    //    string jsonString = null;
    //    await FBDatabase.Read_AllClubs_Highscores_Overall().ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //        highscores.highscoreEntryList.Add(highscoreEntry);
    //    }
    //    else
    //    {
    //        int i;
    //        for (i = 0; i < highscores.highscoreEntryList.Count; i++)
    //        {
    //            string nameClub = highscores.highscoreEntryList[i].time_name;
    //            int t = nameClub.Length - club.Length;
    //            if (t < 0) t = 0;
    //            //Debug.LogError(nameClub.Substring(t));

    //            if (userID == highscores.highscoreEntryList[i].userId && nameClub.Substring(t) == club)
    //            {
    //                highscores.highscoreEntryList[i].score = score;
    //                if (highscores.highscoreEntryList[i].time_name != name)
    //                    highscores.highscoreEntryList[i].time_name = name;
    //                break;
    //            }
    //        }
    //        if (i == highscores.highscoreEntryList.Count)
    //            highscores.highscoreEntryList.Add(highscoreEntry);
    //    }


    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_AllClubs_Highscores_Overall(json).ContinueWith(task =>
    //       {
    //           if (task.IsCompleted)
    //           {
    //               //Debug.Log("All club high scores overall time");
    //           }
    //       });
    //    return json;
    //}
    #endregion
    #region "All club high score monthly"
    //async public static Task<string> AllClubs_Highscore_Monthly(string userID, string club, string month, int score, string name)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = name, userId = userID };
    //    string jsonString = null;
    //    await FBDatabase.Read_AllClubs_Highscores_Monthly(month).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //        highscores.highscoreEntryList.Add(highscoreEntry);
    //    }
    //    else
    //    {
    //        int i;
    //        for (i = 0; i < highscores.highscoreEntryList.Count; i++)
    //        {
    //            string nameClub = highscores.highscoreEntryList[i].time_name;
    //            //Debug.Log(nameClub.Substring(nameClub.Length - 4));
    //            int t = nameClub.Length - club.Length;
    //            if (t < 0) t = 0;
    //            //Debug.LogError(nameClub.Substring(t));
    //            if (userID == highscores.highscoreEntryList[i].userId && nameClub.Substring(t) == club)
    //            {
    //                highscores.highscoreEntryList[i].score = score;
    //                if (highscores.highscoreEntryList[i].time_name != name)
    //                    highscores.highscoreEntryList[i].time_name = name;
    //                break;
    //            }
    //        }
    //        if (i == highscores.highscoreEntryList.Count)
    //            highscores.highscoreEntryList.Add(highscoreEntry);
    //    }


    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_AllClubs_Highscores_Monthly(month, json).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            //Debug.Log("All club high score monthly");
    //        }
    //    });
    //    return json;
    //}
    #endregion
    #region "All club competition overall time"
    //async public static Task<string> ClubsCompetition(int score, string name)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = name };
    //    string jsonString = null;
    //    await FBDatabase.Read_ClubsCompetition().ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //        highscores.highscoreEntryList.Add(highscoreEntry);
    //    }
    //    else
    //    {
    //        int i;
    //        for (i = 0; i < highscores.highscoreEntryList.Count; i++)
    //        {
    //            if (name == highscores.highscoreEntryList[i].time_name)
    //            {
    //                highscores.highscoreEntryList[i].score = score;
    //                break;
    //            }
    //        }
    //        if (i == highscores.highscoreEntryList.Count)
    //            highscores.highscoreEntryList.Add(highscoreEntry);
    //    }

    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_ClubsCompetition(json).ContinueWith(task =>
    //   {
    //       if (task.IsCompleted)
    //       {
    //           //Debug.Log("All club competition");
    //           done?.Invoke();
    //       }
    //   });
    //    return json;
    //}
    #endregion

    #region All club competition monthly
    //async public static Task<string> ClubsCompetition_Monthly(int score, string month, string name)
    //{
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, time_name = name };
    //    string jsonString = null;
    //    await FBDatabase.Read_ClubsCompetition_Monthly(month).ContinueWith(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            jsonString = task.Result;
    //        }
    //    });
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    //    if (highscores == null)
    //    {
    //        highscores = new Highscores()
    //        {
    //            highscoreEntryList = new List<HighscoreEntry>()
    //        };
    //        highscores.highscoreEntryList.Add(highscoreEntry);
    //    }
    //    else
    //    {
    //        int i;
    //        for (i = 0; i < highscores.highscoreEntryList.Count; i++)
    //        {
    //            if (name == highscores.highscoreEntryList[i].time_name)
    //            {
    //                highscores.highscoreEntryList[i].score = score;
    //                break;
    //            }
    //        }
    //        if (i == highscores.highscoreEntryList.Count)
    //            highscores.highscoreEntryList.Add(highscoreEntry);
    //    }

    //    string json = JsonUtility.ToJson(highscores);
    //    await FBDatabase.Write_ClubsCompetition_Monthly(month, json).ContinueWith(task =>
    //     {
    //         if (task.IsCompleted)
    //         {
    //             //Debug.Log("All club competition");
    //         }
    //     });
    //    return json;
    //}
    #endregion

    public class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    public class HighscoreEntry
    {
        public string userId;
        public int score;
        public string time_name;
    }
}


