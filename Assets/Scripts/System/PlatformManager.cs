using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformType
{
    Windows,
    UWP,
    Android
}
public class PlatformManager : MonoBehaviour
{
    public PlatformType platformType = PlatformType.UWP;
    public static PlatformManager inst;
    private void Awake()
    {
        inst = this;
    }
}
