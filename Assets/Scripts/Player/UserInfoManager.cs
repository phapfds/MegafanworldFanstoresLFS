using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirebaseWebGL.Scripts.FirebaseBridge;
using System;

public enum Club
{
    Berlin,
    Frankfurt,
    Bremen,
    Dortmund,
    Dresden,
    Freiburg,
    Gelsenkirchen,
    Hamburg,
    Hannover,
    Monchengladbach,
    Munchen,
    Nuremberg,
    HamburgPirates,
    Wolfsburg,
    Neutral
}

public class UserInfo
{
    public string email;
    public string userID;
    public string name;
    public int coinsNum;
    public Club club;
}
public class UserInfoManager : Singleton<UserInfoManager>
{
    public UserInfo userInfo = new UserInfo();
    [SerializeField] List<Texture> shirtTex;
    //[SerializeField] Material shirtMat;

    private void Awake()
    {
        //userInfo.club = Club.Frankfurt;
    }

    public void SaveData()
    {
        FirebaseDatabase.PostUserInfo(userInfo.email, userInfo.userID, userInfo.name, userInfo.coinsNum, (int)userInfo.club, gameObject.name, "DisplaySaveUserinfoSuccess", null);
    }

    public void DisplaySaveUserinfoSuccess(string data)
    {
        Debug.Log(data);
    }
    public void GetData(string userID)
    {
        FirebaseDatabase.GetJSON("Users/" + userID + "/UserInfo", gameObject.name, "GetUserInfo", null);
    }

    public void GetData(string userID, GameObject gameObject, string callback )
    {
        FirebaseDatabase.GetJSON("Users/" + userID + "/UserInfo", gameObject.name, callback, null);
    }

    public void GetUserInfo(string data)
    {
        Debug.Log(data);
        userInfo = JsonUtility.FromJson<UserInfo>(data);
    }

    public void ChangeShirtMaterial(int shirtIndex)
    {
        //shirtMat.mainTexture = shirtTex[shirtIndex];
    }
}
