using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FanStoreBuyItem : MonoBehaviour
{
    public int id;
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            FanstoreManager.inst.ShowItemDetail?.Invoke(id);
    }
}
