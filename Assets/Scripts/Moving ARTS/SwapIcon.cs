using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapIcon : MonoBehaviour
{
    public GameObject frame;
    public GameObject art;
    private void Awake()
    {
        frame = art.transform.parent.gameObject;
        //art = null;
    }
    public void OnMouseDown()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (art != null)
            {
                art.transform.SetParent(MovingArtManager.inst.currentArtToChange.transform.parent);
                art.transform.localPosition = new Vector3(0, 0, 0.01f);
                art.transform.localEulerAngles = Vector3.zero;
                art.transform.localScale = new Vector3(0.9615384f, 0.9615384f, 1);
                MovingArtManager.inst.currentArtToChange.transform.parent.Find("SwapIcon").GetComponent<SwapIcon>().art = art;
            }
            else
            {
                MovingArtManager.inst.currentArtToChange.transform.parent.Find("SwapIcon").GetComponent<SwapIcon>().art = null;
            }
            MovingArtManager.inst.currentArtToChange.transform.SetParent(frame.transform);
            MovingArtManager.inst.currentArtToChange.transform.localPosition = new Vector3(0, 0, 0.01f);
            MovingArtManager.inst.currentArtToChange.transform.localEulerAngles = Vector3.zero;
            MovingArtManager.inst.currentArtToChange.transform.localScale = new Vector3(0.9615384f, 0.9615384f, 1);
            art = MovingArtManager.inst.currentArtToChange;
            MovingArtManager.inst.canSwap = true;
            LightManage.ins.CheckLight();
            MovingArtManager.inst.DisableSwapIcons();
        }


    }



}
