using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusUI : MonoBehaviour
{
    public Text _name;
    public Text _text;
    public StatusUI(string _name, string _text)
    {
        this._name.text = _name;
        this._text.text = _text;
    }
}
