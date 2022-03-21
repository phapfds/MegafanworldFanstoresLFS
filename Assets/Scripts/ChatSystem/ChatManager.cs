using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public static ChatManager inst;
    public GameObject chatObject;
    private void Awake()
    {
        inst = this;
    }
    public void EnableChatSystem()
    {
        chatObject.SetActive(true);
    }
    public void DisableChatSystem()
    {
        if (chatObject.activeSelf)
            chatObject.SetActive(false);
    }
}
