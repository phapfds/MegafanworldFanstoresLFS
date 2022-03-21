using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject waypointInVision;
    public GameObject waypointNotInVision;

    public GameObject KeyPort;
    private Vector3 offect = Vector3.up;
    public static GameObject keyport;
    private void OnEnable()
    {
        waypointInVision.SetActive(false);
        waypointNotInVision.SetActive(false);
        keyport = null;
    }
    private void Update()
    {
        if (keyport != null)
        {
            var screenPos = Camera.main.WorldToScreenPoint(keyport.transform.position + offect);
            //Debug.LogError(screenPos);
            if (screenPos.z > 0 && screenPos.x > 0 && screenPos.y > 0)
            {
                waypointInVision.transform.position = screenPos;
                waypointNotInVision.SetActive(false);
                waypointInVision.SetActive(true);
            }
            else
            {
                waypointNotInVision.SetActive(true);
                waypointInVision.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        keyport = null;
    }
}
