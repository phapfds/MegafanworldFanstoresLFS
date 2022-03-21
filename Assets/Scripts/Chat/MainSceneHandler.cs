using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections;
public class MainSceneHandler : MonoBehaviour
{
    public static MainSceneHandler Instance;

    public TMP_InputField textIF;

    public GameObject messagePrefab;
    public Transform messagesContainer;

    public Dictionary<string, MessageHandler> messages = new Dictionary<string, MessageHandler>();

    [SerializeField] ScrollRect scrollRect;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        APIHandler.Instance.databaseAPI.ListenForNewMessages(gameObject, "DisplayData", null);
        //APIHandler.Instance.databaseAPI.ListenForEditedMessages(EditMessage, Debug.Log);
        //APIHandler.Instance.databaseAPI.ListenForDeletedMessages(DeleteMessage, Debug.Log);
    }

    public void DisplayData(string data)
    {
        string[] data_ = data.Split('/');
        CreateMessage(JsonUtility.FromJson<Message>(data_[0]), data_[1]);
    }

    public void SendMessage()
    {
        if (textIF.text != null && textIF.text != "")
        {
            APIHandler.Instance.databaseAPI.PostMessage( new Message(UserInfoManager.Instance.userInfo.name, UserInfoManager.Instance.userInfo.userID, textIF.text), gameObject, "", "");
            textIF.text = "";
            EventSystem.current.SetSelectedGameObject(textIF.gameObject, null);
            textIF.OnPointerClick(null);
        }
    }

    private void CreateMessage(Message message, string messageId)
    {
        var newMessage = Instantiate(messagePrefab, transform.position, Quaternion.identity);
        newMessage.transform.SetParent(messagesContainer, false);

        var newMessageHandler = newMessage.GetComponent<MessageHandler>();
        newMessageHandler.message = message;
        newMessageHandler.messageId = messageId;
        if (UserInfoManager.Instance.userInfo != null)
            newMessageHandler.isOwner = message.senderUserId == UserInfoManager.Instance.userInfo.userID;

        messages.Add(messageId, newMessageHandler);
        StartCoroutine(ScrollToBottom());
    }


    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }

    private void EditMessage(string messageId, string newText) => messages[messageId].text.text = $"{messages[messageId].message.senderNickname}: {newText}";

    private void DeleteMessage(string messageId)
    {
        Destroy(messages[messageId].gameObject);
        messages.Remove(messageId);
    }

    private void Update()
    {
        //scrollRect.normalizedPosition = new Vector2(0, 0);
        if (Input.GetKeyDown(KeyCode.Return))
            SendMessage();

    }
}
