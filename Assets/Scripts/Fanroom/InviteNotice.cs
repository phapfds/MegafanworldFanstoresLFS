using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirebaseWebGL.Scripts.FirebaseBridge;
public class InviteNotice : MonoBehaviour
{
    public Text inviteText;
    public string userID;
    private void Awake()
    {
        //Debug.Log(userID);
        FirebaseDatabase.GetRawData("Users/" + userID + "/UserInfo/name", gameObject.name, "Notice", null);
    }

    public void Notice(string _name)
    {
        Debug.Log(_name);
        inviteText.text = "You are invited to visit " + _name + "'s fanroom. Touch here to go there.";
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
    }

    public void MoveToHisFanroom()
    {
        //Debug.Log(userID);
        FanroomFriendManager.userID = userID;
        ViewsManager.Instance.LoadSceneByName("FanRoomOfFriend");
    }
}
