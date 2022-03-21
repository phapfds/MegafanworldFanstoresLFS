using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageManager_Profile : MonoBehaviour
{
    public Text characterName;
    public Text logout;
    //public TextMeshProUGUI logoutAsk;
    //public TextMeshProUGUI yes;
    //public TextMeshProUGUI no;
    public TextMeshProUGUI quitQuestion;
    public TextMeshProUGUI yes;
    public TextMeshProUGUI no;

    void Update()
    {
        if (LanguageManager.language == LanguageType.German)
        {
            if (characterName.text == "")
            {
                characterName.text = "Spielername";
            }

            logout.text = "Ausloggen";
            //logoutAsk.text = "Wirklich abmelden?";
            //yes.text = "Ja";
            //no.text = "Nein";
            quitQuestion.text = "Möchtest du wirklich aufhören?";
            yes.text = "Ja";
            no.text = "Nein";

        }
        else
        {
            if (characterName.text == "")
            {
                characterName.text = "Character name";
            }
            logout.text = "Log out";
            //logoutAsk.text = "Do you really want to logout?";
            //yes.text = "Yes";
            //no.text = "No";
        }
    }
}
