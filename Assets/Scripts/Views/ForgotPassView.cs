using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ForgotPassView : Views
{
    [SerializeField] InputField email;
    [SerializeField] Image sucess;
    [SerializeField] Image fail;
    private bool resetSuccess;
    private bool resetFail;

    public override void OnAwake()
    {
        base.OnAwake();
        Auth.resetPassWord += (ob) =>
        {
            if (ob) resetSuccess = true;
            else
                resetFail = true;
        };
    }
    public void ResetPassWord()
    {
        //Auth.ResetPassword(email.text);
    }
    IEnumerator ResetSucess()
    {
        resetSuccess = false;
        sucess.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        ViewsManager.Instance.ChangeView(ViewType.LoginView);
    }
    public override void SetUp()
    {
        base.SetUp();
        sucess.gameObject.SetActive(false);
        fail.gameObject.SetActive(false);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (resetSuccess)
            StartCoroutine(ResetSucess());
        if (resetFail)
        {
            resetFail = false;
            fail.gameObject.SetActive(true);
        }
    }
    public void MoveToSignUp()
    {
        ViewsManager.Instance.ChangeView(ViewType.SignUpView);
    }

}
