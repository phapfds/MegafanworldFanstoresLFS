using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StatusPost
{
    public List<Status> status = new List<Status>();
}
[System.Serializable]
public class Status
{
    public string name;
    public string text;
}