using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class CaptureScreen : MonoBehaviour
{
    public string namePic;
    int index = 0;
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot("Captures/" + namePic + index + ".png");
            index++;
        }
#endif
    }
}
