using TMPro;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject editButton;
    public GameObject deleteButton;

    public Message message;
    public string messageId;
    public bool isOwner;

    public Color ownerColor;

    private void Start()
    {
        text.text = $"{message.senderNickname}: {message.text}";

        if (isOwner) text.color = ownerColor;

        if (!isOwner) return;
        //editButton.SetActive(true);
        //deleteButton.SetActive(true);
    }

    private void OnEnable()
    {
        if (UserInfoManager.Instance.userInfo != null)
            isOwner = message.senderUserId == UserInfoManager.Instance.userInfo.userID;
        if (isOwner)
            text.color = ownerColor;
        else
            text.color = Color.white;
    }

    //public void EditMessage() =>
    //    APIHandler.Instance.databaseAPI.EditMessage(messageId, MainSceneHandler.Instance.textIF.text,
    //        () => Debug.Log("Message edited!"), Debug.Log);

    //public void DeleteMessage() =>
    //    APIHandler.Instance.databaseAPI.DeleteMessage(messageId, () => Debug.Log("Message deleted!"), Debug.Log);
}
