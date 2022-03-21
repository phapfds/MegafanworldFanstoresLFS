using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManage : MonoBehaviour
{
    public static LightManage ins { private set; get; }
    public List<GameObject> frames;
    public List<GameObject> lights;
    private void Awake()
    {
        ins = this;
    }
    public void CheckLight()
    {
        for (int i = 0; i < frames.Count; i++)
        {
            lights[i].SetActive(frames[i].GetComponentInChildren<ArtSwap>() != null);
            lights[i].transform.parent.Find("Object_1").gameObject.SetActive(frames[i].GetComponentInChildren<ArtSwap>() != null);
            lights[i].transform.parent.Find("Object_3").gameObject.SetActive(frames[i].GetComponentInChildren<ArtSwap>() != null);
        }
    }
    private void Start()
    {
        CheckLight();
    }

}
