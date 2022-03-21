using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExpandMapType
{
    left,
    right
}
public class ExpandMap : MonoBehaviour
{
    public ExpandMapType expandMap;
    public List<GameObject> city;
    public int index;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            //Debug.LogError("Player enter expandmap");
            switch (expandMap)
            {
                case ExpandMapType.left:
                    for (int i = 0; i < city.Count; i++)
                    {
                        if (i == index || i == index - 1) city[i].SetActive(true);
                        else
                            city[i].SetActive(false);
                    }
                    break;
                case ExpandMapType.right:
                    for (int i = 0; i < city.Count; i++)
                    {
                        if (i == index || i == index + 1) city[i].SetActive(true);
                        else
                            city[i].SetActive(false);
                    }
                    break;
            }
        }
    }
}
