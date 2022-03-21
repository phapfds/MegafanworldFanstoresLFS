using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoundManager : MonoBehaviour
{

    public static SelectRoundManager ins;
    public int scoreUnlock;
    //public static int munichScoreUnlock;
    //public static int fankfurtScoreUnlock;

    //[HideInInspector] public int munichStadiumUnlock;
    //[HideInInspector] public int frankfurtStadiumUnlock;

    private void Awake()
    {
        ins = this;
        //munichStadiumUnlock = PlayerPrefs.GetInt("MunichStadiumUnlock", 0);
        //frankfurtStadiumUnlock = PlayerPrefs.GetInt("FrankfurtStadiumUnlock", 0);
    }
}
