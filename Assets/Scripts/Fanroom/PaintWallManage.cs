using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintWallManage : MonoBehaviour
{
    [SerializeField] Material matWall;
    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("ColorKey", "E1E4E7FF"), out Color color);
        //Debug.Log(color);
        matWall.color = color;
    }
}
