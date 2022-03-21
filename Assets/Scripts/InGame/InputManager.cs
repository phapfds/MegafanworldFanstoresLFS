using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public static Vector3 direction;
    public static VariableJoystick variableJoystick;
    void OnEnable()
    {
        variableJoystick = FindObjectOfType<VariableJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") + variableJoystick.Horizontal;
        vertical = Input.GetAxisRaw("Vertical") + variableJoystick.Vertical;
        direction = new Vector3(horizontal, 0, vertical).normalized;
        if(variableJoystick.gameObject.activeSelf == false)
        {
            direction = Vector3.zero;
        }
    }
}
