using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanguageType
{
    German,
    English,
    
}
public class LanguageManager : MonoBehaviour
{
    public static LanguageType language = LanguageType.English;
}
