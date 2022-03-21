using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenChooseArt : MonoBehaviour
{
    public Material privateMat;
    public static Material currentMat;

    //private void OnMouseUp()
    //{
    //    if (!EventSystem.current.IsPointerOverGameObject())
    //    {
    //        currentMat = privateMat;
    //        FanroomView frv = ViewsManager.Instance.currentView as FanroomView;
    //        frv.OpenChooseArt();
    //    }
    //}
}
