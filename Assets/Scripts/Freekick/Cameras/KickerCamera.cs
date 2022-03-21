using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickerCamera : MonoBehaviour
{
    [SerializeField] Transform kicker;
    [SerializeField] Transform cam;
    Vector3 offset;
    [SerializeField] Vector3 startPoint;
    [SerializeField] Vector3 endPoint;

    void Start()
    {
        offset = endPoint - kicker.position;
        transform.position = startPoint;
    }

    void FixedUpdate()
    {
        if(FreeKickManager.Ins.currentState == FreeKickState.Introduction)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, 0.2f);
        }
        else
            transform.position = kicker.position + offset;
    }
}
