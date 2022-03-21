using Proyecto26;
using UnityEngine;
using System.Threading.Tasks;
using System;
using RSG;
public class DatabaseRESTAPI : MonoBehaviour
{
    private readonly static string url = "https://megafanworld.firebaseio.com/";
    #region "Acconut"
    public static void CheckAccount(string userIDLocal, Action Exist, Action Empty)
    {
        Debug.LogError("Firebase Rest api: Check account");
        RestClient.Get<UserInfo>($"{url}Users/{userIDLocal}/UserInfo.json")
           .Then(res =>
           {
               if (res != null && res.userID == userIDLocal)
               {
                   Exist();
               }
               else
                   Empty();
           });
    }
    public static void SaveUserInfo(string userID, string data, Action callback)
    {
        Debug.LogError("Firebase Rest api: Save user info");
        RestClient.Put<UserInfo>($"{url}Users/{userID}/UserInfo.json", data)
            .Then(res =>
            {
                callback();
            });
    }
    public static void ReadUserInfoOnce(string userID, Action<UserInfo> callback)
    {
        Debug.LogError("Firebase Rest api: Read user info");
        RestClient.Get<UserInfo>($"{url}Users/{userID}/UserInfo.json")
           .Then(res =>
           {
               callback(res);
           });
    }
    #endregion
    #region"For monthly tables"
    public static void Write_MyTable_Club_Monthly(string userID, string month, string club, string timeScore, Action callback)
    {
        Debug.LogError("Firebase Rest api: Write_MyTable_Club_Monthly");
        RestClient.Put<string>($"{url}Users/{userID}/scores/{month}/{club}.json", timeScore)
            .Then(res =>
            {
                callback();
            });
    }
    public static void Read_MyTable_Club_Monthly(string userID, string month, string club, Action<string> callback)
    {
        Debug.LogError("Firebase Rest api:  Read_MyTable_Club_Monthly");
        RestClient.Get<HighscoresManager.Highscores>($"{url}Users/{userID}/scores/{month}/{club}.json")
           .Then(res =>
           {
               //Debug.LogError(JsonUtility.ToJson(res));
               callback(JsonUtility.ToJson(res));
           });
    }
    #endregion
    #region"To display in my high scores table"
    public static void Write_MyTable_AllClub_Monthly(string userID, string month, string timeScore, Action callback)
    {
        Debug.LogError("Firebase Rest api: Write_MyTable_AllClub_Monthly");
        RestClient.Put<string>($"{url}Users/{userID}/scores/{month}/allClub.json", timeScore)
            .Then(res =>
            {
                callback();
            });
    }
    public static void Read_MyTable_AllClub_Monthly(string userID, string month, Action<string> callback)
    {
        Debug.LogError("Firebase Rest api:  Read_MyTable_AllClub_Monthly");
        RestClient.Get<HighscoresManager.Highscores>($"{url}Users/{userID}/scores/{month}/allClub.json")
            .Then(res =>
            {
                Debug.LogError(JsonUtility.ToJson(res));
                callback(JsonUtility.ToJson(res));
            });
    }
    #endregion
    #region "For overall time tables"
    public static void Write_MyTable_Club_Overall(string userID, string club, string timeScore, Action callback)
    {
        Debug.LogError("Firebase Rest api: Write_MyTable_Club_Overall");
        RestClient.Put<string>($"{url}Users/{userID}/scores/2020/{club}.json", timeScore)
            .Then(res =>
            {
                callback();
            });
    }
    public static void Read_MyTable_Club_Overall(string userID, string club, Action<string> callback)
    {
        Debug.LogError("Firebase Rest api:  Read_MyTable_Club_Overall");
        RestClient.Get<HighscoresManager.Highscores>($"{url}Users/{userID}/scores/2020/{club}.json")
           .Then(res =>
           {
               //Debug.LogError(JsonUtility.ToJson(res));
               callback(JsonUtility.ToJson(res));
           });
    }
    #endregion
    #region "Club high scores monthly"
    public static void Read_Club_Highscore_Monthly(string club, string month, Action<string> callback)
    {
        Debug.LogError("Firebase Rest api:  Read_Club_Highscore_Monthly");
        RestClient.Get<HighscoresManager.Highscores>($"{url}ScoreTables/ClubHighscoresMonthly/{month}/{club}.json")
           .Then(res =>
           {
               callback(JsonUtility.ToJson(res));
           });
    }
    public static void Write_Club_Highscore_Monthly(string club, string month, string name_score, Action callback)
    {
        Debug.LogError("Firebase Rest api: Write_Club_Highscore_Monthly");
        RestClient.Put<string>($"{url}ScoreTables/ClubHighscoresMonthly/{month}/{club}.json", name_score)
            .Then(res =>
            {
                callback();
            });
    }
    #endregion
}