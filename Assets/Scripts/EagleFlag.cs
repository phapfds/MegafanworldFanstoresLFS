using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleFlag : MonoBehaviour
{
    private void Awake()
    {
        GetComponentInChildren<Flag>().flagPositionType = FlagPositionType.BirdFlag;
        GetComponentInChildren<Flag>().StopDestroy();
    }
}
