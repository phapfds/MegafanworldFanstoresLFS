using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirebaseWebGL.Scripts.FirebaseBridge;
public class StatusPostManager : MonoBehaviour
{
    [Header ("REFERENCE")]
    [HideInInspector] public StatusPost statusPost;
    [HideInInspector] public string userID;
    //private void Awake()
    //{
        //FirebaseDatabase.GetRawData("Users/" + userID + "/StatusFriend", gameObject.name, "LoadStatusData", null);
    //}

    //public void LoadStatusData(string data)
    //{
    //    Debug.Log(data);
    //    if (data != null && data != "" && data != null)
    //        statusPost = JsonUtility.FromJson<StatusPost>(data);
    //}

}
