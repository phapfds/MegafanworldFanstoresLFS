using System;
using System.Threading.Tasks;
//using Firebase;
//using Firebase.Database;
//using Firebase.Unity.Editor;
using UnityEngine;
using FirebaseWebGL.Scripts.FirebaseBridge;

public class DatabaseAPI : MonoBehaviour
{
    //private DatabaseReference reference;
    //private EventHandler<ChildChangedEventArgs> newMessageListener;
    //private EventHandler<ChildChangedEventArgs> editedMessageListener;
    //private EventHandler<ChildChangedEventArgs> deletedMessageListener;

    //private EventHandler<ChildChangedEventArgs> newRoomListener;

    //public static string url;

    //private void Awake()
    //{
    //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://megafanworld.firebaseio.com/");
    //reference = FirebaseDatabase.DefaultInstance.RootReference;
    //}

    //public void CreateRoom(Room room, Action callback, Action<AggregateException> fallback)
    //{
    //    var roomJSON = StringSerializationAPI.Serialize(typeof(Room), room);
    //    reference.Child("messages").Child("rooms").Push().SetRawJsonValueAsync(roomJSON).ContinueWith(task =>
    //    {
    //        if (task.IsCanceled || task.IsFaulted) fallback(task.Exception);
    //        else callback();
    //    });

    //}

    public void PostMessage(Message message, GameObject gameObject, string callback, string fallback)
    {
        #region "Firebase sdk"
        //var messageJSON = StringSerializationAPI.Serialize(typeof(Message), message);
        //reference.Child("Messages").Child("PublicRoom").Child("Texts").Push().SetRawJsonValueAsync(messageJSON).ContinueWith(task =>
        //{
        //    if (task.IsCanceled || task.IsFaulted) fallback(task.Exception);
        //    else callback();
        //});
        #endregion
        FirebaseDatabase.PushMessage(message.senderNickname, message.senderUserId, message.text, gameObject.name, callback, fallback);
    }

    //public void EditMessage(string messageId, string newText, Action callback, Action<AggregateException> fallback)
    //{
    //var messageJSON = StringSerializationAPI.Serialize(typeof(string), newText);
    //reference.Child($"Messages/PublicRoom/Texts/{messageId}/text").SetRawJsonValueAsync(messageJSON).ContinueWith(task =>
    //{
    //    if (task.IsCanceled || task.IsFaulted) fallback(task.Exception);
    //    else callback();
    //});
    //}

    //public void DeleteMessage(string messageId, Action callback, Action<AggregateException> fallback)
    //{
    //reference.Child($"Messages/PublicRoom/Texts/{messageId}").SetRawJsonValueAsync(null).ContinueWith(task =>
    //{
    //    if (task.IsCanceled || task.IsFaulted) fallback(task.Exception);
    //    else callback();
    //});
    //}

    //internal void ListenForNewRoom(Action<Room, string> callback, Action<AggregateException> fallback)
    //{
    //    void CurrentListener(object o, ChildChangedEventArgs args)
    //    {
    //        if (args.DatabaseError != null) fallback(new AggregateException(new Exception(args.DatabaseError.Message)));
    //        else
    //            callback(
    //                StringSerializationAPI.Deserialize(typeof(Room), args.Snapshot.GetRawJsonValue()) as Room, args.Snapshot.Key);
    //    }
    //    newRoomListener = CurrentListener;

    //    reference.Child("messages").Child("rooms").ChildAdded += newRoomListener;
    //}
    public void ListenForNewMessages(GameObject gameObject, string callback, string fallback)
    {
        //public void ListenForNewMessages(Action<Message, string> callback, Action<AggregateException> fallback)
        //{
        //void CurrentListener(object o, ChildChangedEventArgs args)
        //{
        //    if (args.DatabaseError != null) fallback(new AggregateException(new Exception(args.DatabaseError.Message)));
        //    else
        //        callback(
        //            StringSerializationAPI.Deserialize(typeof(Message), args.Snapshot.GetRawJsonValue()) as Message,
        //            args.Snapshot.Key);
        //}

        //newMessageListener = CurrentListener;

        //reference.Child("Messages").Child("PublicRoom").Child("Texts").ChildAdded += newMessageListener;
        FirebaseDatabase.ListenForNewMessages(gameObject.name, callback, fallback);
    }

