using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager_SignUp : MonoBehaviour
{
    //SceneLogin
    public Text email;
    public Text passWord;
    public Text alreadyHaveAccount;
    public Text success;
    public Text fail;
    //public Text forgotPassword;



    void OnEnable()
    {
        if (LanguageManager.language == LanguageType.German)
        {
            email.text = "E-Mailadresse";
            passWord.text = "Passwort";
            alreadyHaveAccount.text = "Sie haben bereits ein Konto? Einloggen";
            success.text = "SignUp erfolgreich";
            fail.text = "Falscher Username oder Passwort";
            //forgotPassword.text = "Passwort vergessen?";


        }
        else
        {
            email.text = "Email address";
            passWord.text = "Password";
            alreadyHaveAccount.text = "Already have an account? Login";
            success.text = "Signup successfully";
            fail.text = "Wrong username or password";
            //forgotPassword.text = "Forgot password?";

        }
    }
}
