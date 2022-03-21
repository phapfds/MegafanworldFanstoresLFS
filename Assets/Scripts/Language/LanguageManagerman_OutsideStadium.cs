using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageManagerman_OutsideStadium : MonoBehaviour
{
    public Text time;

    public TextMeshProUGUI quitQuestion;
    public TextMeshProUGUI quitQuestion_1;
    public TextMeshProUGUI quitQuestion_2;
    public TextMeshProUGUI yes;
    public TextMeshProUGUI no;
    public TextMeshProUGUI yes_1;
    public TextMeshProUGUI no_1;
    public TextMeshProUGUI yes_2;
    public TextMeshProUGUI no_2;

    public Text buyABeer;
    public Text buy;
    public Text cancel;
    public Text notHaveEnoughCredits;
    public Text yes_3;
    public Text no_3;
    public Text noticeInStadium_header;
    public Text noticeInStadium;

    void Start()
    {
        if (LanguageManager.language == LanguageType.German)
        {
            time.text = "ZEIT";
            quitQuestion.text = "Möchtest du wirklich aufhören?";
            quitQuestion_1.text = "Möchtest du wirklich aufhören?";
            quitQuestion_2.text = "Möchtest du wirklich aufhören?";
            yes.text = "Ja";
            yes_1.text = "Ja";
            yes_2.text = "Ja";
            no.text = "Nein";
            no_1.text = "Nein";
            no_2.text = "Nein";

            buyABeer.text = "Kaufen Sie ein Bratwurst, um 30 Sekunden zu verlängern";
            buy.text = "Kaufen";
            cancel.text = "Stornieren";
            notHaveEnoughCredits.text = "Haben Sie nicht genug Credits, kaufen Sie mehr";
            yes_3.text = "Ja";
            no_3.text = "Nein";
            noticeInStadium_header.text = "Entspann Dich!";
            noticeInStadium.text = " Du kaufst eine Bratwurst.\n" + " Ist eine Bratwurst, um 30 Sekunden Bonuszeit zu bekommen.";
        }
        else
        {
            time.text = "TIME";
            quitQuestion.text = "Do you really want to go to profile?";
            quitQuestion_1.text = "Do you really want to quit?";
            quitQuestion_2.text = "Do you really want to quit?";
            yes.text = "Yes";
            yes_1.text = "Yes";
            yes_2.text = "Yes";
            no.text = "No";
            no_1.text = "No";
            no_2.text = "No";

            buyABeer.text = "Buy a bratwurst to extend time";
            buy.text = "Buy";
            cancel.text = "Cancel";
            notHaveEnoughCredits.text = "Don't have enough credits, buy more";
            yes_3.text = "Yes";
            no_3.text = "No";
            noticeInStadium_header.text = "Relax!";
            noticeInStadium.text = "You're buying a bratwurst.\n" + "Eat a bratwurst to get 30 seconds bonus time";
        }
    }
}
