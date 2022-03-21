using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager_ForgotPassword : MonoBehaviour
{
    public Text email;
    public Text sendEmail;
    public Text signup;
    public Text success;
    public Text fail;

    void OnEnable()
    {
        if (LanguageManager.language == LanguageType.German)
        {

            email.text = "E-Mailadresse";
            sendEmail.text = "Senden E-Mail";
            signup.text = "Erstelle einen Zugang";
            success.text = "Bitte überprüfen Sie die Mail zum Zurücksetzen des Passworts!";
            fail.text = "Falscher E-Mail";
        }
        else
        {
            email.text = "Email address";
            sendEmail.text = "Send email";
            signup.text = "Do not have an account? Sign up";
            success.text = "Please check the reset password mail!";
            fail.text = "Wrong email";
        }
    }
}