    //public void ListenForEditedMessages(Action<string, string> callback, Action<AggregateException> fallback)
    //{
    //void CurrentListener(object o, ChildChangedEventArgs args)
    //{
    //    if (args.DatabaseError != null) fallback(new AggregateException(new Exception(args.DatabaseError.Message)));
    //    else
    //        callback(args.Snapshot.Key,
    //            StringSerializationAPI.Deserialize(typeof(string), args.Snapshot.Child("text").GetRawJsonValue()) as
    //                string);
    //}

    //editedMessageListener = CurrentListener;

    //reference.Child("Messages").Child("PublicRoom").Child("Texts").ChildChanged += editedMessageListener;
    //}

    //public void ListenForDeletedMessages(Action<string> callback, Action<AggregateException> fallback)
    //{
    //void CurrentListener(object o, ChildChangedEventArgs args)
    //{
    //    if (args.DatabaseError != null) fallback(new AggregateException(new Exception(args.DatabaseError.Message)));
    //    else callback(args.Snapshot.Key);
    //}

    //deletedMessageListener = CurrentListener;

    //reference.Child("Messages").Child("PublicRoom").Child("Texts").ChildRemoved += deletedMessageListener;
    //}

    //public void StopListeningForMessages()
    //{
    //    reference.Child("messages").Child(FindRoomHandler.roomName).Child("Texts").ChildAdded -= newMessageListener;
    //    reference.Child("messages").Child(FindRoomHandler.roomName).Child("Texts").ChildChanged -= editedMessageListener;
    //    reference.Child("messages").Child(FindRoomHandler.roomName).Child("Texts").ChildRemoved -= deletedMessageListener;
    //}

    //public void StopListenForRoom()
    //{
    //    reference.Child("messages").Child("rooms").ChildAdded -= newRoomListener;
    //}

    //public void PostUser(UserInfo user, Action callback, Action<AggregateException> fallback)
    //{
    //var messageJSON = StringSerializationAPI.Serialize(typeof(UserInfo), user);
    //reference.Child($"users/{UserInfoManager.Instance.userInfo.userID}").SetRawJsonValueAsync(messageJSON)
    //    .ContinueWith(task =>
    //    {
    //        if (task.IsCanceled || task.IsFaulted) fallback(task.Exception);
    //        else callback();
    //    });
    //}

    //public void GetUser(Action<UserInfo> callback, Action<AggregateException> fallback)
    //reference.Child($"users/{UserInfoManager.Instance.userInfo.userID}").GetValueAsync().ContinueWith(
    //    task =>
    //    {
    //        if (task.IsCanceled || task.IsFaulted) fallback(task.Exception);
    //        else callback(StringSerializationAPI.Deserialize(typeof(UserInfo), task.Result.GetRawJsonValue()) as UserInfo);
    //    });

    //public void AddUser(UserInfo user, Action callback, Action<AggregateException> fallback)
    //{
    //    //foreach (string userid in )
    //    reference.Child("Messages").Child("PublicRoom").Child("RoomInfo").Child("User").Push().SetRawJsonValueAsync(UserInfoManager.Instance.userInfo.userID).ContinueWith(
    //    task =>
    //    {
    //        if (task.IsCanceled || task.IsFaulted) fallback(task.Exception);
    //        else callback();
    //    });

    //    FirebaseDatabase.DefaultInstance
    //.GetReference().OrderByChild("category").EqualTo("livingroom")
    //    reference.Child("messages").Child(FindRoomHandler.roomName).Child("Room info").Child("User").ValueChanged += (object sender2, ValueChangedEventArgs e2) =>
    //{
    //    if (e2.DatabaseError != null)
    //    {
    //        Debug.LogError(e2.DatabaseError.Message);
    //    }


    //    if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
    //    {
    //        foreach (var childSnapshot in e2.Snapshot.Children)
    //        {
    //            var name = childSnapshot.Value.ToString();
    //            Debug.Log(name.ToString());
    //        }
    //    }

    //};
    //}
}
