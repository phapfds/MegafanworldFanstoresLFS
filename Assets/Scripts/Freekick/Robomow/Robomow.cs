using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robomow : MonoBehaviour
{
    private Transform trans;
    public float speed = 0.8f;
    void Start()
    {
        trans = transform;
    }

    void FixedUpdate()
    {
        trans.position = Vector3.MoveTowards(trans.position, trans.position - Vector3.forward*speed, Time.deltaTime);
    }
}
