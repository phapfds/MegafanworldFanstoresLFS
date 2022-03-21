using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using FirebaseWebGL.Scripts.FirebaseBridge;

public class BootLoader : MonoBehaviour
{
    UserInfo userInfotest;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        //PlayerPrefs.DeleteAll();

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality", 1), true);
        //if (PlayerPrefs.GetString("DeletedCatched") != "Yes")
        //{
        //    PlayerPrefs.DeleteAll();
        //    PlayerPrefs.SetString("DeletedCatched", "Yes");
        //}
    }
    private void Start()
    {
#if UNITY_EDITOR
        //ViewsManager.Instance.LoadSceneByName("FanRoomOfFriend");
        //ViewsManager.Instance.LoadSceneByName("UEFAStore");
        //ViewsManager.Instance.LoadSceneByName("MunichStadium");
        //ViewsManager.Instance.LoadSceneByName("DortmundStore");
        //ViewsManager.Instance.LoadSceneByName("HamburgStore");
        ViewsManager.Instance.LoadSceneByName("FrankfurtStoreNew");

#else
        ViewsManager.Instance.LoadSceneByName("Login");
#endif
        //ViewsManager.Instance.LoadSceneByName("Freekick");
        //ViewsManager.Instance.LoadSceneByName("Munich");
        //ViewsManager.Instance.LoadSceneByName("Frankfurt_DesktopVersion"); 
        //ViewsManager.Instance.LoadSceneByName("FrankfurtStadium_DesktopVersion"); //WalkingStreet_DesktopVersion
    }
}
