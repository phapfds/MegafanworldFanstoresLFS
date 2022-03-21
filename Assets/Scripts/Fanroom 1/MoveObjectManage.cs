using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectManage : MonoBehaviour
{
    public static MoveObjectManage ins { get; private set; }
    public Camera cam;
    public GameObject gameObjectIsSelected;
    public GameObject gameObjectRef;
    public Vector3 originPosOfGOIsSelected;
    public Texture2D cursorTexture;
    void Awake()
    {
        ins = this;
    }
    void Update()
    {
        if (ViewsManager.Instance.currentView.viewType == ViewType.FanroomView)
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //print("I'm looking at " + hit.transform.name);
            }
            else
            {
                //print("I'm looking at nothing!");
            }
            RaycastHit hitMouse;
            Ray rayMouse = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayMouse, out hitMouse))
            {
                if (hit.transform.name == hitMouse.transform.name)
                {

                    if (hit.transform.gameObject.GetComponent<ObjectToMove>() != null)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            FanroomView frv = ViewsManager.Instance.dicViews[ViewType.FanroomView] as FanroomView;
                            frv.OpenUIMoveObject();
                            gameObjectIsSelected = hit.transform.gameObject;
                            originPosOfGOIsSelected = hit.transform.gameObject.transform.position;
                        }
                    }
                }
                else
                {

                }
            }
        }
    }
    public void LateUpdate()
    {
        if (gameObjectIsSelected != null)
        {
            gameObjectIsSelected.transform.position = gameObjectRef.transform.position;
            gameObjectIsSelected.transform.rotation = gameObjectIsSelected.transform.rotation;

        }
    }
    public void FreeIt()
    {
        if (gameObjectIsSelected != null)
        {
            //FanroomManager.inst.SavePosition();
            PlayerPrefs.SetString(UserInfoManager.Instance.userInfo.userID + gameObjectIsSelected.name, gameObjectIsSelected.transform.position.ToString());
            PlayerPrefs.SetString(UserInfoManager.Instance.userInfo.userID + gameObjectIsSelected.name + "rotation", gameObjectIsSelected.transform.eulerAngles.ToString());
            CheckContainer checkContainer = gameObjectIsSelected.GetComponent<CheckContainer>();
            if (checkContainer != null)
            {
                checkContainer.Check();
            };

            foreach (CheckContainer checkContainer1 in gameObjectIsSelected.GetComponentsInChildren<CheckContainer>())
            {
                if (checkContainer1 != null)
                    checkContainer1.SavePosition();
            }
            gameObjectIsSelected = null;
        }
    }
    public void CancelMove()
    {
        if (gameObjectIsSelected != null)
        {
            gameObjectIsSelected.transform.position = originPosOfGOIsSelected;
            gameObjectIsSelected = null;
        }
    }
}
