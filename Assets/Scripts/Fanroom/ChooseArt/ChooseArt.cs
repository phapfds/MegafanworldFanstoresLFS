using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ChooseArt : MonoBehaviour
{
    public Texture _image;
    public void Choose()
    {
        OpenChooseArt.currentMat.mainTexture = _image;
        GameObject.Find("ChooseArt").SetActive(false);
    }
}
