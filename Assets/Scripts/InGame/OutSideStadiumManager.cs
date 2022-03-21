using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSideStadiumManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.LogError(InGameManager.Instance.timeNow);
    }
}
