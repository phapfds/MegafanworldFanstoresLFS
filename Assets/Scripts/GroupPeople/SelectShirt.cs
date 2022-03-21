using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShirtType
{
    FanShirt,
    Flag
}
public class SelectShirt : MonoBehaviour
{
    [SerializeField] ShirtType shirtType;
    public List<Texture> shirts;
    public Material mat;
    void Start()
    {
        //Debug.LogError(gameObject.name);
        int i = (int)UserInfoManager.Instance.userInfo.club;
        if (i == 14) i = (int)Club.Dortmund;
        if (shirts[i] != null)
            mat.mainTexture = shirts[i];
    }
    //private void OnDisable()
    //{
    //    if(shirtType == ShirtType.Flag)
    //    {
    //        mat.mainTexture = shirts[(int)Club.Bremen];
    //    }
    //}
}
