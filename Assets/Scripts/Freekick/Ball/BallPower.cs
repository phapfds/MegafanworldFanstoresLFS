using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BallPower : MonoBehaviour
{
    private Rigidbody rigid;
    public float forwardF;
    public float upF;
    public float rightF;
    public event EventHandler IsKicked;
    private Vector3 posOrigin;
    private void Awake()
    {
        posOrigin = transform.position;
        rigid = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Kicker"))
        {
            IsKicked?.Invoke(this,null);
            ApplyForce();
        }
    }
    private void ApplyForce()
    {
        Debug.Log("Apply force");
        upF = BallForceSlider.upFValue;
        float valueF = 1200;
        forwardF = Arrow.direction.y*valueF;
        rightF = Arrow.direction.x*valueF;
        rigid.AddForce(Vector3.forward * forwardF + Vector3.up * upF + Vector3.right * rightF, ForceMode.Force);
    }
    public void OnReset()
    {
        transform.position = posOrigin;
    }
}
