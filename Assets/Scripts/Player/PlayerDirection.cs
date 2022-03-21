using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    public static Transform trans;
    private void Awake()
    {
        trans = transform;
    }
    public static float DotBetweenForwardDirectionAndRightVector()
    {
        Vector3 forward = Vector3.zero;
        if (trans != null)
        {
            forward = trans.TransformDirection(Vector3.forward);
        }
        return Vector3.Dot(forward, Vector3.right);
    }


}
