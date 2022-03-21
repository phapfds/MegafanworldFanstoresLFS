using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInvisible : MonoBehaviour
{
    public Renderer _renderer;
    public void Update()
    {
        if (!_renderer.isVisible)
        {
            Debug.Log("Invisible");
            //transform.parent.transform.position = Vector3.zero;
            transform.position = Vector3.zero;
        }
    }
}
