using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHighscore : MonoBehaviour
{
    [SerializeField]
    private List<Image> effect_mainMenu;
    [SerializeField]
    private List<Image> Menu;
    [SerializeField]
    private List<Image> effect;
    public Transform content;
    public void Start()
    {
        ChooseTableMenu(0);
    }
    public void ChooseTableMenu(int index)
    {
        for (int i = 0; i < effect_mainMenu.Count; i++)
        {
            effect_mainMenu[i].gameObject.SetActive(index == i);
            Menu[i].gameObject.SetActive(index == i);
        }
        if (index == 0) ChooseTable(0);
        else ChooseTable(4);
    }
    public void ChooseTable(int index)
    {
        content.localPosition = new Vector3(0, 0, 0);
        HighscoreTable.inst.OnChangeTable(index);
        for (int i = 0; i < effect.Count; i++)
        {
            effect[i].gameObject.SetActive(index == i);
        }
    }
}
