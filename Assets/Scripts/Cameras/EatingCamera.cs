using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingCamera : MonoBehaviour
{
    #region
    [SerializeField] Vector3 startMarker = new Vector3();
    [SerializeField] Vector3 endMarker = new Vector3();
    [SerializeField] float speed = 3.5f;
    #endregion
    private void Awake()
    {
        transform.position = endMarker;
    }
    //private void Update()
    //{
    //    //transform.position = Vector3.Lerp(transform.position, endMarker, Time.fixedDeltaTime);

    //}
}
