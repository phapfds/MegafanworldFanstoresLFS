using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LanguageManager_IAP : MonoBehaviour
{
    public Text creditPuchase;
    public Text buy_1;
    public Text buy_2;
    public Text buy_3;
    public Text back;
    public Text Purchase;
    //public Text purchaseSuccessful;
    void OnEnable()
    {
        if (LanguageManager.language == LanguageType.German)
        {
            creditPuchase.text = "Credits kaufen";
            buy_1.text = "Kaufen 30 Credits (3eurs)";
            buy_2.text = "Kaufen 60 Credits (5eurs)";
            buy_3.text = "Kaufen 120 Credits (10eurs)";
            back.text = "Zurück";
            Purchase.text = "Kaufen";
            //purchaseSuccessful.text = "Dein Kauf war erfolgreich";
        }
    }
}
