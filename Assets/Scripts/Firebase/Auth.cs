using System;
using UnityEngine;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;
using System.Text;
public class Auth : MonoBehaviour
{
    public static Auth ins;
    public static Action<bool, string> login;
    public static Action<bool> resetPassWord;

    public bool autoLogin;


    private void Start()
    {
        ins = this;
        FirebaseAuth.OnAuthStateChanged(gameObject.name, "DisplayUserInfo", "DisplayInfo");

        if (autoLogin)
        {
            URLParameters.Instance.RegisterOnceOnDone((x) =>
            {
                if (URLParameters.Instance.SearchParameters.ContainsKey("email"))
                {
                    Debug.Log(URLParameters.Instance.SearchParameters["email"].ToString());
                    FanroomFriendManager.userID = URLParameters.Instance.SearchParameters["email"].Replace("%3D", "-");
                    FanroomFriendView fr = ViewsManager.Instance.dicViews[ViewType.FanroomFriendView] as FanroomFriendView;
                    fr.backButton.gameObject.SetActive(false);
                    fr.postMessage.gameObject.SetActive(false);
                    ViewsManager.Instance.LoadSceneByName("FanRoomOfFriend");
                }
                else
                {
                    string userID = URLParameters.Instance.SearchParameters["uid"];
                    string timestamp = URLParameters.Instance.SearchParameters["timestamp"];
                    string hash = URLParameters.Instance.SearchParameters["hash"];

                    var timestampUnity = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

                    if (timestampUnity - Convert.ToDouble(timestamp) < 600)
                    {
                        if (MakeSHA1.Make("fdssoft" + timestamp + userID) == hash)
                        {
                            login?.Invoke(true, URLParameters.Instance.SearchParameters["uid"]);
                        }
                        else
                            login?.Invoke(false, null);
                    }
                    else
                        login?.Invoke(false, null);
                }

            });
        }

    }
    #region "SIGN IN"
    public void DisplayUserInfo(string user)
    {
        var parsedUser = StringSerializationAPI.Deserialize(typeof(FirebaseUser), user) as FirebaseUser;
        //Debug.Log($"Email: {parsedUser.email}, UserId: {parsedUser.uid}, EmailVerified: {parsedUser.isEmailVerified}");
        //login?.Invoke(true, parsedUser.uid);
    }
    public void DisplayInfo(string info)
    {
        //Debug.Log(info);
    }
    public void DisplayErrorObject(string error)
    {
        var parsedError = StringSerializationAPI.Deserialize(typeof(FirebaseError), error) as FirebaseError;
        //Debug.Log(parsedError.message);
        login?.Invoke(false, null);

    }
    public static void SignIn(string email, string password)
    {
        FirebaseAuth.SignInWithEmailAndPassword(email, password, ins.gameObject.name, "DisplayInfo", "DisplayErrorObject");
    }
    #endregion

    public void SignUp(string email, string password, GameObject gameObject, string callback, string fallback)
    {
        FirebaseAuth.CreateUserWithEmailAndPassword(email, password, gameObject.name, callback, fallback);
    }

}
#if UNITY_STANDALONE_WIN || UNITY_ANDROID
    private static FirebaseAuth auth;
    public void Awake()
    {
        //string version = null;
        //await FirebaseDatabse.GetVersion().ContinueWith(task=> {
        //    if (task.IsCompleted)
        //        version = task.Result;
        //});
        //if (version != "\"0.0.0\"")
        //{
        //    Debug.Log(version);
        //    Debug.LogError("Old version");
        //}
        auth = FirebaseAuth.DefaultInstance;
    }
    async public void Start()
    {
        if (PlatformManager.inst.platformType == PlatformType.Windows)
        {
            if (UserInfoManager.Instance.userInfo != null && UserInfoManager.Instance.userInfo.userID != "")
            {
                if (await FBDatabase.CheckAccount(UserInfoManager.Instance.userInfo.userID))
                {
                    isLoggedin?.Invoke(true);
                }
                else
                    isLoggedin?.Invoke(false);
            }
            else
            {
                isLoggedin?.Invoke(false);
            }
        }
    }

    public static void SignUp(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                signUp?.Invoke(false);
                return;
            }
            if (task.IsFaulted)
            {
                signUp?.Invoke(false);
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            signUp?.Invoke(true);

        });
    }

    public static void SignIn(string email, string password)
    {

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {

                login?.Invoke(false, null);
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {

                login?.Invoke(false, null);
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            //Debug.LogError("Login invoke action");
            login?.Invoke(true, newUser.UserId);
        });
    }
    public static void ResetPassword(string email)
    {
        auth.SendPasswordResetEmailAsync(email).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                resetPassWord?.Invoke(false);
                Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                resetPassWord?.Invoke(false);
                Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                return;
            }
            resetPassWord?.Invoke(true);
            Debug.Log("Password reset email sent successfully.");
        });
    }


#endif

