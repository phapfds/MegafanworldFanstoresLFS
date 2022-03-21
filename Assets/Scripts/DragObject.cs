using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private float mYCoord;
    public static bool isDrag;
    public bool isTable;
    public bool canDrag;
    public bool isSelectedRotate;
    public float mouseSpeedMultiplier = 1f;
    public float smoothSpeed = 1f;
    private float mouseX;
    public float euleurX;
    public float euleurZ;
    //public bool furnitureDefault;
    private void Awake()
    {
        //if (ViewsManager.Instance.currentView.viewType == ViewType.FanroomView)
        //{
        string vector3_ = PlayerPrefs.GetString(UserInfoManager.Instance.userInfo.userID + gameObject.name);
        if (vector3_ != null && vector3_ != "")
            transform.localPosition = StringToVector3(vector3_);

        string rotation_ = PlayerPrefs.GetString(UserInfoManager.Instance.userInfo.userID + gameObject.name + "rotation");
        //Debug.Log(UserInfoManager.Instance.userInfo.userID + gameObject.name + "rotation");
        if (rotation_ != null && rotation_ != "")
            transform.eulerAngles = StringToVector3(rotation_);
        //}
        euleurX = transform.eulerAngles.x;
        euleurZ = transform.eulerAngles.z;
    }
    void OnMouseDown()
    {
        if (canDrag)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            // Store offset = gameobject world pos - mouse world pos
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint_();
            isDrag = true;
            if (!isTable)
                transform.localPosition = new Vector3(transform.localPosition.x, 2, transform.localPosition.z);
            FanroomManager.inst.SelectedObjectToRotate(gameObject.name);
        }
    }
    private void OnMouseUp()
    {
        isDrag = false;
    }
    private Vector3 GetMouseAsWorldPoint_()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        //Debug.Log(mousePoint);
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }
    public Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
    void OnMouseDrag()
    {
        if (canDrag)
        {
            //if (ViewsManager.Instance.currentView.viewType == ViewType.FanroomView)
            Vector3 vt = GetMouseAsWorldPoint_() + mOffset;
            if (vt.x < FanroomManager.inst.left.position.x) vt.x = FanroomManager.inst.left.position.x;
            if (vt.x > FanroomManager.inst.right.position.x) vt.x = FanroomManager.inst.right.position.x;
            if (vt.z < FanroomManager.inst.bottom.position.z) vt.z = FanroomManager.inst.bottom.position.z;
            if (vt.z > FanroomManager.inst.top.position.z) vt.z = FanroomManager.inst.top.position.z;
            transform.position = vt;
            if (!isTable)
                transform.localPosition = new Vector3(transform.localPosition.x, 2, transform.localPosition.z);
        }
    }
    private void Update()
    {
        if (transform.position.y < -5)
            transform.localPosition = new Vector3(transform.localPosition.x, 2, transform.localPosition.z);
    }
    void LateUpdate()
    {
        if (isSelectedRotate)
        {
            mouseX += Input.mouseScrollDelta.y * mouseSpeedMultiplier;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euleurX, -mouseX/2, euleurZ), smoothSpeed);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, mouseX), smoothSpeed);
        }
    }
}