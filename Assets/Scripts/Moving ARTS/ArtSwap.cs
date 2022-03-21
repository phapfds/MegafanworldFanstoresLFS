using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArtSwap : MonoBehaviour
{
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();

        string parentName = PlayerPrefs.GetString(UserInfoManager.Instance.userInfo.userID + gameObject.name);
        if (parentName != null && parentName != "")
            gameObject.transform.parent.Find("SwapIcon").GetComponent<SwapIcon>().art = null;

    }
    private void Start()
    {
        LoadPosition();
    }
    public void LoadPosition()
    {
        string parentName = PlayerPrefs.GetString(UserInfoManager.Instance.userInfo.userID + gameObject.name);
        if (parentName != null && parentName != "")
        {
            //gameObject.transform.parent.Find("SwapIcon").GetComponent<SwapIcon>().art = null;
            GameObject parent = GameObject.Find(parentName);
            gameObject.transform.parent = parent.transform;
            transform.localPosition = new Vector3(0, 0, 0.01f);
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = new Vector3(0.9615384f, 0.9615384f, 1);
            parent.transform.Find("SwapIcon").GetComponent<SwapIcon>().art = this.gameObject;
            LightManage.ins.CheckLight();
        }
    }
    public void OnMouseDown()
    {
        if (MovingArtManager.inst.canSwap && !EventSystem.current.IsPointerOverGameObject())
        {
            MovingArtManager.inst.currentArtToChange = this.gameObject;
            MovingArtManager.inst.canSwap = false;
            MovingArtManager.inst.EnableSwapIcons();
            transform.parent.Find("SwapIcon").gameObject.SetActive(false);
            transform.parent.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void SavePosition()
    {
        try
        {
            PlayerPrefs.SetString(UserInfoManager.Instance.userInfo.userID + gameObject.name, transform.parent.name);
        }
        catch
        {
            PlayerPrefs.SetString(gameObject.name, transform.parent.name);
        }
    }
    private void OnDisable()
    {
        SavePosition();
    }
}
