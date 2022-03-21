using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobomowCamera : MonoBehaviour
{
    public Vector3 aimPos;
    public Transform robotMow;


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(aimPos.x, aimPos.y, robotMow.position.z-7), Time.deltaTime);
    }
}
