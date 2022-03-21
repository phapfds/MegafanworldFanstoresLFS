using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        transform.RotateAround(target.position, -Vector3.right, Time.deltaTime*150);
    }
}
