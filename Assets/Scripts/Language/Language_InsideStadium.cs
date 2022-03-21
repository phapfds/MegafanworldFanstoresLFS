using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Language_InsideStadium : MonoBehaviour
{
    public Text congratulation_header;
    public Text notice;
    public Text buyABeer;
    public Text buy;
    public Text cancel;
    public Text notHaveEnoughCredits;
    public Text yes;
    public Text no;

    public Text time;
    public TextMeshProUGUI quitQuestion;
    public TextMeshProUGUI yes_1;
    public TextMeshProUGUI no_1;

    void OnEnable()
    {
        if (LanguageManager.language == LanguageType.German)
        {
            congratulation_header.text = "Entspann Dich!";
            notice.text = " Du kaufst eine Bratwurst.\n" + " Isst eine Bratwurst, um 30 Sekunden Bonuszeit zu bekommen.";
            buyABeer.text = "Kaufen Sie ein Bratwurst, um 30 Sekunden zu verlängern";
            buy.text = "Kaufen";
            cancel.text = "Stornieren";

            notHaveEnoughCredits.text = "Haben Sie nicht genug Credits, kaufen Sie mehr";
            yes.text = "Ja";
            no.text = "Nein";

            time.text = "ZEIT";
            quitQuestion.text = "Möchtest du wirklich aufhören?";
            yes_1.text = "Ja";
            no_1.text = "Nein";
        }
    }
}
