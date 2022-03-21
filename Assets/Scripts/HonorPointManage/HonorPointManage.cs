using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonorPointData
{
    public float hornor_point;
    public string email;
    public HonorPointData(float point, string email)
    {
        this.hornor_point = point;
        this.email = email;
    }
}
public class Response
{
    public string status;
    public string message;
    public string data;
}

public class HonorPointManage : MonoBehaviour
{
    public static HonorPointManage ins { private set; get; }
    [SerializeField] private bool isTest;
    private readonly string urlTest = "https://3dgallery.fdssoft.com/dashboard/hornor/hornor-point";
    private readonly string urlProd = "https://megafanworld.fdssoft.com/dashboard/hornor/hornor-point";
    private void Awake()
    {
        ins = this;
    }
    public void AddHonorPoint(float hp)
    {
        string url = isTest ? urlTest : urlProd;
        HonorPointData honorPointData = new HonorPointData(hp, UserInfoManager.Instance.userInfo.email);
        RestClient.Post<Response>(url, honorPointData).Then(res => {
            Debug.Log(JsonUtility.ToJson(res));
            Debug.Log(hp + "_" + UserInfoManager.Instance.userInfo.email);
        });
    }
}
