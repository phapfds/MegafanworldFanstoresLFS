using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LanguageManager_WalkingStreet : MonoBehaviour
{
    public Text time;
    public Text howToPlay_header;
    public Text howToPlay;

    public TextMeshProUGUI quitQuestion;
    public TextMeshProUGUI quitQuestion_1;
    public TextMeshProUGUI quitQuestion_2;
    public TextMeshProUGUI yes;
    public TextMeshProUGUI no;
    public TextMeshProUGUI yes_1;
    public TextMeshProUGUI no_1;
    public TextMeshProUGUI yes_2;
    public TextMeshProUGUI no_2;

    //public TextMeshProUGUI nextRound;


    void Start()
    {
        if (LanguageManager.language == LanguageType.German)
        {
            time.text = "ZEIT";
            howToPlay_header.text = "Spielanleitung";
            howToPlay.text = "- Bewegen Sie sich mit dem Joystick.\n" + "- Finde feindliche rote Flaggen und entferne sie per Klick.\n" + "-Hole Dir einen Bembel oder ein Bier für Bonuszeit.\n";
            quitQuestion.text = "Möchtest du wirklich aufhören?";
            quitQuestion_1.text = "Möchtest du wirklich aufhören?";
            quitQuestion_2.text = "Möchtest du wirklich aufhören?";
            yes.text = "Ja";
            yes_1.text = "Ja";
            yes_2.text = "Ja";
            no.text = "Nein";
            no_1.text = "Nein";
            no_2.text = "Nein";
            //nextRound.text = "Fortsetzung folgt,\n" + "Du gehst ins Stadion...";
        }
        else
        {
            time.text = "TIME";
            howToPlay_header.text = "How to play";
            howToPlay.text = "- Use joystick to move around.\n" + "- Find red enemy flags and remove them by click.\n" + "- Get a beer for bonus time\n";
            quitQuestion.text = "Do you really want to go to profile?";
            quitQuestion_1.text = "Do you really want to quit?";
            quitQuestion_2.text = "Do you really want to quit?";
            yes.text = "Yes";
            yes_1.text = "Yes";
            yes_2.text = "Yes";
            no.text = "No";
            no_1.text = "No";
            no_2.text = "No";
            //nextRound.text = "To be continued,\n" + "You are going to the stadium ...";
        }
    }
}
