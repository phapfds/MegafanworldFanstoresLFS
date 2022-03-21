using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveItemView : Views
{
    public Material matWall;
    public GameObject chooseFloorMat;
    public Material matFloor;
    private void OnEnable()
    {
        chooseFloorMat.gameObject.SetActive(false);

    }

    public void Close()
    {
        FanroomManager.inst.ChangeToFanroomView();
        FanroomManager.inst.SavePosition();
    }

    public void PaintWall()
    {
        ColorPicker.Create(matWall.color, "PAINT THE WALL", SetColor, ColorFinish, true);
        chooseFloorMat.SetActive(!chooseFloorMat.activeSelf);
    }

    public void SetColor(Color currentColor)
    {
        matWall.color = currentColor;
    }

    public void ColorFinish(Color finishColor)
    {
        //Debug.Log(ColorUtility.ToHtmlStringRGBA(finishColor));
        matWall.color = finishColor;
        PlayerPrefs.SetString("ColorKey", ColorUtility.ToHtmlStringRGBA(finishColor)); // with alpha

    }


    public void SelectMatFloor(int index)
    {
        FanroomManager.inst.ChooseMatFloor(index);
        PlayerPrefs.SetInt("FloorMatKey", index);

    }

    //public void SelectMatFloor(FloorMat floorMat)
    //{
    //    matFloor.mainTexture = floorMat.mainTexture;
    //    matFloor.SetTexture("_BumpMap", floorMat.normalMap);
    //    //if (floorMat.metallicMap != null)
    //    matFloor.SetTexture("_MetallicGlossMap", floorMat.metallicMap);
    //    PlayerPrefs.SetString("FloorMatKey", floorMat.name);
    //}
    private void OnDisable()
    {
        foreach(DragObject dr in FanroomManager.inst.dragObjects)
        {
            if (dr != null)
                dr.isSelectedRotate = false;
        }
    }
}
