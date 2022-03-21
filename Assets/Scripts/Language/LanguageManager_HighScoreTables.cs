using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LanguageManager_HighScoreTables : MonoBehaviour
{
    public Text score;
    public Text pos;
    public Text restart;
    //public Text resume;
    public List<Text> menu;

    public TextMeshProUGUI quitQuestion;
    public TextMeshProUGUI yes;
    public TextMeshProUGUI no;
    void OnEnable()
    {
        var club = Enum.GetName(typeof(Club), UserInfoManager.Instance.userInfo.club);
        if (LanguageManager.language == LanguageType.German)
        {
            score.text = "PUNKTE";
            pos.text = "STA";
            restart.text = "ZURÜCK";
            //resume.text = "FORTSETZEN";
            menu[0].text = "Pers. Highscore";
            menu[1].text = club + " highscore monat";
            menu[2].text = "Overall highscore monat";
            menu[3].text = "Overall highscore";
            menu[4].text = club + " ranking monat";
            menu[5].text = "Monatpunkte";
            menu[6].text = "Gesamttabelle monat";
            menu[7].text = "Gesamttabelle";
            quitQuestion.text = "Möchtest du wirklich aufhören?";
            yes.text = "Ja";
            no.text = "No";
        }
    }
}
