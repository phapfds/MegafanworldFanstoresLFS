using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Language : MonoBehaviour
{
    [SerializeField] List<string> english;
    [SerializeField] List<string> german;
    [SerializeField] List<Text> texts;

    private void OnEnable()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            if (LanguageManager.language == LanguageType.English)
            {
                texts[i].text = english[i];
            }
            else
            {
                texts[i].text = german[i];
            }
        }
    }
}
