using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class LanguageControl : MonoBehaviour
{
    public Dropdown m_Dropdown;
    private void OnEnable()
    {
        m_Dropdown.value = (int)LanguageManager.language;
    }
    void Start()
    {
        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(Dropdown change)
    {
        LanguageManager.language = (LanguageType)change.value;
    }


}
