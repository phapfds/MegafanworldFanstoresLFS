

#if UNITY_STANDALONE_WIN || UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Threading.Tasks;
using System;

public class FBDatabase : MonoBehaviour
{
    private static DatabaseReference reference;
    private readonly static string url = "https://megafanworld.firebaseio.com/";

    private void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(url);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
#region "USER INFO"
    //User infor
    async public static Task<bool> CheckAccount(string userID_local)
    {
        bool result = false;
        await reference.Child("Users").Child(userID_local).Child("UserInfo").Child("userID").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                result = false;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (task.Result.Value != null)
                    if (userID_local == task.Result.Value.ToString() && UserInfoManager.Instance.userInfo.email != null && UserInfoManager.Instance.userInfo.email != "")
                        result = true;
            }
        });
        return result;
    }
    async public static Task<bool> SaveUserInfo(string userID, string data)
    {
        bool result = false;
        await reference.Child("Users").Child(userID).Child("UserInfo").SetRawJsonValueAsync(data).ContinueWith(task =>
        {
            if (task.IsCompleted)
                result = true;
        });
        return result;
    }
    async public static Task<string> ReadUserInfoOnce(string userID)
    {
        string data = null; ;
        await reference.Child("Users").Child(userID).Child("UserInfo").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
#endregion
#region "LEADER BOARD"
    //-------------------------------------------------------------------------------------------------------
    //For monthly tables
    async public static Task<bool> Write_MyTable_Club_Monthly(string userID, string month, string club, string timeScore)
    {
        bool result = false;

        await reference.Child("Users").Child(userID).Child("scores").Child(month).Child(club).SetRawJsonValueAsync(timeScore).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    async public static Task<string> Read_MyTable_Club_Monthly(string userID, string month, string club)
    {
        string data = null; ;

        await reference.Child("Users").Child(userID).Child("scores").Child(month).Child(club).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    //To display in my high scores table
    async public static Task<bool> Write_MyTable_AllClub_Monthly(string userID, string month, string club, string timeScore)
    {
        bool result = false;

        await reference.Child("Users").Child(userID).Child("scores").Child(month).Child("allClub").SetRawJsonValueAsync(timeScore).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    async public static Task<string> Read_MyTable_AllClub_Monthly(string userID, string month, string club)
    {
        string data = null; ;

        await reference.Child("Users").Child(userID).Child("scores").Child(month).Child("allClub").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    //For overall time tables
    async public static Task<bool> Write_MyTable_Club_Overall(string userID, string club, string timeScore)
    {
        bool result = false;

        await reference.Child("Users").Child(userID).Child("scores").Child("2020").Child(club).SetRawJsonValueAsync(timeScore).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    async public static Task<string> Read_MyTable_Club_Overall(string userID, string club)
    {
        string data = null; ;

        await reference.Child("Users").Child(userID).Child("scores").Child("2020").Child(club).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    //-------------------------------------------------------------------------------------------------------
    //Club ranking monthly
    async public static Task<string> Read_Club_Ranking_Monthly(string club, string month)
    {
        string data = null; ;

        await reference.Child("ScoreTables").Child("ClubRankingMonthly").Child(month).Child(club).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    async public static Task<bool> Write_Club_Ranking_Monthly(string club, string month, string name_score)
    {
        bool result = false;

        await reference.Child("ScoreTables").Child("ClubRankingMonthly").Child(month).Child(club).SetRawJsonValueAsync(name_score).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    //Club ranking overall
    async public static Task<string> Read_Club_Ranking_Overall(string club)
    {
        string data = null; ;

        await reference.Child("ScoreTables").Child("ClubRankingOverall").Child(club).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    async public static Task<bool> Write_Club_Ranking_Overall(string club, string name_score)
    {
        bool result = false;

        await reference.Child("ScoreTables").Child("ClubRankingOverall").Child(club).SetRawJsonValueAsync(name_score).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    //Club high scores monthly
    async public static Task<string> Read_Club_Highscore_Monthly(string club, string month)
    {
        string data = null; ;

        await reference.Child("ScoreTables").Child("ClubHighscoresMonthly").Child(month).Child(club).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    async public static Task<bool> Write_Club_Highscore_Monthly(string club, string month, string name_score)
    {
        bool result = false;

        await reference.Child("ScoreTables").Child("ClubHighscoresMonthly").Child(month).Child(club).SetRawJsonValueAsync(name_score).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    //-------------------------------------------------------------------------------------------------------
    //All clubs ranking monthly
    async public static Task<string> Read_AllClubs_Ranking_Monthly(string month)
    {
        string data = null; ;

        await reference.Child("ScoreTables").Child("AllClubsRankingMonthly").Child(month).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    async public static Task<bool> Write_AllClubs_Ranking_Monthly(string month, string name_score)
    {
        bool result = false;

        await reference.Child("ScoreTables").Child("AllClubsRankingMonthly").Child(month).SetRawJsonValueAsync(name_score).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    //All clubs ranking overall time
    async public static Task<string> Read_AllClubs_Ranking_Overall()
    {
        string data = null; ;

        await reference.Child("ScoreTables").Child("AllClubsRankingOverall").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    async public static Task<bool> Write_AllClubs_Ranking_Overall(string name_score)
    {
        bool result = false;

        await reference.Child("ScoreTables").Child("AllClubsRankingOverall").SetRawJsonValueAsync(name_score).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    //All clubs high scores monthly
    async public static Task<string> Read_AllClubs_Highscores_Monthly(string month)
    {
        string data = null; ;

        await reference.Child("ScoreTables").Child("AllClubsHighscoresMonthly").Child(month).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    async public static Task<bool> Write_AllClubs_Highscores_Monthly(string month, string name_score)
    {
        bool result = false;

        await reference.Child("ScoreTables").Child("AllClubsHighscoresMonthly").Child(month).SetRawJsonValueAsync(name_score).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    //All clubs high scores overall time
    async public static Task<string> Read_AllClubs_Highscores_Overall()
    {
        string data = null; ;

        await reference.Child("ScoreTables").Child("AllClubsHighscoresOverall").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    async public static Task<bool> Write_AllClubs_Highscores_Overall(string name_score)
    {
        bool result = false;

        await reference.Child("ScoreTables").Child("AllClubsHighscoresOverall").SetRawJsonValueAsync(name_score).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    //-------------------------------------------------------------------------------------------------------
    //Clubs competition overall time
    async public static Task<string> Read_ClubsCompetition()
    {
        string data = null; ;

        await reference.Child("ScoreTables").Child("ClubsCompetition").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    async public static Task<bool> Write_ClubsCompetition(string name_score)
    {
        bool result = false;

        await reference.Child("ScoreTables").Child("ClubsCompetition").SetRawJsonValueAsync(name_score).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    //-------------------------------------------------------------------------------------------------------
    //Clubs competition monthly
    async public static Task<string> Read_ClubsCompetition_Monthly(string month)
    {
        string data = null; ;

        await reference.Child("ScoreTables").Child("ClubsCompetitionMonthly").Child(month).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    async public static Task<bool> Write_ClubsCompetition_Monthly(string month, string name_score)
    {
        bool result = false;

        await reference.Child("ScoreTables").Child("ClubsCompetitionMonthly").Child(month).SetRawJsonValueAsync(name_score).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    //Version
    async public static Task<string> GetVersion()
    {
        string data = null; ;

        await reference.Child("Version").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
    //Credits
    async public static Task<int> GetCredits(string userID)
    {
        int data = 0;

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        await reference.Child("Users").Child(userID).Child("UserInfo").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = 0;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = Convert.ToInt32(snapshot.GetRawJsonValue());
            }
        });
        return data;
    }
    async public static Task<bool> SetCredits(string userID, int credits)
    {
        bool result = false;

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        await reference.Child("Users").Child(userID).Child("UserInfo").Child("credits").SetValueAsync(credits).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    //------------------------------------------------------------------------------------------------------------------------
    async public static Task<bool> WriteMessage(string userID, string message)
    {
        bool result = false;

        await reference.Child("Users").Child(userID).Child("Message").SetValueAsync(message).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                result = true;
            }
        });
        return result;
    }
    async public static Task<string> ReadMessage(string userID)
    {
        string data = null;

        await reference.Child("Users").Child(userID).Child("Message").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                data = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                data = snapshot.GetRawJsonValue();
            }
        });
        return data;
    }
#endregion
#region "SOCIAL NETWORK"
    async public static void WriteUserIDsInvitedToFanroom(string email, string userIDs, Action callback)
    {

        await reference.Child("SocialNetwork").Child("InviteToFanroom").Child(email.Replace(".", "%2E")).SetValueAsync(userIDs).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                callback?.Invoke();
            }
        });
    }

    async public static void ReadUserIDsInvitedToFanroom(string email, Action<string> callback)
    {
        await reference.Child("SocialNetwork").Child("InviteToFanroom").Child(email.Replace(".", "%2E")).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
            }
            else if (task.IsCompleted)
            {
                String data = null;
                if (task.Result.GetRawJsonValue() != null)
                    data = task.Result.GetRawJsonValue().Replace("\"", "");
                callback(data);
            }
        });
    }
    async public static void ReadUserNameInvitedToFanroom(string userID, Action<string> callback)
    {
        await reference.Child("Users").Child(userID).Child("UserInfo").Child("name").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
            }
            else if (task.IsCompleted)
            {
                callback(task.Result.GetRawJsonValue().Replace("\"", ""));
            }
        });
    }

#endregion
#region "FANROOMDATABASE"
    async public static void WriteFanroomData(string userID, string data, Action callback)
    {

        await reference.Child("Users").Child(userID).Child("FanroomItem").SetRawJsonValueAsync(data).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                callback?.Invoke();
            }
        });
    }

    async public static void ReadFanroomData(string userID, Action<string> callback)
    {

        await reference.Child("Users").Child(userID).Child("FanroomItem").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result.GetRawJsonValue() != null)
                    callback?.Invoke(task.Result.GetRawJsonValue());
            }
        });
    }
    async public static void WritePostStatusData(string userID, string data, Action callback)
    {
        await reference.Child("Users").Child(userID).Child("StatusFriend").SetRawJsonValueAsync(data).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                callback?.Invoke();
            }
        });
    }
    async public static void ReadPostStatusData(string userID, Action<string> callback)
    {

        await reference.Child("Users").Child(userID).Child("StatusFriend").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result.GetRawJsonValue() != null)
                    callback?.Invoke(task.Result.GetRawJsonValue());
            }
        });
    }
#endregion
}
#endif