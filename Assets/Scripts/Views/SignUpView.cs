using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirebaseWebGL.Scripts.Objects;
using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;

public class SignUpView : Views
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] Image Success;
    [SerializeField] Image Fail;

    public override void SetUp()
    {
        Success.gameObject.SetActive(false);
        Fail.gameObject.SetActive(false);
    }
    public void SignUp()
    {
        Auth.ins.SignUp(email.text, password.text, gameObject, "SignUpSuccess", "SignUpFail");
    }

    public void SignUpSuccess(string data)
    {
        Debug.Log(data);
        StartCoroutine(SignUpSuccess_());
    }

    public void SignUpFail(string data)
    {

        var parsedError = StringSerializationAPI.Deserialize(typeof(FirebaseError), data) as FirebaseError;
        Debug.Log(parsedError.message);
        Fail.gameObject.SetActive(true);
    }

    public void ChangeToLogin()
    {
        ViewsManager.Instance.ChangeView(ViewType.LoginView);

    }
    public void ResetPassword()
    {
        ViewsManager.Instance.ChangeView(ViewType.ForgotPassView);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    IEnumerator SignUpSuccess_()
    {
        FirebaseAuth.SignOut(null, null, null);
        Success.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        ViewsManager.Instance.ChangeView(ViewType.LoginView);
    }
}

