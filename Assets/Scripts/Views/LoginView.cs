using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
public class LoginView : Views
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] GameObject success;
    [SerializeField] GameObject fail;

    private bool loginSucess;
    private bool loginFail;
    private string userId;

    [SerializeField] Image exit;
    public override void OnAwake()
    {
        base.OnAwake();
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void SetUp()
    {
        base.SetUp();
        Auth.login += OnLogin;
        success.SetActive(false);
        fail.SetActive(false);
        exit.gameObject.SetActive(false);
    }

    public void OnLogin(bool ob, string userId)
    {
        if (ob)
        {
            this.userId = userId;
            loginSucess = true;
        }
        else
        {
            loginFail = true;
        }
    }

    public void Login()
    {

        Auth.SignIn(email.text, password.text);
    }
    public void MoveToSetup()
    {
        loginSucess = false;
        UserInfoManager.Instance.GetData(userId, gameObject, "MoveToSetup_");

    }

    public void MoveToSetup_(string data)
    {
        if (data != "null" && data != null && data != "")
            UserInfoManager.Instance.userInfo = JsonUtility.FromJson<UserInfo>(data);

        UserInfoManager.Instance.userInfo.userID = userId;

        if (UserInfoManager.Instance.userInfo.email == "" || UserInfoManager.Instance.userInfo.email == "null"|| UserInfoManager.Instance.userInfo.email == null)
        {
            byte[] decodedBytes = Convert.FromBase64String(UserInfoManager.Instance.userInfo.userID.Replace("-", "=").Replace(".", "%2E"));
            string decodedText = Encoding.UTF8.GetString(decodedBytes);
            UserInfoManager.Instance.userInfo.email = decodedText;

        }

        success.SetActive(true);
        StartCoroutine(AutoLoadFanroomData());
        FanroomDatabase.ins.GetData(gameObject, "LoadFanroomData_", "FallBack");
    }

    IEnumerator AutoLoadFanroomData()
    {
        yield return new WaitForSeconds(5);
        ViewsManager.Instance.LoadSceneByName("Setup");
    }

    public void FallBack(string data)
    {
        Debug.Log(data);
        ViewsManager.Instance.LoadSceneByName("Setup");
    }

    public void LoadFanroomData_(string data)
    {
        if (data != null && data != "" && data != "null")
        {
            FanroomDatabase.ins.fanroomItem = JsonUtility.FromJson<FanstoreItemList>(data);
        }
        ViewsManager.Instance.LoadSceneByName("Setup");
    }

    public void SignUp()
    {
        ViewsManager.Instance.ChangeView(ViewType.SignUpView);
    }
    public void ForgorPassWord()
    {
        ViewsManager.Instance.ChangeView(ViewType.ForgotPassView);
    }
    public override void OnUpdate()
    {
        if (loginSucess)
        {
            MoveToSetup();
        }
        if (loginFail)
        {
            loginFail = false;
            fail.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            ShowNotice(exit);
        }
    }

    public void ShowNotice(Image notice)
    {
        notice.gameObject.SetActive(true);
    }
    public void OffNotice(Image notice)
    {
        notice.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void OnDisable()
    {
        Auth.login -= OnLogin;
        StopAllCoroutines();
    }
}

