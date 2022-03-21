using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Language_BarInterior : MonoBehaviour
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
            notice.text = "Du bist in einer Kneipe.\n" + "Trinke ein Bier und erhalte 30 Sekunden Bonuszeit.";
            buyABeer.text = "Kaufen Sie ein Bier, um 30 Sekunden zu verlängern";
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
        else
        {
            congratulation_header.text = "Relax!";
            notice.text = "You're in a pub.\n" + "Drink a beer and get 30 seconds bonus time.";
            buyABeer.text = "Buy a beer to extend 30 seconds";
            buy.text = "Buy";
            cancel.text = "Cancel";
            notHaveEnoughCredits.text = "Don't have enough credits, buy more";
            yes.text = "Yes";
            no.text = "No";
        }
    }
}
