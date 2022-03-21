using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloorMat : MonoBehaviour
{
    public Texture mainTexture;
    public Texture normalMap;
    public Texture metallicMap;
    private void Awake()
    {
        mainTexture = GetComponent<RawImage>().texture;
    }
}
