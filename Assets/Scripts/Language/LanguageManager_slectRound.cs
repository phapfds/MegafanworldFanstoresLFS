using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageManager_slectRound : MonoBehaviour
{
    public Text walkingStreet;
    public Text commingSoon;

    void OnEnable()
    {
        if (LanguageManager.language == LanguageType.German)
        {
            walkingStreet.text = "Straße";
            commingSoon.text = "Stadion";
        }
        else
        {
            walkingStreet.text = "Stadion";
            commingSoon.text = "Stadium";
        }
    }
}
