using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageManager_Login : MonoBehaviour
{
    //SceneLogin
    public Text email;
    public Text passWord;
    public Text orCreateAccout;
    public Text forgotPassword;
    public Text success;
    public Text fail;

    public TextMeshProUGUI quitQuestion;
    public TextMeshProUGUI yes;
    public TextMeshProUGUI no;

    void Update()
    {
        if (LanguageManager.language == LanguageType.German)
        {
            email.text = "E-Mailadresse";
            passWord.text = "Passwort";
            orCreateAccout.text = "Erstelle einen Zugang";
            forgotPassword.text = "Passwort vergessen?";
            success.text = "Login erfolgreich";
            fail.text = "Falscher Username oder Passwort";
            quitQuestion.text = "Möchtest du wirklich aufhören?";
            yes.text = "Ja";
            no.text = "Nein";

        }
        else
        {
            email.text = "Email address";
            passWord.text = "Password";
            orCreateAccout.text = "Do not have an account? Sign up";
            forgotPassword.text = "Forgot password?";
            success.text = "Congratulations!\nYou've successfully logged in,";
            fail.text = "Your email password is incorrect,\nPlease try again!";
        }
    }
}
