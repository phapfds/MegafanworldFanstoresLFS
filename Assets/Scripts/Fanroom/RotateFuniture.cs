using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateFuniture: MonoBehaviour
{
    public float mouseSpeedMultiplier = 8;
    public float smoothSpeed = 0.5f;
    private float mouseX;
    void OnMouseDrag()
    {
        //mouseX += Input.GetAxis("Mouse X") * mouseSpeedMultiplier;
        mouseX += Input.mouseScrollDelta.x * mouseSpeedMultiplier;
    }

    void LateUpdate() //Cause we are using Lerp function
    {
        mouseX += Input.mouseScrollDelta.y * mouseSpeedMultiplier;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -mouseX, 0), smoothSpeed);
    }
}
