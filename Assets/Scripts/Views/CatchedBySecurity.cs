using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatchedBySecurity : MonoBehaviour
{
    public Text dialogueText;
    private string sentence;
    private void OnEnable()
    {
        StartCoroutine("TypeSentence");
    }
    IEnumerator TypeSentence()
    {
        //if (LanguageManager.language == LanguageType.German)
        //{
        //    sentence = "SECURITY:\n Hallo Sir, \n ich übernehme die Verantwortung dafür, dass Fans nicht die Flaggen anderer Fans angreifen dürfen.";
        //}
        //else
        //    sentence = "SERCURITY:\n Hello sir,\n I take responsible for not allowing guys to attack other's flags";
        //dialogueText.text = " ";

        //foreach (char letter in sentence.ToCharArray())
        //{
        //    dialogueText.text += letter;
        //    yield return null;
        //}
        //yield return new WaitForSecondsRealtime(1);
        if (LanguageManager.language == LanguageType.German)
        {
            sentence = "SECURITY:\n Bitte greifen Sie sie nicht mehr an.\n Sie verlieren für diese Zeit 50 Punkte. \n Danke für dein Verständnis!";
        }
        else
            sentence = "SERCURITY:\n Please don't attack other's flags anymore.\n You will lose 50 points for this time. \n Thanks for your time!";
        dialogueText.text = " ";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        //yield return new WaitForSecondsRealtime(1);
        if (LanguageManager.language == LanguageType.German)
        {
            sentence = "SPIELER:\n OK, ich werde diesen Fehler nicht noch einmal machen ...";
        }
        else
            sentence = "PLAYER:\n OK, I won't make this mistake again...";
        dialogueText.text = " ";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        //InGameManager.Instance.IngameState = IngameState.Ingame;
    }
}
