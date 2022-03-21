using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallForceSlider : MonoBehaviour
{
    public Slider upF;
    public KickerInputManager kickerInputManager;
    public static float upFValue;
    private void Start()
    {
        kickerInputManager.Kick += KickerInputManager_Kick;
    }
    private void OnEnable()
    {
        upF.value = 0;
    }
    private void KickerInputManager_Kick(object sender, TypeKickInput e)
    {
        if (e == TypeKickInput.Kick)
        {
            upFValue = upF.value;
            upF.gameObject.SetActive(false);
        }

    }
    private void FixedUpdate()
    {
        if (upF.gameObject.activeSelf == false)
            upF.value = 0;
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (upF.value < upF.maxValue)
                    upF.value += 6;
                else
                    upF.value = 0;
            }
        }
    }
}
