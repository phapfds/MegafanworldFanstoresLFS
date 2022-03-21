//using UnityEngine;
//using Proyecto26;

//public class SignInResponse
//{
//    public string kind;
//    public string localId;
//    public string email;
//    public string kidisplayName;
//    public string idToken;
//    public bool registered;
//    public string refreshToken;
//    public string expiresIn;
//}

//public class AuthRESTAPI : MonoBehaviour
//{
//    private const string apiKey = "AIzaSyAijSXnGkTNUOEXniQOIIZ35tZdS2D5JhU";

//    public static void SignIn(string email, string password)
//    {
//        var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
//        RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}", payLoad)
//            .Then(res =>
//            {
//                Debug.Log("Firebase rest api: Login");
//                Auth.login?.Invoke(true, JsonUtility.FromJson<SignInResponse>(res.Text).localId);
//            })
//            .Catch(error =>
//            {
//                //Debug.LogError(error.Message);
//                Auth.login?.Invoke(false, null);
//            });
//    }
//    public static void SignUp(string email, string password)
//    {
//        var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
//        RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}", payLoad)
//            .Then(res =>
//            {
//                Debug.Log("Firebase rest api: SignUp");
//                Auth.signUp?.Invoke(true);
//            })
//            .Catch(error =>
//            {
//                //Debug.LogError(error.Message);
//                Auth.signUp?.Invoke(false);
//            });
//    }

//}
